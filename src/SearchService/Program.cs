
using MassTransit;
using SearchService;
using SearchService.Consumers;
using SearchService.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//await DB.InitAsync("SearchDb", builder.Configuration.GetConnectionString("MongoDbConnection")!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x => {
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ReceiveEndpoint("search-auction-created", e => {
            e.UseMessageRetry(r=>r.Interval(5,5));
            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
//app.usehttpsredirection();
app.UseAuthorization();


app.MapControllers();
try
{
    Console.WriteLine("Attempting to seed the Database");
    await DbInitializer.InitDb(app);

} catch (Exception ex)
{
    await Console.Error.WriteLineAsync(ex.ToString());
}

app.Run();

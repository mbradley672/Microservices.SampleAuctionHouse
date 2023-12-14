using BiddingService.Consumers;
using BiddingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => {
           options.Authority = builder.Configuration["IdentityService:Uri"];
           options.RequireHttpsMetadata = false;
           options.TokenValidationParameters.ValidateAudience = false;
           options.TokenValidationParameters.NameClaimType = "username";
       });

builder.Services.AddMassTransit(x => {

    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
});

builder.Services.AddHostedService<CheckAuctionFinished>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
 
app.UseAuthorization();
app.MapControllers();

await DB.InitAsync(
    "BidDb", MongoClientSettings.FromConnectionString(
        builder.Configuration.GetConnectionString("DefaultConnection")));


app.Run();

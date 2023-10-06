
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//await DB.InitAsync("SearchDb", builder.Configuration.GetConnectionString("MongoDbConnection")!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

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

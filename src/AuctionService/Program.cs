
using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Entities;
using AuctionService.Services;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuctionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<AuctionDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddMassTransit(x => {
                x.AddEntityFrameworkOutbox<AuctionDbContext>(options => {
                    options.QueryDelay = TimeSpan.FromSeconds(10);
                    options.UsePostgres();
                    options.UseBusOutbox();
                });
                x.UsingRabbitMq((context, cfg) => {
                    cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
                        host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                        host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
                    });
                    cfg.ConfigureEndpoints(context);
                });
                x.AddConsumersFromNamespaceContaining<AuctionCreateFaultConsumer>();
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options => {
                       options.Authority = builder.Configuration["IdentityService:Uri"];
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters.ValidateAudience = false;
                       options.TokenValidationParameters.NameClaimType = "username";
                   });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapGrpcService<GrpcAuctionService>();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.usehttpsredirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            try
            {
                DbInitializer.InitDb(app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            app.Run();
        }
    }
}

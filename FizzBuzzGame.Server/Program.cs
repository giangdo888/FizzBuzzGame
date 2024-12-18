
using FizzBuzzGame.Server.Data;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Repositories;
using FizzBuzzGame.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FizzBuzzGameDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            //add repositories
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IGameRuleRepository, GameRuleRepository>();
            builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();

            //add services
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IGameRuleService, GameRuleService>();
            builder.Services.AddScoped<IAttemptService, AttemptService>();

            //inject Dictionary
            builder.Services.AddSingleton<Dictionary<int, AttemptState>>();
            builder.Services.AddSingleton<Dictionary<int, string>>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

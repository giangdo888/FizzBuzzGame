
using FizzBuzzGame.Server.Data;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Repositories;
using FizzBuzzGame.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            builder.Services.AddSwaggerGen(/*opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            }*/);

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

            //configure authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
                options.Audience = builder.Configuration["Auth0:Audience"];
            });

            //allow Cross-Origin Resource Sharing (CROS)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigins");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using var serviceScope = app.Services.CreateScope();
                using  var dbContext = serviceScope.ServiceProvider.GetService<DbContext>();
                dbContext?.Database.Migrate();
            }

            // Automate migration application
            if (app.Environment.IsProduction())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<FizzBuzzGameDbContext>();
                    dbContext.Database.Migrate();
                }
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();



            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

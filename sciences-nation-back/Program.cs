using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using sciences_nation_back.Services;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Load .env
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowSpecificOrigins",
                //builder =>
                //{
                //    builder.WithOrigins("http://localhost:5173")
                //           .AllowAnyHeader()
                //           .AllowAnyMethod()
                //           .AllowCredentials();
                //});

                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Get environment variables
            var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            var databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");
            var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            // Register services singleton
            builder.Services.AddSingleton<MongoDbService>(serviceProvider =>
            {
                return new MongoDbService(connectionString, databaseName);
            });
            builder.Services.AddSingleton<IJwtService, JwtService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IFavoriteService, FavoriteService>();

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Configure JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                };
            });

            var app = builder.Build();

            // Statics Files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "assets")),
                RequestPath = "/assets"
            });

            // HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use CORS middleware
            app.UseCors("AllowAll");

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Business;
using VideoGameCatalog.Business.Logic;
using VideoGameCatalog.Repository.DbContext;
using VideoGameCatalog.Repository.Repositories.Implementation;
using VideoGameCatalog.Repository.Repositories.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("VideoGameSqlDbConnectionString")));

        RegisterServices(builder);
        RegisterRepositories(builder);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAngularApp");

        app.MapControllers();

        app.Run();
    }

    private static void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGameService, GameService>();
    }

    private static void RegisterRepositories(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGameRepository, GameRepository>();
    }
}
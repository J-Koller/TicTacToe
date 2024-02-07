
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using TicTacToe.Api.HostedServices;
using TicTacToe.Api.Hubs;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Logic.Services.Players.Ranks;
using TicTacToe.Api.Logic.Services.Strings;
using TicTacToe.Api.Logic.Services.Symbols;
using TicTacToe.Api.Shared.Services.Http.Request;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Repositories.Games;
using TicTacToe.Data.Repositories.Players;
using TicTacToe.Data.Repositories.Symbols;

namespace TicTacToe.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

            ConfigureBindings(builder);
            ConfigureDbContext(builder);
            ConfigureMappers(builder);

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins("http://localhost:4200")
                           .AllowCredentials()
                           .SetIsOriginAllowed(origin => true);

                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors();
            }

            app.UseSerilogRequestLogging();

            ConfigureHubs(app);

            Log.Information("Starting up the api! The time is: {time}", DateTime.UtcNow);

            app.Run();
        }

        private static void ConfigureHubs(WebApplication app)
        {
            app.MapControllers();
            app.MapHub<ChatHub>("/chatHub");
            app.MapHub<GameHub>("/gameHub");
        }

        private static void ConfigureMappers(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TicTacToeContext>(db =>
            {
                string connectionStringToTake = "local";

                string connectionString = builder.Configuration.GetConnectionString(connectionStringToTake);

                db.UseSqlServer(connectionString);
            });
        }

        private static void ConfigureBindings(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
            builder.Services.AddTransient<IGameRepository, GameRepository>();
            builder.Services.AddTransient<ISymbolRepository, SymbolRepository>();

            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<IMoveService, MoveService>();
            builder.Services.AddTransient<ISymbolAssignmentService, SymbolAssignmentService>();
            builder.Services.AddTransient<IStringService, StringService>();
            builder.Services.AddTransient<IRankService, RankService>();
            builder.Services.AddTransient<IApiConnectionService, RestSharpApiConnectionService>();

            builder.Services.AddHostedService<HeartbeatService>();
            builder.Services.AddHostedService<CleanGameService>();
        }
    }
}
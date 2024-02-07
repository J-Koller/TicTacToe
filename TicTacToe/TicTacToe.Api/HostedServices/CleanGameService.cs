using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Services.Http.Request;

namespace TicTacToe.Api.HostedServices
{
    public class CleanGameService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IApiConnectionService _apiConnectionService;

        private Timer _timer;

        public CleanGameService(IServiceProvider serviceProvider, IApiConnectionService apiConnectionService)
        {
            _serviceProvider = serviceProvider;
            _apiConnectionService = apiConnectionService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await CleanGames(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private async Task CleanGames()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var cleanResponse = await _apiConnectionService.MakeRequestAsync($"Games/clean", RestSharp.Method.Post);

                if (!cleanResponse.ResponseSuccessful)
                {
                    Log.Error("Error checking inactivity in hosted service.");
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

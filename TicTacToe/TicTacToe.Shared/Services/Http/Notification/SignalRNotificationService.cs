using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Enums;

namespace TicTacToe.Api.Shared.Services.Http.Notification
{
    public class SignalRNotificationService : INotificationService
    {
        private HubConnection _chatConnection;
        private HubConnection _gameConnection;

        private string _fullChatHubEndPoint => $"{_domain}/{_chatHubEndPoint}";
        private string _fullGameHubEndPoint => $"{_domain}/{_gameHubEndPoint}";

        private readonly string _domain = "http://localhost:5159";

        private readonly string _chatHubEndPoint = "chatHub";
        private readonly string _gameHubEndPoint = "gameHub";

        public string UserGameConnectionId => _gameConnection.ConnectionId;

        public string OpponentGameConnectionId { get; set; }

        public string[] GameConnectionIds => new string[] { UserGameConnectionId, OpponentGameConnectionId};

        public string ChatConnectionId => _chatConnection.ConnectionId;

        public async Task SendAsync(Hubs hub, string methodName, object argument, params string[] connectionIds)
        {
            try
            {
                if (hub == Hubs.Game)
                {
                    await _gameConnection.SendAsync(methodName, argument, connectionIds);
                }
                else if (hub == Hubs.Chat)
                {
                    await _chatConnection.SendAsync(methodName, argument);
                }
            }
            catch (Exception e)
            {
            }
        }

        public void ConfigureOn<TArg>(Hubs hub, string methodName, Action<TArg> handler)
        {
            if (hub == Hubs.Game)
            {
                _gameConnection.On(methodName, handler);
            }
            else if (hub == Hubs.Chat)
            {
                _chatConnection.On(methodName, handler);
            }
        }

        public void ConfigureOn<TArg, TReturn>(Hubs hub, string methodName, Func<TArg, TReturn> handler)
        {
            if (hub == Hubs.Game)
            {
                _gameConnection.On(methodName, handler);
            }
            else if (hub == Hubs.Chat)
            {
                _chatConnection.On(methodName, handler);
            }
        }

        public async Task StartConnectionAsync()
        {
            bool connectionsAreBuilt = _chatConnection != null && _gameConnection != null;

            if (!connectionsAreBuilt)
            {
                _chatConnection = new HubConnectionBuilder()
               .WithUrl(_fullChatHubEndPoint)
               .WithAutomaticReconnect()
               .Build();

                _gameConnection = new HubConnectionBuilder()
                    .WithUrl(_fullGameHubEndPoint)
                    .WithAutomaticReconnect()
                    .Build();
            }

            bool connectionsAreActive = _chatConnection.State == HubConnectionState.Connected && _gameConnection.State == HubConnectionState.Connected;

            if (connectionsAreActive)
            {
                return;
            }

            await _gameConnection.StartAsync();
            await _chatConnection.StartAsync();

        }

        public async Task StopConnectionAsync()
        {
            bool connectionsAreBuilt = _chatConnection != null && _gameConnection != null;

            if (connectionsAreBuilt)
            {
                await _chatConnection.DisposeAsync();
                await _gameConnection.DisposeAsync();
                _chatConnection = null;
                _gameConnection = null;
            }
        }
    }
}

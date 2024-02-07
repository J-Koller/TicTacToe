using System;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Enums;

namespace TicTacToe.Api.Shared.Services.Http.Notification
{
    public  interface INotificationService
    {
        public string[] GameConnectionIds { get; } 

        public string UserGameConnectionId { get;}

        public string OpponentGameConnectionId { get; set;  }

        public string ChatConnectionId { get;}

        Task StartConnectionAsync();

        Task StopConnectionAsync();

        Task SendAsync(Hubs hub, string methodName, object argument, params string[] connectionIds);

        void ConfigureOn<TArg>(Hubs hub, string methodName, Action<TArg> handler);

        void ConfigureOn<TArg, TReturn>(Hubs hub, string methodName, Func<TArg, TReturn> handler);
    }
}

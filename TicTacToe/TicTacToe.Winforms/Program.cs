using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Services.Http.Notification;
using TicTacToe.Api.Shared.Services.Http.Request;
using TicTacToe.WinFormsApp.Forms;
using TicTacToe.WinFormsApp.Services.Gui.Boxes;
using TicTacToe.WinFormsApp.Services.Gui.Buttons;
using TicTacToe.WinFormsApp.Services.Gui.ControlCoordinates;
using TicTacToe.WinFormsApp.Services.Gui.Labels;
using TicTacToe.WinFormsApp.Services.Gui.MessageBoxes;

namespace TicTacToe.WinFormsApp
{
    public static class Program
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();

        [STAThread]
        public static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();

                ConfigureBindings();

                ConfigureLogging();

                Log.Information("Hello from Winforms!");

                var serviceProvider = _serviceCollection.BuildServiceProvider();

                var frmMain = serviceProvider.GetService<FrmMain>();

                Application.ApplicationExit += LogOnExit;

                Application.Run(frmMain);
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
            }
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadName()
                .Enrich.WithMachineName()
                .MinimumLevel.Debug()
                .WriteTo.File
                    (formatter: new CompactJsonFormatter(),
                    "log/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
            .CreateLogger();
        }

        private static void ConfigureBindings()
        {
            _serviceCollection.AddTransient<IBoxService, BoxService>();
            _serviceCollection.AddTransient<IControlCoordinatesService, ControlTagCoordinatesService>();
            _serviceCollection.AddTransient<ILabelService, LabelService>();
            _serviceCollection.AddTransient<IMessageBoxService, MessageBoxService>();
            _serviceCollection.AddTransient<IButtonService, ButtonService>();

            _serviceCollection.AddTransient<INotificationService, SignalRNotificationService>();
            _serviceCollection.AddTransient<IApiConnectionService, RestSharpApiConnectionService>();

            _serviceCollection.AddTransient<FrmCreateAccount>();
            _serviceCollection.AddTransient<FrmMain>();
            _serviceCollection.AddTransient<FrmSignIn>();
        }

        public static void LogOnExit(object sender, EventArgs e)
        {
            Log.Information($"App closing at: {DateTime.Now}");
        }
    }
}
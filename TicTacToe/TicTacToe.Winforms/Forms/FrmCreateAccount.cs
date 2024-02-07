using System;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Services.Http.Request;
using TicTacToe.WinFormsApp.Services.Gui.Buttons;
using TicTacToe.WinFormsApp.Services.Gui.MessageBoxes;

namespace TicTacToe.WinFormsApp.Forms
{
    public partial class FrmCreateAccount : Form
    {
        private readonly IApiConnectionService _apiConnectionService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IButtonService _buttonService;

        public PlayerCredentialsDto NewPlayerCredentialsDto { get; private set; }

        public FrmCreateAccount(IApiConnectionService apiConnectionService, IMessageBoxService messageBoxService, IButtonService buttonService)
        {
            _apiConnectionService = apiConnectionService;
            _messageBoxService = messageBoxService;
            _buttonService = buttonService;

            InitializeComponent();
        }

        private void FrmCreateAccount_Load(object sender, EventArgs e)
        {
            TxbNewUsername.Clear();
            TxbNewPassword.Clear();
            TxbConfirmNewPassword.Clear();
        }

        private async void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            Button btnCreateAccount = (Button)sender;
            _buttonService.LockButtons(btnCreateAccount);

            string username = TxbNewUsername.Text;
            string password = TxbNewPassword.Text;
            string confirmPassword = TxbConfirmNewPassword.Text;

            bool passesMatch = password == confirmPassword;

            if (!passesMatch)
            {
                _messageBoxService.ShowInformationBox("Passwords must match!");
                _buttonService.UnlockButtons(btnCreateAccount);
                return;
            }

            NewPlayerCredentialsDto = new PlayerCredentialsDto
            {
                Username = username,
                Password = password,
            };

            var registerResponse = await _apiConnectionService.MakeRequestAsync("Players/register", RestSharp.Method.Post, NewPlayerCredentialsDto);

            if (!registerResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(registerResponse.ErrorMessage);
                _buttonService.UnlockButtons(btnCreateAccount);
                return;
            }

            _buttonService.UnlockButtons(btnCreateAccount);
            DialogResult = DialogResult.OK;
        }
    }
}

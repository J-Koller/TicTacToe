using System;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Services.Http.Request;
using TicTacToe.WinFormsApp.Services.Gui.Buttons;
using TicTacToe.WinFormsApp.Services.Gui.MessageBoxes;

namespace TicTacToe.WinFormsApp.Forms
{
    public partial class FrmSignIn : Form
    {
        private readonly FrmCreateAccount _frmCreateAccount;

        private readonly IApiConnectionService _apiConnectionService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IButtonService _buttonService;

        private readonly string _emptyCredentialsErrorMessage = "Please enter a username and password.";

        public int SignedInPlayerId { get; private set; }

        public FrmSignIn(FrmCreateAccount frmCreateAccount,
            IApiConnectionService apiConnectionService,
            IMessageBoxService messageBoxService,
            IButtonService buttonService)
        {
            _frmCreateAccount = frmCreateAccount;
            _apiConnectionService = apiConnectionService;
            _messageBoxService = messageBoxService;
            _buttonService = buttonService;

            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            SummonFormCreateAccount();
        }

        private void SummonFormCreateAccount()
        {
            DialogResult dialogResult = _frmCreateAccount.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                TxbUsername.Text = _frmCreateAccount.NewPlayerCredentialsDto.Username;
                TxbPassword.Text = _frmCreateAccount.NewPlayerCredentialsDto.Password;
            }
        }

        private async void BtnSignIn_Click(object sender, EventArgs e)
        {

            Button btnSignIn = (Button)sender;
            _buttonService.LockButtons(btnSignIn);

            bool credentialsAreEmpty = string.IsNullOrWhiteSpace(TxbUsername.Text) || string.IsNullOrWhiteSpace(TxbPassword.Text);

            if (credentialsAreEmpty)
            {
                _messageBoxService.ShowErrorBox(_emptyCredentialsErrorMessage);
                _buttonService.UnlockButtons(btnSignIn);
                return;
            }

            var playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = TxbUsername.Text,
                Password = TxbPassword.Text
            };

            var signInResponse = await _apiConnectionService.MakeRequestAsync<PlayerCredentialsDto, int>("Players/signin", RestSharp.Method.Post, playerCredentialsDto);

            if (!signInResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(signInResponse.ErrorMessage);
            }
            else
            {
                SignedInPlayerId = signInResponse.Dto;
                DialogResult = DialogResult.OK;
            }
            _buttonService.UnlockButtons(btnSignIn);
        }

        private void FrmSignIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }

        private void FrmSignIn_Load(object sender, EventArgs e)
        {
            TxbUsername.Clear();
            TxbPassword.Clear();
        }
    }
}

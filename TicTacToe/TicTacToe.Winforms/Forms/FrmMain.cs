using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Enums;
using TicTacToe.Api.Shared.Services.Http.Notification;
using TicTacToe.Api.Shared.Services.Http.Request;
using TicTacToe.WinFormsApp.Services.Gui.Boxes;
using TicTacToe.WinFormsApp.Services.Gui.Buttons;
using TicTacToe.WinFormsApp.Services.Gui.ControlCoordinates;
using TicTacToe.WinFormsApp.Services.Gui.Labels;
using TicTacToe.WinFormsApp.Services.Gui.MessageBoxes;
using Timer = System.Windows.Forms.Timer;

namespace TicTacToe.WinFormsApp.Forms
{
    public partial class FrmMain : Form
    {
        private readonly FrmSignIn _frmSignIn;

        private readonly IBoxService _boxService;
        private readonly IApiConnectionService _apiConnectionService;
        private readonly ILabelService _labelService;
        private readonly IControlCoordinatesService _controlCoordinatesService;
        private readonly INotificationService _notificationService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IButtonService _buttonService;

        private List<Button> _buttons { get; set; }

        private PlayerDto _currentPlayerDto { get; set; }
        private PlayerDto _opponentPlayerDto { get; set; }

        private int _currentGameId;

        private bool _isPlayersTurn;

        private bool _isPlayerOne;

        private bool _gameIsOver;

        private bool _lockButton;

        private Timer _playerHeartBeatTimer;


        public FrmMain(
            FrmSignIn frmSignIn,
            IApiConnectionService apiConnectionService,
            ILabelService labelService,
            IControlCoordinatesService controlCoordinatesService,
            IBoxService boxService,
            INotificationService notificationService,
            IMessageBoxService messageBoxService,
            IButtonService buttonService)
        {
            InitializeComponent();

            _frmSignIn = frmSignIn;
            _boxService = boxService;
            _labelService = labelService;
            _apiConnectionService = apiConnectionService;
            _controlCoordinatesService = controlCoordinatesService;
            _notificationService = notificationService;
            _messageBoxService = messageBoxService;
            _buttonService = buttonService;


            _buttons = new List<Button>
            {
                Btn00, Btn01, Btn02, Btn10, Btn11, Btn12, Btn20, Btn21, Btn22
            };

            Btn00.Tag = new Point(0, 0);
            Btn01.Tag = new Point(0, 1);
            Btn02.Tag = new Point(0, 2);
            Btn10.Tag = new Point(1, 0);
            Btn11.Tag = new Point(1, 1);
            Btn12.Tag = new Point(1, 2);
            Btn20.Tag = new Point(2, 0);
            Btn21.Tag = new Point(2, 1);
            Btn22.Tag = new Point(2, 2);

            _playerHeartBeatTimer = new Timer
            {
                Interval = (int)TimeSpan.FromSeconds(10).TotalMilliseconds
            };
            _playerHeartBeatTimer.Tick += async (sender, e) => await SendHeartBeat();

        }

        private async Task SendHeartBeat()
        {
            var heartBeatResponse = await _apiConnectionService.MakeRequestAsync($"Players/heartbeat/{_currentPlayerDto.Id}", RestSharp.Method.Post);

            if (!heartBeatResponse.ResponseSuccessful)
            {
                Log.Error("Error sending heartbeat.");
            }
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            _buttonService.LockButtons(BtnLogOut, BtnFindGame, BtnNewGame);
            ShowFormSignIn();

            if (_frmSignIn.DialogResult == DialogResult.OK)
            {
                await StartConnectionsAsync();

                _boxService.LockGameBoard(_buttons);

                var getPlayerResponse = await _apiConnectionService.MakeRequestAsync<PlayerDto>($"Players/getplayer/{_frmSignIn.SignedInPlayerId}", RestSharp.Method.Get);
                if (!getPlayerResponse.ResponseSuccessful)
                {
                    _messageBoxService.ShowErrorBox(getPlayerResponse.ErrorMessage);
                    return;
                }
                else
                {
                    _buttonService.UnlockButtons(BtnLogOut, BtnFindGame, BtnNewGame);
                }

                _currentPlayerDto = getPlayerResponse.Dto;
                _currentPlayerDto.GameHubConnectionId = _notificationService.UserGameConnectionId;

                Log.Information($"SignalR connection: {_notificationService.UserGameConnectionId}");
                FillPlayerData();

                _playerHeartBeatTimer.Start();
                await SendHeartBeat();
            }
        }

        private async void BtnLogOut_Click(object sender, EventArgs e)
        {
            var signOutResponse = await _apiConnectionService.MakeRequestAsync($"Players/signout/{_currentPlayerDto.Id}", RestSharp.Method.Post);
            if (!signOutResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(signOutResponse.ErrorMessage);
                return;
            }

            _playerHeartBeatTimer.Stop();
            await StopConnectionsAsync();
            WipePlayerData();
            _boxService.ClearGameBoard(_buttons);
            _labelService.ClearText(LblOpponentName, LblTurn);

            ShowFormSignIn();

            if (_frmSignIn.DialogResult == DialogResult.OK)
            {
                await StartConnectionsAsync();

                _boxService.LockGameBoard(_buttons);

                var getPlayerResponse = await _apiConnectionService.MakeRequestAsync<PlayerDto>($"Players/getplayer/{_frmSignIn.SignedInPlayerId}", RestSharp.Method.Get);
                if (!getPlayerResponse.ResponseSuccessful)
                {
                    _messageBoxService.ShowErrorBox(getPlayerResponse.ErrorMessage);
                    return;
                }

                _currentPlayerDto = getPlayerResponse.Dto;
                _currentPlayerDto.GameHubConnectionId = _notificationService.UserGameConnectionId;

                FillPlayerData();

                _playerHeartBeatTimer.Start();
                await SendHeartBeat();
            }
        }

        private async void BtnNewGame_Click(object sender, EventArgs e)
        {
            DialogResult newGameResult = _messageBoxService.ShowQuestionBox("Start new game?", "New Game?");
            if (newGameResult == DialogResult.No)
            {
                return;
            }

            ClearCache();

            _labelService.ClearText(LblOpponentName, LblTurn);

            var newGameDto = new NewGameDto
            {
                PlayerId = _currentPlayerDto.Id,
                GameHubConnectionId = _notificationService.UserGameConnectionId,
            };

            var newGameResponse = await _apiConnectionService.MakeRequestAsync<NewGameDto, int>("Games/newgame", RestSharp.Method.Post, newGameDto);
            if (!newGameResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(newGameResponse.ErrorMessage);
                return;
            }

            _currentGameId = newGameResponse.Dto;
            _isPlayersTurn = false;
            _isPlayerOne = true;

            _boxService.ClearGameBoard(_buttons);
            _boxService.UnlockGameBoard(_buttons);
            _buttonService.LockButtons(BtnFindGame, BtnNewGame, BtnLogOut);
            _labelService.ChangeTextColorToBlue(LblTurn, "Waiting on an opponent!");
        }

        private async void BtnFindGame_Click(object sender, EventArgs e)
        {
            if (_lockButton)
            {
                return;
            }

            _lockButton = true;
            ClearCache();

            _labelService.ClearText(LblOpponentName, LblTurn);

            var getAvailableGamesResponse = await _apiConnectionService.MakeRequestAsync<IEnumerable<AvailableGameDto>>("Games", RestSharp.Method.Get);
            if (!getAvailableGamesResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(getAvailableGamesResponse.ErrorMessage);
                return;
            }

            var availableGameDtos = getAvailableGamesResponse.Dto;

            if (availableGameDtos.Count() == 0)
            {
                _messageBoxService.ShowExclamatoryBox("No games available. Try making a new game!");
                _lockButton = false;
                return;
            }

            int gameId = availableGameDtos.FirstOrDefault().Id;
            _currentGameId = gameId;

            var joinGameDto = new JoinGameDto
            {
                PlayerId = _currentPlayerDto.Id,
                GameId = gameId,
                GameHubConnectionId = _notificationService.UserGameConnectionId
            };

            var joinGameResponse = await _apiConnectionService.MakeRequestAsync<JoinGameDto, PlayerDto>($"Games/joingame", RestSharp.Method.Post, joinGameDto);
            if (!joinGameResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(joinGameResponse.ErrorMessage);

                return;
            }

            _notificationService.OpponentGameConnectionId = joinGameResponse.Dto.GameHubConnectionId;

            await _notificationService.SendAsync(Hubs.Game, "PlayerJoin", _currentPlayerDto, _notificationService.OpponentGameConnectionId);

            _isPlayerOne = false;
            _isPlayersTurn = false;

            UpdateOpponent(joinGameResponse.Dto);

            _labelService.ChangeTextColorToBlue(LblTurn, $"{_opponentPlayerDto.Username}'s Turn");
            _boxService.ClearGameBoard(_buttons);
            _boxService.UnlockGameBoard(_buttons);
            _buttonService.LockButtons(BtnFindGame, BtnNewGame, BtnLogOut);
            _lockButton = false;

        }

        private async void Btn_Board_Click(object sender, EventArgs e)
        {
            if (!_isPlayersTurn)
            {
                return;
            }

            if (_gameIsOver)
            {
                return;
            }


            if (_lockButton)
            {
                return;
            }

            Button button = (Button)sender;

            bool gameBoxHasSymbol = !string.IsNullOrEmpty(button.Text);

            if (gameBoxHasSymbol)
            {
                return;
            }

            Point coordinates = _controlCoordinatesService.GetCoordinatesByControl(button);

            var newMoveDto = new NewMoveDto
            {
                PlayerId = _currentPlayerDto.Id,
                GameId = _currentGameId,
                PositionX = coordinates.X,
                PositionY = coordinates.Y
            };

            _lockButton = true;

            var makeMoveResponse = await _apiConnectionService.MakeRequestAsync<NewMoveDto, MoveDto>("Games/makemove", RestSharp.Method.Post, newMoveDto);

            if (!makeMoveResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(makeMoveResponse.ErrorMessage);
                _lockButton = false;
                return;
            }
            else
            {
                await _notificationService.SendAsync(Hubs.Game, "AddMove", makeMoveResponse.Dto, _notificationService.GameConnectionIds);

            }
        }

        private async void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            var signOutResponse = await _apiConnectionService.MakeRequestAsync($"Players/signout/{_currentPlayerDto.Id}", RestSharp.Method.Post);
            if (!signOutResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(signOutResponse.ErrorMessage);
                return;
            }

            bool gameInProgress = _currentGameId > 0;
            bool gameInProgressWithOpponent = _currentGameId > 0 && _opponentPlayerDto != null;

            if (!gameInProgress)
            {
                Environment.Exit(0);
            }

            var gameResultDto = new GameResultDto
            {
                GameId = _currentGameId
            };

            if (gameInProgressWithOpponent)
            {
                var dialogResult = _messageBoxService.ShowQuestionBox("You are in the middle of a game, are you sure you want to exit? Quitting will result in a loss.");

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                gameResultDto.WinnerId = _opponentPlayerDto.Id;
            }

            var abandonGameResponse = await _apiConnectionService.MakeRequestAsync("Games/abandongame", RestSharp.Method.Post, gameResultDto);
            if (!abandonGameResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(abandonGameResponse.ErrorMessage);
                return;
            }

            if (gameInProgressWithOpponent)
            {
                await _notificationService.SendAsync(Hubs.Game, "AbandonGame", _currentPlayerDto, _notificationService.OpponentGameConnectionId);
            }

            Environment.Exit(0);
        }

        private void OnPlayerJoin(PlayerDto opponentDto)
        {
            Log.Information($"Recieved opponent: {opponentDto.Username}. Opponent connectionId: {opponentDto.GameHubConnectionId}");

            UpdateOpponent(opponentDto);

            _notificationService.OpponentGameConnectionId = opponentDto.GameHubConnectionId;

            _isPlayersTurn = true;
            _labelService.ChangeTextColorToBlue(LblTurn, "Your Turn");
        }

        private void OnMove(MoveDto moveDto)
        {
            _lockButton = false;

            Log.Information($"Recieved move. Made by player: {moveDto.PlayerId}");

            _boxService.UpdateGameBoard(_buttons, moveDto);

            if (_gameIsOver)
            {
                return;
            }

            bool currentPlayerMadeMove = moveDto.PlayerId == _currentPlayerDto.Id;

            if (currentPlayerMadeMove)
            {
                if (moveDto.Symbol == "X")
                {
                    _labelService.ChangeTextColorToRed(LblTurn, $"{_opponentPlayerDto.Username}'s Turn");
                }
                else
                {
                    _labelService.ChangeTextColorToBlue(LblTurn, $"{_opponentPlayerDto.Username}'s Turn");
                }

                _isPlayersTurn = false;
            }
            else
            {
                if (moveDto.Symbol == "X")
                {
                    _labelService.ChangeTextColorToRed(LblTurn, "Your Turn");
                }
                else
                {
                    _labelService.ChangeTextColorToBlue(LblTurn, "Your Turn");
                }

                _isPlayersTurn = true;
            }

        }

        private async Task OngameEnd(GameResultDto gameResultDto)
        {   
            _gameIsOver = true;
            var getPlayerResponse = await _apiConnectionService.MakeRequestAsync<PlayerDto>($"Players/getplayer/{_currentPlayerDto.Id}", RestSharp.Method.Get);
            if (!getPlayerResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(getPlayerResponse.ErrorMessage);
            }

            _currentPlayerDto = getPlayerResponse.Dto;
            _currentPlayerDto.GameHubConnectionId = _notificationService.UserGameConnectionId;

            if (gameResultDto.IsTie)
            {
                if (_isPlayerOne)
                {
                    _labelService.ChangeTextColorToBlue(LblTurn, "Tie!");
                }
                else
                {
                    _labelService.ChangeTextColorToRed(LblTurn, "Tie!");
                }
            }

            bool playerIsWinner = gameResultDto.WinnerId == _currentPlayerDto.Id;

            if (playerIsWinner)
            {
                if (_isPlayerOne)
                {
                    _labelService.ChangeTextColorToBlue(LblTurn, "Winner!");
                }
                else
                {
                    _labelService.ChangeTextColorToRed(LblTurn, "Winner!");
                }
            }
            else if (!playerIsWinner && !gameResultDto.IsTie)
            {
                if (_isPlayerOne)
                {
                    _labelService.ChangeTextColorToBlue(LblTurn, "Loser!");
                }
                else
                {
                    _labelService.ChangeTextColorToRed(LblTurn, "Loser!");
                }
            }

            _buttonService.UnlockButtons(BtnFindGame, BtnNewGame, BtnLogOut);

            _currentGameId = 0;
            FillPlayerData();
        }

        private async Task OnQuitter(PlayerDto quittingPlayerDto)
        {
            Log.Information($"Recieved quitter: {quittingPlayerDto.Username}. Connection Id: {quittingPlayerDto.GameHubConnectionId}");

            var getPlayerResponse = await _apiConnectionService.MakeRequestAsync<PlayerDto>($"Players/getplayer/{_currentPlayerDto.Id}", RestSharp.Method.Get);
            if (!getPlayerResponse.ResponseSuccessful)
            {
                _messageBoxService.ShowErrorBox(getPlayerResponse.ErrorMessage);
            }

            _currentPlayerDto = getPlayerResponse.Dto;
            _currentPlayerDto.GameHubConnectionId = _notificationService.UserGameConnectionId;

            if (_isPlayerOne)
            {
                _labelService.ChangeTextColorToBlue(LblTurn, $"Winner! {quittingPlayerDto.Username} has quit the game.");
                _labelService.ChangeTextColorToRed(LblOpponentName, $"Opponent: {quittingPlayerDto.Username} | Rank: QUITTER!");
            }
            else
            {
                _labelService.ChangeTextColorToRed(LblTurn, $"Winner! {quittingPlayerDto.Username} has quit the game.");
                _labelService.ChangeTextColorToBlue(LblOpponentName, $"Opponent: {quittingPlayerDto.Username} | Rank: QUITTER!");
            }

            _buttonService.UnlockButtons(BtnFindGame, BtnNewGame, BtnLogOut);

            var gameResultDto = new GameResultDto
            {
                GameId = _currentGameId,
                WinnerId = _currentPlayerDto.Id
            };

            var abandonGameResponse = await _apiConnectionService.MakeRequestAsync("Games/abandongame", RestSharp.Method.Post, gameResultDto);


            ClearCache();
            _gameIsOver = true;
            FillPlayerData();
        }

        private void ShowFormSignIn()
        {
            DialogResult dialogResult = _frmSignIn.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
            {
                Close();
            }
        }

        private async Task StartConnectionsAsync()
        {
            await _notificationService.StartConnectionAsync();

            _notificationService.ConfigureOn<MoveDto>(Hubs.Game, "RecievedMove", OnMove);
            _notificationService.ConfigureOn<PlayerDto>(Hubs.Game, "RecievedPlayer", OnPlayerJoin);
            _notificationService.ConfigureOn<GameResultDto, Task>(Hubs.Game, "RecievedGame", OngameEnd);
            _notificationService.ConfigureOn<PlayerDto, Task>(Hubs.Game, "RecievedQuitter", OnQuitter);
        }

        private async Task StopConnectionsAsync()
        {
            await _notificationService.StopConnectionAsync();
        }

        private void UpdateOpponent(PlayerDto opponentDto)
        {
            _opponentPlayerDto = opponentDto;

            if (_isPlayerOne)
            {
                _labelService.ChangeTextColorToRed(LblOpponentName, $"Opponent: {opponentDto.Username} | Rank: {opponentDto.Rank}");
            }
            else
            {
                _labelService.ChangeTextColorToBlue(LblOpponentName, $"Opponent: {opponentDto.Username} | Rank: {opponentDto.Rank}");
            }
        }

        private void ClearCache()
        {
            _currentGameId = 0;

            _isPlayersTurn = false;

            _isPlayerOne = false;

            _gameIsOver = false;

            _opponentPlayerDto = null;
        }

        private void FillPlayerData()
        {
            string username = _currentPlayerDto.Username;
            string rank = _currentPlayerDto.Rank;
            string gamesPlayed = _currentPlayerDto.GamesPlayed.ToString();
            string gamesWon = _currentPlayerDto.GamesWon.ToString();
            string gamesLost = _currentPlayerDto.GamesLost.ToString();
            string gamesTied = _currentPlayerDto.GamesTied.ToString();

            _labelService.ChangeText(LblUsername, $"Username: {username}");
            _labelService.ChangeText(LblRank, $"Rank: {rank}");
            _labelService.ChangeText(LblGamesPlayed, $"Games Played: {gamesPlayed}");
            _labelService.ChangeText(LblGamesWon, $"Games Won: {gamesWon}");
            _labelService.ChangeText(LblGamesLost, $"Games Lost: {gamesLost}");
            _labelService.ChangeText(LblGamesTied, $"Games Tied: {gamesTied}");
        }

        private void WipePlayerData()
        {
            _labelService.ChangeText(LblUsername, $"Username: ");
            _labelService.ChangeText(LblRank, $"Rank: ");
            _labelService.ChangeText(LblGamesPlayed, $"Games Played: ");
            _labelService.ChangeText(LblGamesWon, $"Games Won: ");
            _labelService.ChangeText(LblGamesLost, $"Games Lost: ");
            _labelService.ChangeText(LblGamesTied, $"Games Tied: ");
        }
    }
}

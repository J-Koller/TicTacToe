import { Component, OnDestroy, OnInit, HostListener } from "@angular/core";
import { ActivatedRoute, ActivatedRouteSnapshot, NavigationEnd, Router } from "@angular/router";
import { Subscription, interval } from "rxjs";
import { AvailableGameDto } from "../data/dto/available-game-dto";
import { GameResultDto } from "../data/dto/game-result-dto";
import { MoveDto } from "../data/dto/move-dto";
import { PlayerDto } from "../data/dto/player-dto";
import { Hubs } from "../data/enums/hubs";
import { ApiCallDataResponse } from "../data/response/api-call-data-response";
import { ApiCallResponse } from "../data/response/api-call-response";
import { HttpService } from "../http/http.service";
import { SignalRService } from "../http/signalr.service";
import { GameBoardService } from "../services/game-board-service";
import { GameService } from "../services/game-service";
import { MenuService } from "../services/menu-service";
import { SignInService } from "../services/sign-in.service";

@Component({
    templateUrl: "./main.component.html",
    styleUrls: ["./main.component.css"]
})
export class MainComponent implements OnInit, OnDestroy {

    private heartbeatSubscription: Subscription | undefined;


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private apiConnectionService: HttpService,
        private notificationService: SignalRService,
        public gameBoardService: GameBoardService,
        public menuService: MenuService,
        public gameService: GameService,
        public signInService: SignInService
    ) {
        this.establishEventInstances();
        this.startSignalRConnections();
    }

    ngOnInit(): void {
        if (this.signInService.playerId == 0) {
            this.router.navigate(['signin']);
        }

        this.apiConnectionService.makeRequestWithoutBody<ApiCallDataResponse<PlayerDto>>(`Players/getplayer/${this.signInService.playerId}`, 'GET')
            .subscribe({
                next: (getPlayerResponse) => {
                    this.gameService.playerConnectionId = this.notificationService.userGameConnectionId;
                    this.gameService.updateCurrentPlayerDto(getPlayerResponse.dto);
                    this.menuService.fillPlayerData(getPlayerResponse.dto);
                    this.setupHeartbeatTimer();
                },
                error: (getPlayerError) => {
                    console.error('Error getting player: ', getPlayerError)
                    // this.gameBoardService.setErrorText("Unable to sign in. Please log out and try again.");
                    this.gameBoardService.setErrorText(getPlayerError.errorMessage);
                }
            });

        this.gameService.clearCache();
        this.gameBoardService.clearGameBoard();
        this.gameBoardService.disableGameBoard();
        this.menuService.enableMenuButtons();
    }

    onNewGameClick(): void {
        this.gameService.playerConnectionId = this.notificationService.userGameConnectionId;

        if (this.gameService.playerConnectionId == null) {
            this.gameBoardService.setErrorText("Unable to establish connection. Please log out and try again.");
            return;
        }
        this.gameService.clearCache();
        this.gameBoardService.clearGameBoard();
        this.gameBoardService.disableGameBoard();
        this.menuService.disableMenuButtons();

        let newGameDto = this.gameService.newGameDto();
        this.apiConnectionService.makeRequestWithBody<ApiCallDataResponse<number>>("Games/newgame", "POST", newGameDto)
            .subscribe({
                next: (newGameResponse) => {
                    this.gameService.setNewGameDataAsOwner(newGameResponse.dto);
                    this.gameBoardService.setWaitingOnOpponentText();
                },
                error: (newGameError) => {
                    console.error('Error starting new game: ', newGameError)
                    // this.gameBoardService.setErrorText("Unable to start a new game. Please try again in a few moments.");
                    this.gameBoardService.setErrorText(newGameError.errorMessage);
                    this.menuService.enableMenuButtons();
                }
            })
    }

    onFindGameClick(): void {
        this.gameService.playerConnectionId = this.notificationService.userGameConnectionId;

        if (this.gameService.playerConnectionId == null) {
            this.gameBoardService.setErrorText("Unable to establish connection. Please log out and try again.");
            return;
        }

        this.gameService.currentPlayerDto.gameHubConnectionId = this.gameService.playerConnectionId;
        this.gameService.clearCache();
        this.gameBoardService.clearGameBoard();
        this.gameBoardService.disableGameBoard();
        this.menuService.disableMenuButtons();

        this.apiConnectionService.makeRequestWithoutBody<ApiCallDataResponse<Array<AvailableGameDto>>>('Games', 'GET')
            .subscribe({
                next: (availableGamesResponse) => {
                    let availableGames: Array<AvailableGameDto> = availableGamesResponse.dto;

                    if (availableGames.length <= 0) {
                        this.gameBoardService.setNoAvailableGamesText();
                        this.menuService.enableMenuButtons();

                        return;
                    }

                    let joinGameDto = this.gameService.newJoinGameDto(availableGames[0].id);

                    this.apiConnectionService.makeRequestWithBody<ApiCallDataResponse<PlayerDto>>('Games/joingame', 'POST', joinGameDto)
                        .subscribe({
                            next: (joinGameResponse) => {
                                this.gameService.opponentConnectionId = joinGameResponse.dto.gameHubConnectionId;
                                this.updateOpponent(joinGameResponse.dto);
                                console.log(`Opponent connection id: ${this.gameService.opponentConnectionId}`);

                                this.notificationService.send<PlayerDto>(Hubs.Game, "PlayerJoin", this.gameService.currentPlayerDto, this.gameService.opponentConnectionId);

                                this.gameService.setNewGameDataAsJoiner(availableGames[0].id);
                                this.gameBoardService.setOpponentsTurnText();
                                this.gameBoardService.enableGameBoard();
                                this.menuService.disableMenuButtons();
                            },
                            error: (joinGameError) => {
                                console.error('Error joining the game:', joinGameError);
                                // this.gameBoardService.setErrorText("Unable to join game. Please try again in a few moments.");
                                this.gameBoardService.setErrorText(joinGameError.errorMessage);
                                this.menuService.enableMenuButtons();
                            }
                        });
                },
                error: (availableGamesError) => {
                    console.error('Error fetching available games:', availableGamesError);
                    // this.gameBoardService.setErrorText("Unable to find games. Please try again in a few moments.");
                    this.gameBoardService.setErrorText(availableGamesError.errorMessage);
                    this.menuService.enableMenuButtons();
                }
            });

    }

    onButtonClick(button: any): void {
        if (!this.gameService.validGridClick(button)) {
            return;
        }
        this.gameBoardService.disableGameBoard();
        let newMoveDto = this.gameService.newMoveDto(button.point.row, button.point.col);
        this.apiConnectionService.makeRequestWithBody<ApiCallDataResponse<MoveDto>>("Games/makemove", 'POST', newMoveDto)
            .subscribe({
                next: (makeMoveResponse) => {
                    this.notificationService.send<MoveDto>(Hubs.Game, 'AddMove', makeMoveResponse.dto, ...this.gameService.connectionIds);
                },
                error: (error) => {
                    console.error('Error making move:', error);
                    this.gameBoardService.setErrorText("Error making move");
                }
            });

    }

    onLogOutClick(): void {
        this.menuService.disableMenuButtons();
        this.notificationService.stopConnections();
        this.router.navigate(['signin']);
    }

    // not 100% sure if this is even triggered or working the way i wish
    @HostListener('window:beforeunload', ['$event'])
    async onBeforeUnload(event: any): Promise<void> {
        console.log("#unloaded");

        if (this.heartbeatSubscription) {
            this.heartbeatSubscription.unsubscribe();
        }

        let gameInProgress = this.gameService.currentGameId > 0;
        let gameInProgressWithOpponent = gameInProgress && this.gameService.hasOpponent;

        if (gameInProgress) {
            let gameResultDto = new GameResultDto();
            gameResultDto.gameId = this.gameService.currentGameId;

            if (gameInProgressWithOpponent) {
                gameResultDto.winnerId = this.gameService.opponentPlayerDto.id;

                this.notificationService.send(Hubs.Game, "AbandonGame", this.gameService.currentPlayerDto, this.gameService.opponentConnectionId);
            }
        }
    }


    ngOnDestroy(): void {
        if (this.signInService.playerId === 0) {
            return;
        }

        console.log("#destroyed");
        if (this.heartbeatSubscription) {
            this.heartbeatSubscription.unsubscribe();
        }

        let gameInProgress = this.gameService.currentGameId > 0;
        let gameInProgressWithOpponent = gameInProgress && this.gameService.hasOpponent;
        let gameResultDto = new GameResultDto();

        if (gameInProgress) {
            gameResultDto.gameId = this.gameService.currentGameId;

            if (gameInProgressWithOpponent) {
                gameResultDto.winnerId = this.gameService.opponentPlayerDto.id;
            }

            this.apiConnectionService.makeRequestWithBody<ApiCallResponse>("Games/abandongame", "POST", gameResultDto)
                .subscribe({
                    next: (abandonGameResponse) => {
                        console.log(`abandoned game!`);
                        if (gameInProgressWithOpponent) {
                            console.log(`sending signalR abandon game notification`);
                            this.notificationService.send(Hubs.Game, "AbandonGame", this.gameService.currentPlayerDto, this.gameService.opponentConnectionId);
                        }
                    },
                    error: (abandonGameError) => {
                        console.error('Error abandoning the game:', abandonGameError);
                    }
                });
        }

        this.apiConnectionService.makeRequestWithoutBody<ApiCallResponse>(`Players/signout/${this.gameService.currentPlayerDto.id}`, 'POST')
            .subscribe({
                next: () => {
                    console.log("signed out");
                },
                error: (signOutError) => {
                    console.error('Error signing out:', signOutError);
                }
            });

    }

    private onPlayerJoin(opponentDto: PlayerDto): void {
        try {
            console.log(`Recieved opponent: ${opponentDto}`);
            this.gameService.opponentConnectionId = opponentDto.gameHubConnectionId;
            console.log(`Opponent connection id: ${this.gameService.opponentConnectionId}`);
            this.gameService.isPlayersTurn = true;
            this.gameBoardService.setPlayersTurnText();
            this.gameBoardService.enableGameBoard();
            this.updateOpponent(opponentDto);
        }
        catch (err) {
            console.log(err);
        }
    }

    private onMove(moveDto: any): void {
        try {
            console.log(`Recieved move: ${JSON.stringify(moveDto)}`);
            this.gameBoardService.updateGameBoard(moveDto);
            this.gameBoardService.enableGameBoard();

            console.log("In on move and gameIsOver = ", this.gameService.gameIsOver);
            if (this.gameService.gameIsOver) {
                console.log("gonna return from onMove");
                return;
            }

            if (this.gameService.playerMadeMove(moveDto.playerId)) {
                this.gameBoardService.setOpponentsTurnText();
            }
            else {
                this.gameBoardService.setPlayersTurnText();
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    private onGameEnd(gameResultDto: GameResultDto): void {
        console.log("recieved game end")
        this.gameService.gameIsOver = true;
        console.log("set game over to true.")
        if (gameResultDto.isTie) {
            this.gameBoardService.setIsTieText();
        }

        let playerIsWinner = this.gameService.playerIsWinner(gameResultDto.winnerId);
        if (playerIsWinner) {
            if (!this.gameService.isPlayerOne) {
                this.gameBoardService.beRed = true;
            }
            this.gameBoardService.setIsWinnerText();
        }
        else if (!playerIsWinner && !gameResultDto.isTie) {
            this.gameBoardService.setIsLoserText();
        }

        this.menuService.enableMenuButtons();
        this.gameBoardService.disableGameBoard();
        this.gameService.clearCache();

        this.apiConnectionService.makeRequestWithoutBody<ApiCallDataResponse<PlayerDto>>(`Players/getplayer/${this.gameService.currentPlayerDto.id}`, 'GET')
            .subscribe({
                next: (getPlayerResponse) => {
                    this.gameService.updateCurrentPlayerDto(getPlayerResponse.dto);
                    this.menuService.fillPlayerData(getPlayerResponse.dto);
                },
                error: (getPlayerError) => {
                    console.error('Error fetching player data from onGameEnd:', getPlayerError);
                }
            });
    }

    private onQuitter(quittingPlayerDto: PlayerDto): void {
        try {
            this.apiConnectionService.makeRequestWithoutBody<ApiCallDataResponse<PlayerDto>>(`Players/getplayer/${this.gameService.currentPlayerDto.id}`, 'GET')
                .subscribe({
                    next: (getPlayerResponse) => {
                        this.gameService.updateCurrentPlayerDto(getPlayerResponse.dto);
                        this.menuService.fillPlayerData(getPlayerResponse.dto);
                        this.gameBoardService.setQuitterTexts(quittingPlayerDto.username);
                    },
                    error: (getPlayerError) => {
                        console.error('Error fetching player data from onQuitter:', getPlayerError);
                    }
                });


            let gameResultDto = new GameResultDto();
            gameResultDto.gameId = this.gameService.currentGameId;
            gameResultDto.winnerId = this.gameService.currentPlayerDto.id;

            this.apiConnectionService.makeRequestWithBody<ApiCallResponse>("Games/abandongame", "POST", gameResultDto)
                .subscribe({
                    next: () => {
                        this.gameService.clearCache();
                        this.menuService.enableMenuButtons();
                        this.gameBoardService.disableGameBoard();
                    },
                    error: (abandonGameError) => {
                        console.error('Error abandoning the game:', abandonGameError);
                    }
                });

        }
        catch (err) {
            console.log(err);
        }
    }

    private startSignalRConnections(): void {
        this.notificationService.startConnections();

        this.gameService.playerConnectionId = this.notificationService.userGameConnectionId;

        this.notificationService.configureOn<PlayerDto>(Hubs.Game, 'RecievedPlayer', this.onPlayerJoin);
        this.notificationService.configureOn<PlayerDto>(Hubs.Game, "RecievedQuitter", this.onQuitter);
        this.notificationService.configureOn<any>(Hubs.Game, 'RecievedMove', this.onMove);
        this.notificationService.configureOn<GameResultDto>(Hubs.Game, 'RecievedGame', this.onGameEnd);
    }

    private establishEventInstances(): void {
        this.onPlayerJoin = this.onPlayerJoin.bind(this);
        this.onQuitter = this.onQuitter.bind(this);
        this.onMove = this.onMove.bind(this);
        this.onGameEnd = this.onGameEnd.bind(this);
    }

    private setupHeartbeatTimer(): void {
        this.heartbeatSubscription = interval(10000).subscribe(() => {
            this.apiConnectionService
                .makeRequestWithoutBody<ApiCallResponse>(`Players/heartbeat/${this.gameService.currentPlayerDto.id}`, "POST")
                .subscribe();
        });
    }

    private updateOpponent(opponentDto: PlayerDto): void {
        this.gameService.updateOpponentDto(opponentDto);
        this.gameBoardService.setOpponentText();
    }
}
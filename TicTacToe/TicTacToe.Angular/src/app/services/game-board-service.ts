import { Injectable } from "@angular/core";
import { GameService } from "./game-service";

@Injectable({
    providedIn: 'root'
})
export class GameBoardService {

    opponentLabelText: string = '';
    turnLabelText: string = '';

    waitingOnOpponentText: string = "Waiting on an opponent!";
    noAvailableGamesText: string = "No available games. Try making a new game!";
    yourTurnText: string = "Your Turn";
    tieText: string = "Tie!";
    winnerText: string = "Winner!"
    loserText: string = "Loser!";

    beRed: boolean = true;
    red: string = "rgb(255,128,128)";
    blue: string = "rgb(128,255,255)";
    opponentLabelColor: string = '';

    buttons: Array<{ id: string, disabled: boolean, point: { row: number, col: number }, value: string }> = [];

    constructor(private gameService: GameService){
        this.initializeGameBoard();
    }

    //methods for grid

    public disableGameBoard(): void {
        this.buttons.forEach((button) => (button.disabled = true));
    }

    public enableGameBoard(): void {
        this.buttons.forEach((button) => (button.disabled = false));
    }

    public clearGameBoard(): void {
        this.opponentLabelText = '';
        this.turnLabelText = '';

        for (let button of this.buttons) {
            button.value = '';
        }
    }

    public updateGameBoard(moveDto: any) {
        let button = this.buttons.find(b => b.point.row === moveDto.coordinates.x && b.point.col === moveDto.coordinates.y);
        if (button) {
            button.value = moveDto.symbol;
        }
    }

    private initializeGameBoard(): void {
        for (let row = 0; row < 3; row++) {
            for (let col = 0; col < 3; col++) {
                const id = `Btn${row}${col}`;
                const point = { row, col };
                let value = '';
                this.buttons.push({ id, disabled: false, point, value });
            }
        }
    }

    // methods for changing text 

    public setWaitingOnOpponentText(): void {
        this.beRed = false;
        this.turnLabelText = this.waitingOnOpponentText;
    }

    public setNoAvailableGamesText(): void {
        this.beRed = true;
        this.turnLabelText = this.noAvailableGamesText;
    }

    public setErrorText(error: string): void {
        this.beRed = true;
        this.turnLabelText = error;
    }

    public setOpponentsTurnText(): void {
        this.setColorIfTurn();
        this.turnLabelText = `${this.gameService.opponentPlayerDto.username}'s Turn`;
    }

    public setOpponentText(): void {
        if(this.gameService.isPlayerOne){
            this.opponentLabelColor = this.red;
        }
        else{
            this.opponentLabelColor = this.blue;
        }
        this.opponentLabelText = `Opponent: ${this.gameService.opponentPlayerDto.username} | Rank: ${this.gameService.opponentPlayerDto.rank}`;
    }

    public setPlayersTurnText(): void {
        this.setColorIfPlayerOne();
        this.turnLabelText = this.yourTurnText;
    }

    public setIsTieText(): void {
        this.setColorIfPlayerOne();
        this.turnLabelText = this.tieText;
    }

    public setIsWinnerText(): void {
        this.setColorIfPlayerOne();
        this.turnLabelText = this.winnerText;
    }

    public setIsLoserText(): void {
        this.setColorIfPlayerOne();
        this.turnLabelText = this.loserText;
    }

    public setQuitterTexts(quitterName: string): void {
        this.turnLabelText = `Winner! ${quitterName} has quit the game.`;
        this.opponentLabelText = `Opponent: ${quitterName} | Rank: QUITTER!`
    }

    // color set

    private setColorIfPlayerOne(): void {
        if(this.gameService.isPlayerOne){
            this.beRed = false;
        }
        else{
            this.beRed = true;
        }
    }

    private setColorIfTurn(): void {
        if((this.gameService.isPlayerOne && this.gameService.isPlayersTurn) || (!this.gameService.isPlayerOne && !this.gameService.isPlayersTurn)){
            this.beRed = false;
        }
        else{
            this.beRed = true;
        }
    }

    getBorderColor(button: any): string {
        let blank = "1px solid #ccc";
        let red = "1px solid rgb(255,128,128)";
        let blue = '1px solid rgb(128,255,255)';

        if (button.value == 'X') {
            return blue;
        }
        else if (button.value == 'O') {
            return red;
        }

        return blank;
    }

}
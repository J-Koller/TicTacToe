import { Injectable } from "@angular/core";
import { PlayerDto } from "../data/dto/player-dto";
import { JoinGameDto } from "../data/dto/join-game-dto";
import { NewMoveDto } from "../data/dto/new-move-dto";
import { NewGameDto } from "../data/dto/new-game-dto";

@Injectable({
    providedIn: 'root'
})
export class GameService {

    currentPlayerDto!: PlayerDto;
    opponentPlayerDto!: PlayerDto

    playerConnectionId: string = '';
    opponentConnectionId: string = '';

    private _connectionIds: string[] = [];
    get connectionIds(): string[] {
        this._connectionIds = [this.playerConnectionId, this.opponentConnectionId];
        return this._connectionIds;
    }

    currentGameId: number = 0;

    isPlayerOne: boolean = false;
    isPlayersTurn: boolean = false;
    gameIsOver: boolean = true;
    playerWon: boolean = false;
    hasOpponent: boolean = false;

    public playerIsWinner(winnerId: number): boolean {
        return winnerId == this.currentPlayerDto.id;
    }

    public updateCurrentPlayerDto(playerDto: PlayerDto): void {
        this.currentPlayerDto = playerDto;
        this.currentPlayerDto.gameHubConnectionId = this.playerConnectionId;
    }

    public updateOpponentDto(opponentDto: PlayerDto): void {
        this.opponentPlayerDto = opponentDto;
        this.hasOpponent = true;
    }

    public setNewGameDataAsOwner(newGameId: number): void {
        this.isPlayersTurn = true;
        this.isPlayerOne = true;
        this.currentGameId = newGameId;
        this.gameIsOver = false;
    }

    public setNewGameDataAsJoiner(newGameId: number): void {
        this.currentGameId = newGameId;
        this.isPlayerOne = false;
        this.isPlayersTurn = false;
        this.gameIsOver = false;
    }

    public clearCache(): void {
        this.currentGameId = 0;
        this.isPlayersTurn = false;
        this.isPlayerOne = false;
        this.gameIsOver = true;
    }

    public newJoinGameDto(gameId: number): JoinGameDto {
        return new JoinGameDto(this.currentPlayerDto.id, gameId, this.playerConnectionId);
    }

    public newGameDto(): NewGameDto {
        return new NewGameDto(this.currentPlayerDto.id, this.playerConnectionId);
    }

    public newMoveDto(row: number, col: number): NewMoveDto{
        return new NewMoveDto(this.currentPlayerDto.id, this.currentGameId, row, col);
    }


    public playerMadeMove(movePlayerId: number): boolean {
        console.log(`Move player id: ${movePlayerId}`);
        console.log(`Player id: ${this.currentPlayerDto.id}`);
        if(movePlayerId == this.currentPlayerDto.id){
            this.isPlayersTurn = false;
            return true;
        }
        this.isPlayersTurn = true;
        return false;
    }

    public validGridClick(button: any): boolean {
        if (!this.isPlayersTurn) {
            return false;
        }

        if (this.gameIsOver) {
            return false;
        }

        if (button.value != '') {
            return false;
        }

        return true;
    }
}
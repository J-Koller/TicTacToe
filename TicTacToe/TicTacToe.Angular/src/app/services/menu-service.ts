import { Injectable } from "@angular/core";
import { PlayerDto } from "../data/dto/player-dto";

@Injectable({
    providedIn: 'root'
})
export class MenuService {
    menuButtonsDisabled = false;

    username: string = '';
    rank: string = '';
    gamesPlayed: string = '';
    gamesWon: string = '';
    gamesTied: string = '';
    gamesLost: string = '';

    public fillPlayerData(currentPlayerDto: PlayerDto): void {
        this.username = currentPlayerDto.username;
        this.rank = currentPlayerDto.rank;
        this.gamesPlayed = currentPlayerDto.gamesPlayed.toString();
        this.gamesWon = currentPlayerDto.gamesWon.toString();
        this.gamesLost = currentPlayerDto.gamesLost.toString();
        this.gamesTied = currentPlayerDto.gamesTied.toString();
    }

    public disableMenuButtons(): void {
        this.menuButtonsDisabled = true;
    }

    public enableMenuButtons(): void {
        this.menuButtonsDisabled = false;
    }
}
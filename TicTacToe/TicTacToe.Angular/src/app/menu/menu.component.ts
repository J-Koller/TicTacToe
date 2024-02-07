import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { PlayerDto } from "../data/dto/player-dto";
import { NewGameDto } from "../data/dto/new-game-dto";
import { GameService } from "../services/game-service";
import { HttpService } from "../http/http.service";
import { ApiCallDataResponse } from "../data/response/api-call-data-response";

@Component({
    selector: "app-menu",
    templateUrl: "./menu.component.html",
    styleUrls: ["./menu.component.css"]
})
export class MenuComponent implements OnChanges {

    @Input() currentPlayerDto!: PlayerDto;
    userGameHubConnectionId: string = '';

    username: string = '';
    rank: string = '';
    gamesPlayed: string = '';
    gamesWon: string = '';
    gamesTied: string = '';
    gamesLost: string = '';

    constructor(private gameService: GameService, private apiConnectionService: HttpService){}

    ngOnChanges(changes: SimpleChanges): void {
        if(changes["currentPlayerDto"]){
            this.fillPlayerData();
        }
    }

    private fillPlayerData(): void {
        this.username = this.currentPlayerDto.username;
        this.rank = this.currentPlayerDto.rank;
        this.gamesPlayed = this.currentPlayerDto.gamesPlayed.toString();
        this.gamesWon = this.currentPlayerDto.gamesWon.toString();
        this.gamesLost = this.currentPlayerDto.gamesLost.toString();
        this.gamesTied = this.currentPlayerDto.gamesTied.toString();
    }
}    
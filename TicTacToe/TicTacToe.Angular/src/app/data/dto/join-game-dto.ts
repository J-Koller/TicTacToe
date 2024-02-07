export class JoinGameDto {
    PlayerId: number = 0;
    GameId: number = 0;
    GameHubConnectionId: string = '';

    constructor(playerId: number, gameId: number, connectionId: string){
        this.PlayerId = playerId;
        this.GameId = gameId;
        this.GameHubConnectionId = connectionId;
    }
}
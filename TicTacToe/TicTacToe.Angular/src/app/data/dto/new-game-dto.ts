export class NewGameDto {
    PlayerId: number = 0;
    GameHubConnectionId: string = '';

    constructor(PlayerId: number, GameHubConnectionId: string){
        this.PlayerId = PlayerId;
        this.GameHubConnectionId = GameHubConnectionId;
    }
}
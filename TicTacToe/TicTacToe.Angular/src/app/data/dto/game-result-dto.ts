export class GameResultDto {
    gameId: number = 0;
    isTie: boolean = false;
    winnerId: number = 0;
    gameConnectionIds: Array<string> = [];

    constructor(gameId?: number, isTie?: boolean, winnerId?: number, gameConnectionIds?: Array<string>) {
        this.gameId = gameId ?? this.gameId;
        this.isTie = isTie ?? this.isTie;
        this.winnerId = winnerId ?? this.winnerId;
        this.gameConnectionIds = gameConnectionIds ?? this.gameConnectionIds;
    }
}

export class NewMoveDto{
    PlayerId: number = 0;
    GameId: number = 0;
    PositionX: number = 0;
    PositionY: number =0;

    constructor(PlayerId: number, GameId: number, PositionX: number, PositionY: number){
        this.PlayerId = PlayerId;
        this.GameId = GameId;
        this.PositionX = PositionX;
        this.PositionY = PositionY;
    }
}
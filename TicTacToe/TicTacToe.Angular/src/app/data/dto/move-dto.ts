export class MoveDto {
    symbol: string = '';
    playerId: number = 0;
    coordinates: { X: number; Y: number } = {X: 0, Y: 0};

    static fromCSharpMoveDto(csharpMoveDto: any): MoveDto {
        // Assuming csharpMoveDto.coordinates is of type Point in C#
        const transformedDto = new MoveDto();
        transformedDto.symbol = csharpMoveDto.symbol;
        transformedDto.playerId = csharpMoveDto.playerId;
        transformedDto.coordinates.X = csharpMoveDto.coordinates.X;
        transformedDto.coordinates.Y = csharpMoveDto.coordinates.Y;
        return transformedDto;
    }
  }
    

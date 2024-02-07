using TicTacToe.Data.Entities.Base;

namespace TicTacToe.Data.Entities
{
    public class Move : EntityBase
    {
        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public virtual int GameId { get; set; }
        public virtual Game Game { get; set; }

        public virtual int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int SymbolId { get; set; }
        public Symbol Symbol { get; set; }
    }
}

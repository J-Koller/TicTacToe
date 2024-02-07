using System;
using System.Collections.Generic;
using TicTacToe.Data.Entities.Base;

namespace TicTacToe.Data.Entities
{
    public class Game : EntityBase
    {
        public virtual ICollection<Move> Moves { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual int? WinnerId { get; set; }
        public virtual Player Winner { get; set; }

        public virtual int? LoserId { get; set; }
        public virtual Player Loser { get; set; }

        public int PlayerOneId { get; set; }

        public int PlayerTwoId { get; set; }

        //public bool IsTie => !WinnerId.HasValue && !LoserId.HasValue;

        public bool IsTied { get; set; }

        public bool IsLookingForPlayers { get; set; }

        public bool IsPlayerOneTurn { get; set; }

        public string PlayerOneGameConnectionId { get; set; }

        public string PlayerTwoGameConnectionId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public Game()
        {
            Moves = new HashSet<Move>();
            Players = new HashSet<Player>();
        }
    }
}

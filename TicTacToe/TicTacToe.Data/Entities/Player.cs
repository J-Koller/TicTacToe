using System;
using System.Collections.Generic;
using TicTacToe.Data.Entities.Base;

namespace TicTacToe.Data.Entities
{
    public class Player : EntityBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Rank { get; set; }

        public bool IsLookingForGame { get; set; }

        public bool IsLoggedIn { get; set; }

        public DateTime LastActive { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Player()
        {
            Games = new HashSet<Game>();
        }
    }
}

using System;

namespace TicTacToe.Data.Entities.Base
{
    public class EntityBase
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}

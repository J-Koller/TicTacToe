using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.Entities;

namespace TicTacToe.Data.Configuration
{
    public class TicTacToeContext : DbContext
    {
        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Move> Moves { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Symbol> Symbols { get; set; }

        public TicTacToeContext(DbContextOptions<TicTacToeContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tic_tac_toe");

            modelBuilder.Entity<Player>(player =>
            {
                player.ToTable("Players");
                player.HasKey(p => p.Id);
                player.Property(p => p.CreationDate);

                player.Property(p => p.Username).IsRequired();
                player.Property(p => p.Password).IsRequired();
                player.Property(p => p.Rank);
                player.Property(p => p.IsLookingForGame);
                player.Property(p => p.IsLoggedIn);
                player.Property(p => p.LastActive);

                player.HasMany(p => p.Games).WithMany(g => g.Players).UsingEntity(j => j.ToTable("GamesPlayers"));
            });

            modelBuilder.Entity<Game>(game =>
            {
                game.ToTable("Games");
                game.HasKey(g => g.Id);
                game.Property(p => p.CreationDate);

                game.Property(g => g.IsLookingForPlayers);
                game.Property(g => g.IsPlayerOneTurn);
                game.Property(g => g.StartDate);
                game.Property(g => g.FinishDate);
                game.Property(g => g.PlayerOneId);
                game.Property(g => g.PlayerTwoId);
                game.Property(g => g.PlayerOneGameConnectionId);
                game.Property(g => g.PlayerTwoGameConnectionId);
                game.Property(g => g.IsTied);

                game.HasMany(g => g.Moves)
                    .WithOne(m => m.Game)
                    .HasForeignKey(m => m.GameId)
                    .OnDelete(DeleteBehavior.NoAction);

                game.HasOne(g => g.Winner)
                    .WithMany()
                    .HasForeignKey(g => g.WinnerId)
                    .OnDelete(DeleteBehavior.NoAction);

                game.HasOne(g => g.Loser)
                    .WithMany()
                    .HasForeignKey(g => g.LoserId)
                    .OnDelete(DeleteBehavior.NoAction);

                game.Navigation(g => g.Moves).AutoInclude();
                game.Navigation(g => g.Players).AutoInclude();
            });

            modelBuilder.Entity<Move>(move =>
            {
                move.ToTable("Moves");
                move.HasKey(m => m.Id);
                move.Property(p => p.CreationDate);

                move.Property(m => m.PositionX);
                move.Property(m => m.PositionY);

                move.HasOne(m => m.Game)
                    .WithMany(g => g.Moves)
                    .HasForeignKey(m => m.GameId)
                    .OnDelete(DeleteBehavior.NoAction);

                move.HasOne(m => m.Player)
                    .WithMany()
                    .HasForeignKey(m => m.PlayerId)
                    .OnDelete(DeleteBehavior.NoAction);

                move.HasOne(m => m.Symbol)
                    .WithMany()
                    .HasForeignKey(m => m.SymbolId)
                    .OnDelete(DeleteBehavior.NoAction);

                move.Navigation(m => m.Symbol).AutoInclude();
                move.Navigation(m => m.Player).AutoInclude();
                move.Navigation(m => m.Game).AutoInclude();
            });

            modelBuilder.Entity<Symbol>(symbol =>
            {
                symbol.ToTable("Symbols");
                symbol.HasKey(s => s.Id);
                symbol.Property(p => p.CreationDate);

                symbol.Property(s => s.Value)
                    .IsRequired()
                    .HasMaxLength(1);
            });
        }
    }
}

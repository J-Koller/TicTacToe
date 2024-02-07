using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities.Base;

namespace TicTacToe.Data.Repositories.Base
{
    public class DatabaseRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly TicTacToeContext _ticTacToeAppContext;

        public DatabaseRepositoryBase(TicTacToeContext ticTacToeAppContext)
        {
            _ticTacToeAppContext = ticTacToeAppContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _ticTacToeAppContext.Set<TEntity>().ToListAsync();

            return entities;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var entityEntry = await _ticTacToeAppContext.Set<TEntity>().AddAsync(entity);

            await _ticTacToeAppContext.SaveChangesAsync();

            int entityId = entityEntry.Entity.Id;
            return entityId;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _ticTacToeAppContext.Set<TEntity>().Remove(entity);

            await _ticTacToeAppContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ticTacToeAppContext.SaveChangesAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicTacToe.Data.Repositories.Base
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<int> AddAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}

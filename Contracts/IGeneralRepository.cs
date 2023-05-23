using API_Web.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Web.Contracts;

public interface IGeneralRepository<TEntity>
{
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(Guid guid);
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);

    TEntity? GetByDate(DateTime date);
}

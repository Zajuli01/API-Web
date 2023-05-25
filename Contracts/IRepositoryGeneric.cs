namespace API_Web.Contracts;

public interface IRepositoryGeneric<TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);
    TEntity? Create(TEntity entity);
    public bool Update(TEntity entity);
    bool Delete(Guid guid);
}

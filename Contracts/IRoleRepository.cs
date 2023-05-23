using API_Web.Model;

namespace API_Web.Contracts;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role Create(Role role);
    bool Update(Role role);
    bool Delete(Guid guid);
    IEnumerable<Role> GetAll();
    Role? GetByGuid(Guid guid);
}

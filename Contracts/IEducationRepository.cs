using API_Web.Model;

namespace API_Web.Contracts;

public interface IEducationRepository : IGeneralRepository<Education>
{
    Education Create(Education education);
    bool Update(Education education);
    bool Delete(Guid guid);
    IEnumerable<Education> GetAll();
    Education? GetByGuid(Guid guid);
}

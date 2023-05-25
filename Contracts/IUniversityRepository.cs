using API_Web.Model;

namespace API_Web.Contracts;

public interface IUniversityRepository : IGeneralRepository<University>
{
    University Create(University university);
    bool Update(University university);
    bool Delete(Guid guid);
    IEnumerable<University> GetAll();
    University? GetByGuid(Guid guid);

    University CreateWithValidate(University university);
}

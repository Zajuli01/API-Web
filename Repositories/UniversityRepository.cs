using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
{
    private readonly BookingManagementDBContext _context;
    public UniversityRepository(BookingManagementDBContext context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name));
    }

    public University CreateWithValidate(University university)
    {
        try
        {
            var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
            var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

            if (existingUniversityWithCode != null & existingUniversityWithName != null)
            {
                university.Guid = existingUniversityWithCode.Guid;

                _context.SaveChanges();

            }
            else
            {
                _context.Universities.Add(university);
                _context.SaveChanges();
            }

            return university;

        }
        catch
        {
            return null;
        }
    }

}

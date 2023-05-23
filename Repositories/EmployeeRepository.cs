using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.Employees;

namespace API_Web.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    private readonly BookingManagementDBContext _context;

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUniversityRepository _universityRepository;
    public EmployeeRepository(BookingManagementDBContext context): base(context)
    {
        _context = context;
    }
    public IEnumerable<Employee> GetByGuidWithEducation(Guid guid)
    {
        try
        {
            var employee = GetByGuid(guid);
            if (employee is null)
            {
                return null;
            }

            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return null;
            }

            var university = _universityRepository.GetByGuid(education.UniversityGuid);
            if (university is null)
            {
                return null;
            }

            var data = new
            {
                NIK = employee.NIK,
                Fullname = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education.GPA,
                UniversityName = university.Name
            };

            return data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    //    public EmpEdu EmpEdu {
    //        var data = new
    //            {
    //                NIK = employee.NIK,
    //                Fullname = employee.FirstName + " " + employee.LastName,
    //                BirthDate = employee.BirthDate,
    //                Gender = employee.Gender.ToString(),
    //                HiringDate = employee.HiringDate,
    //                Email = employee.Email,
    //                PhoneNumber = employee.PhoneNumber,
    //                education.Major,
    //                education.Degree,
    //                education.GPA,
    //                University = university.Name
    //};}
}
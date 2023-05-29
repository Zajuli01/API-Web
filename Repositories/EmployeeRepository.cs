using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.Employees;

namespace API_Web.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUniversityRepository _universityRepository;
    public EmployeeRepository(BookingManagementDBContext context,
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository) : base(context)
    {
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    public bool CheckEmailAndPhoneAndNIK(string emailOrPhoneOrNIK)
    {
        // Perform the necessary logic to check if the email or phone number already exists in your repository
        // Return true if it exists, false otherwise
        // Example implementation:
        // Assuming you have a collection or database table for employees,
        // you can check if the email or phone number exists in the collection or table.
        // Replace the code below with your actual implementation.

        
        return _context.Employees.Any(e => e.Email == emailOrPhoneOrNIK || e.PhoneNumber == emailOrPhoneOrNIK || e.NIK == emailOrPhoneOrNIK);

    }


    public int CreateWithValidate(Employee employee)
    {
        try
        {
            var isEmployeeExist = _context.Employees.ToList();
            if (isEmployeeExist.Count == 0)
            {
                return 3;
            }

            bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
            if (ExistsByEmail)
            {
                return 1;
            }

            bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
            if (ExistsByPhoneNumber)
            {
                return 2;
            }

            Create(employee);
            return 3;

        }
        catch
        {
            return 0;
        }
    }

    public Guid? FindGuidByEmail(string email)
    {
        try
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
            if (employee == null)
            {
                return null;
            }
            return employee.Guid;
        }
        catch
        {
            return null;
        }
    }


    public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
    {
        var employees = GetAll();
        var educations = _educationRepository.GetAll();
        var universities = _universityRepository.GetAll();

        var employeeEducations = new List<MasterEmployeeVM>();

        foreach (var employee in employees)
        {
            var education = educations.FirstOrDefault(e => e.Guid == employee.Guid);
            var university = universities.FirstOrDefault(u => u.Guid == education.UniversityGuid);

            var employeeEducation = new MasterEmployeeVM
            {
                Guid = employee.Guid,
                NIK = employee.NIK.ToString(),
                FullName = employee.FirstName + " " + employee.LastName,
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

            employeeEducations.Add(employeeEducation);
        }

        return employeeEducations;
    }


    MasterEmployeeVM? IEmployeeRepository.GetMasterEmployeeByGuid(Guid guid)
    {
        var employee = GetByGuid(guid);
        var education = _educationRepository.GetByGuid(guid);
        var university = _universityRepository.GetByGuid(education.UniversityGuid);

        var data = new MasterEmployeeVM
        {
            Guid = employee.Guid,
            NIK = employee.NIK.ToString(),
            FullName = employee.FirstName + " " + employee.LastName,
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

    public object GetEmployeeAll(Guid guid)
    {
        var employee = GetByGuid(guid);
        var education = _educationRepository.GetByGuid(guid);
        var university = _universityRepository.GetByGuid(education.UniversityGuid);

        var data = new
        {
            Guid = employee.Guid,
            NIK = employee.NIK.ToString(),
            Fullname = employee.FirstName + " " + employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender.ToString(),
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.GPA,
            University = university.Name
        };

        return data;
    }

    public Employee GetByEmail(string email)
    {
        var emp = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);
            return emp;
    }


    //public MasterEmployeeVM? GetAllByGuid(Guid guid)
    //{
    //    var emp = GetByGuid(guid);
    //    var edu = _educationRepository.GetByGuid(guid);
    //    var uni = _universityRepository.GetByGuid(edu.UniversityGuid);

    //    var data = new MasterEmployeeVM
    //    {
    //        guid = emp.,
    //        NIK  = emp.NIK,
    //        FullName = emp.FirstName

    //    };
    //}


    //public IEnumerable<Employee> GetByGuidWithEducation(Guid guid)
    //{
    //    try
    //    {
    //        var employee = GetByGuid(guid);
    //        if (employee is null)
    //        {
    //            return null;
    //        }

    //        var education = _educationRepository.GetByGuid(guid);
    //        if (education is null)
    //        {
    //            return null;
    //        }

    //        var university = _universityRepository.GetByGuid(education.UniversityGuid);
    //        if (university is null)
    //        {
    //            return null;
    //        }

    //        var data = new
    //        {
    //            NIK = employee.NIK,
    //            Fullname = employee.FirstName + " " + employee.LastName,
    //            BirthDate = employee.BirthDate,
    //            Gender = employee.Gender.ToString(),
    //            HiringDate = employee.HiringDate,
    //            Email = employee.Email,
    //            PhoneNumber = employee.PhoneNumber,
    //            Major = education.Major,
    //            Degree = education.Degree,
    //            GPA = education.GPA,
    //            UniversityName = university.Name
    //        };

    //        return data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

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
using API_Web.Contracts;
using API_Web.Model;
using API_Web.Repositories;
using API_Web.ViewModels.Accounts;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Educations;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Rooms;
using API_Web.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IMapper<University, UniversityVM> _universityVMMapper;
    private readonly IMapper<Employee, EmployeeVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationVMMapper;
    private readonly IMapper<Account, AccountVM> _accountVMMapper;
    private readonly IMapper<Booking, BookingVM> _bookingVMMapper;

    public EmployeeController(IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository,
        IAccountRepository accountRepository,
        IBookingRepository bookingRepository,
        IUniversityRepository universityRepository,
        IMapper<Employee, EmployeeVM> mapper,
        IMapper<Education, EducationVM> educationVMMapper,
        IMapper<Account, AccountVM> accountVMMapper,
        IMapper<Booking, BookingVM> bookingVMMapper,
        IMapper<University, UniversityVM> universityVMMapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _bookingVMMapper = bookingVMMapper;
        _accountRepository = accountRepository;
        _accountVMMapper = accountVMMapper;
        _bookingRepository = bookingRepository;
        _educationRepository = educationRepository;
        _educationVMMapper = educationVMMapper;
        _universityRepository = universityRepository;
        _universityVMMapper = universityVMMapper;
    }
    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAllMasterEmployee()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound();
        }

        return Ok(masterEmployees);
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid()
    {
        var masterEmployees = _employeeRepository.GetAll();
        if (!masterEmployees.Any())
        {
            return NotFound();
        }

        return Ok(masterEmployees);
    }


    //[HttpGet("WithEducation")]
    //public IActionResult GetAllWithEducation()
    //{
    //var edu = _educationRepository.GetAll();
    //if (edu.Any())
    //{
    //    return NotFound();
    //}
    //var emp = _employeeRepository.GetAll();
    //if (emp.Any())
    //{
    //    return NotFound();
    //}
    //var univ = _universityRepository.GetAll();
    //if (univ.Any())
    //{
    //    return NotFound();
    //}

    //    var educationGet = edu;
    //    var employeeGet = emp;
    //    var universityGet = univ;

    //    var getAll = from e in employeeGet
    //                 join ed in educationGet on e.Guid equals ed.Guid
    //                 join uni in universityGet on ed.UniversityGuid equals uni.Guid
    //                 select new
    //                 {
    //                     GUID = e.Guid,
    //                     NIK = e.NIK,
    //                     Fullname = e.FirstName + " " + e.LastName,
    //                     Birtdate = e.BirthDate,
    //                     Gender = e.Gender,
    //                     HiringDate = e.HiringDate,
    //                     Email = e.Email,
    //                     PhoneNumber = e.PhoneNumber,
    //                     Major = ed.Major,
    //                     Degree = ed.Degree,
    //                     GPA = ed.GPA,
    //                     University = uni.Name
    //                 };

    //    var response = getAll.ToList();

    //    return Ok(response);
    //}



    [HttpGet("GetAllByGuid")]
    public IActionResult GetByGuidWithEducation(Guid guid)
    {
        try
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound();
            }

            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return NotFound();
            }

            var university = _universityRepository.GetByGuid(education.UniversityGuid);
            if (university is null)
            {
                return NotFound();
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
                education.Major,
                education.Degree,
                education.GPA,
                University = university.Name
            };

            return Ok(data);

        }
        catch (Exception)
        {
            throw;
        }
    }

    //[HttpGet("GetByGuidWithEducation/{guid}")]
    //public IActionResult GetByGuidWithEducation2(Guid guid)
    //{
    //    var employeeData = _employeeRepository.GetByGuidWithEducation(guid);
    //    if (employeeData is null)
    //    {
    //        return NotFound();
    //    }

    //    return Ok(employeeData);
    //}



    [HttpGet]
    public IActionResult GetAll()
    {
        var employee = _employeeRepository.GetAll();
        if (!employee.Any())
        {
            return NotFound();
        }
        var collection = new
        {
            Employees = employee.Select(_mapper.Map).ToList()
        };
        return Ok(collection);

    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound();
        }
        var result = _mapper.Map(employee);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var resultConverted = _mapper.Map(employeeVM);
        var result = _employeeRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var resultConverted = _mapper.Map(employeeVM);
        var isUpdated = _employeeRepository.Update(resultConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

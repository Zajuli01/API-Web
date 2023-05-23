using API_Web.Utility;

namespace API_Web.ViewModels.Employees;

public class EmployeeVM
{
    public Guid? Guid { get; set; }
    public char NIK { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

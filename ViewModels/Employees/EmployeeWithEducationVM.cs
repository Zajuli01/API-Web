using API_Web.Model;
using API_Web.Utility;

namespace API_Web.ViewModels.Employees;

public class EmployeeWithEducationVM
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

    public Education? Education { get; set; }
    public Account? Account { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
}

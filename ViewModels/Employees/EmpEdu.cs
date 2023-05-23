using API_Web.Utility;

namespace API_Web.ViewModels.Employees;

public class EmpEdu
{
    public Guid guid {  get; set; }
    public string NIK { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set;}
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public string UniversityName { get; set; }

}

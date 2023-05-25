using API_Web.Model;
using API_Web.ViewModels.Employees;

namespace API_Web.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    Employee Create(Employee employee);
    bool Update(Employee employee);
    bool Delete(Guid guid);
    IEnumerable<Employee> GetAll();
    Employee? GetByGuid(Guid guid);

    //IEnumerable<MasterEmployeeVM> GetAllByGuid(Guid guid);

    IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();

        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        object GetEmployeeAll(Guid guid);

    public Guid? FindGuidByEmail(string email);

    int CreateWithValidate(Employee employee);

}

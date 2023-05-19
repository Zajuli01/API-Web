using API_Web.Model;

namespace API_Web.Contracts;

public interface IEmployeeRepository
{
    Employee Create(Employee employee);
    bool Update(Employee employee);
    bool Delete(Guid guid);
    IEnumerable<Employee> GetAll();
    Employee? GetByGuid(Guid guid);
}

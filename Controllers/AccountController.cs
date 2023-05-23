using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.AccountRoles;
using API_Web.ViewModels.Accounts;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IMapper<Employee, EmployeeVM> _employeeVMMapper;
    private readonly IMapper<AccountRole, AccountRoleVM> _accountRoleVMMapper;

    public AccountController(IAccountRepository accountRepository, 
        IEmployeeRepository employeeRepository, 
        IAccountRoleRepository accountRoleRepository, 
        IMapper<Account, AccountVM> mapper,
        IMapper<Employee, EmployeeVM> employeeVMMapper,
        IMapper<AccountRole, AccountRoleVM> accountRoleVMMapper)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
        _employeeVMMapper = employeeVMMapper;
        _accountRoleVMMapper = accountRoleVMMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var account = _accountRepository.GetAll();
        if (!account.Any())
        {
            return NotFound();
        }
        var result = account.Select(_mapper.Map).ToList();

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(account);

        return Ok(account);
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var resultConverted = _mapper.Map(accountVM);
        var result = _accountRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var resultConverted = _mapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(resultConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.AccountRoles;
using API_Web.ViewModels.Accounts;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
    private readonly IMapper<Account, AccountVM> _accountVMMapper;
    private readonly IMapper<Role, RoleVM> _roleVMMapper;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository,
        IAccountRepository accountRepository,
        IRoleRepository roleRepository,
        IMapper<AccountRole, AccountRoleVM> mapper,
        IMapper<Account, AccountVM> accountVMMapper,
        IMapper<Role, RoleVM> roleVMMapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _accountVMMapper = accountVMMapper;
        _roleVMMapper = roleVMMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountRole = _accountRoleRepository.GetAll();
        if (!accountRole.Any())
        {
            return NotFound();
        }
        var result = accountRole.Select(_mapper.Map).ToList();

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(accountRole);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountRoleVM)
    {
        var resultConverted = _mapper.Map(accountRoleVM);
        var result = _accountRoleRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountRoleVM)
    {
        var resultConverted = _mapper.Map(accountRoleVM);
        var isUpdated = _accountRoleRepository.Update(resultConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

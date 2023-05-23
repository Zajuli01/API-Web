using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.AccountRoles;
using API_Web.ViewModels.Educations;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<Role, RoleVM> _mapper;
    private readonly IMapper<AccountRole, AccountRoleVM> _accountRoleVMMapper;
    public RoleController(IRoleRepository roleRepository,
        IAccountRoleRepository accountRoleRepository,
        IMapper<Role, RoleVM> mapper,
        IMapper<AccountRole, AccountRoleVM> accountRoleVMMapper)
    {
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
        _accountRoleVMMapper = accountRoleVMMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var role = _roleRepository.GetAll();
        if (!role.Any())
        {
            return NotFound();
        }
        var result = role.Select(_mapper.Map).ToList();

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound();
        }
        var result = _mapper.Map(role);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(RoleVM roleVM)
    {
        var resultConverted = _mapper.Map(roleVM);
        var result = _roleRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(RoleVM roleVM)
    {
        var resultConverted = _mapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(resultConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

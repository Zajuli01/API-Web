using API_Web.Contracts;
using API_Web.Model;
using API_Web.Utility;
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

    [HttpPost("login")]
    public IActionResult Login(LoginVM loginVM)
    {
        var account = _accountRepository.Login(loginVM);

        if (account == null)
        {
            return NotFound("Account not found");
        }

        if (account.Password != loginVM.Password)
        {
            return BadRequest("Password is invalid");
        }

        return Ok("Login Successfully");
    }




    [HttpPost("Register")]

    public IActionResult Register(RegisterVM registerVM)
    {

        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest("Registration failed");
            case 1:
                return BadRequest("Email already exists");
            case 2:
                return BadRequest("Phone number already exists");
            case 3:
                return Ok("Registration success");
        }

        return Ok();

    }

    [HttpPost("ForgotPassword" + "{email}")]
    public IActionResult UpdateResetPass(String email)
    {

        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            return NotFound("Akun tidak ditemukan");
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest();
            default:
                var hasil = new AccountResetPasswordVM
                {
                    Email = email,
                    OTP = isUpdated
                };

                MailService mailService = new MailService();
                mailService.WithSubject("Kode OTP")
                           .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                                     "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                           .WithEmail(email)
                           .Send();

                return Ok(hasil);

        }
    }



    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
    {
        // Cek apakah email dan OTP valid
        var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
        var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
        switch (changePass)
        {
            case 0:
                return BadRequest("");
            case 1:
                return Ok("Password has been changed successfully");
            case 2:
                return BadRequest("Invalid OTP");
            case 3:
                return BadRequest("OTP has already been used");
            case 4:
                return BadRequest("OTP expired");
            case 5:
                return BadRequest("Wrong Password No Same");
            default:
                return BadRequest();
        }
        return null;

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

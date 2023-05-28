﻿using API_Web.Contracts;
using API_Web.Model;
using API_Web.Others;
using API_Web.Utility;
using API_Web.ViewModels.AccountRoles;
using API_Web.ViewModels.Accounts;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static API_Web.Utility.EmailService;

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
    private readonly EmailService _emailService;



    public AccountController(IAccountRepository accountRepository,
        IEmployeeRepository employeeRepository,
        IAccountRoleRepository accountRoleRepository,
        IMapper<Account, AccountVM> mapper,
        IMapper<Employee, EmployeeVM> employeeVMMapper,
        IMapper<AccountRole, AccountRoleVM> accountRoleVMMapper,
        EmailService emailService)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
        _employeeVMMapper = employeeVMMapper;
        _accountRoleVMMapper = accountRoleVMMapper;
        _emailService = emailService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginVM loginVM)
    {
        var respons = new ResponseVM<LoginVM>();
        try
        {
            var account = _accountRepository.Login(loginVM);

            if (account == null)
            {
                return NotFound(respons.NotFound(account));
            }

            var currentlyHash = Hashing.HashPassword(loginVM.Password);
            var validatePassword = Hashing.ValidatePassword(loginVM.Password, currentlyHash);
            //if (account.Password != loginVM.Password)
            if (!validatePassword)
            {
                var message = "Password is invalid";
                return NotFound(respons.NotFound(message));
            }

            return Ok(respons.Success(account));
        }
        catch (Exception ex)
        {

            return BadRequest(respons.Error(ex.Message));

        }
    }




    [HttpPost("Register")]

    public IActionResult Register(RegisterVM registerVM)
    {

        var respons = new ResponseVM<RegisterVM>();
        try
        {
            var result = _accountRepository.Register(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(respons.NotFound("Registration failed"));
                case 1:
                    return BadRequest(respons.NotFound("Email already exists"));
                case 2:
                    return BadRequest(respons.NotFound("Phone number already exists"));
                case 3:
                    return Ok(respons.Success("Registration success"));
            }

            return Ok(respons.Success("Berhasil"));
        }
        catch (Exception ex)
        {
            return BadRequest(respons.Error(ex.Message));
        }

    }

    [HttpPost("ForgotPassword" + "{email}")]
    public IActionResult UpdateResetPass(String email)
    {

        var respons = new ResponseVM<AccountResetPasswordVM>();
        try
        {
            var getGuid = _employeeRepository.FindGuidByEmail(email);
            if (getGuid == null)
            {
                var message = "Akun tidak ditemukan";
                return NotFound(respons.NotFound(message));
            }

            var isUpdated = _accountRepository.UpdateOTP(getGuid);

            switch (isUpdated)
            {
                case 0:
                    var message = "OTP belum di update";
                    return BadRequest(respons.NotFound(message));

                default:
                    var hasil = new AccountResetPasswordVM
                    {
                        Email = email,
                        OTP = isUpdated
                    };

                    EmailService _emailService = new EmailService("localhost", 25, "no-reply@mcc.com");
                    //EmailService _emailService = new EmailService();
                    _emailService.SetEmail(hasil.Email)
                        .SetSubject("Forgot Password")
                        .SetHtmlMessage($"Your OTP is {hasil.OTP}")
                        .SentEmailAsynch();

                    //MailService mailService = new MailService();
                    //mailService.WithSubject("Kode OTP")
                    //           .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                    //                     "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                    //           .WithEmail(email)
                    //           .Send();

                    return Ok(respons.Success(hasil));

            }
        }
        catch (Exception ex)
        {
            return BadRequest(respons.Error(ex.Message));
        }
    }



    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
    {
        // Cek apakah email dan OTP valid
        var respons = new ResponseVM<ChangePasswordVM>();
        try
        {
            // Cek apakah email dan OTP valid
            var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest(respons.Error("Runtime error"));
                case 1:
                    return Ok(respons.Success("Password has been changed successfully"));
                case 2:
                    return NotFound(respons.Error("Invalid OTP"));
                case 3:
                    return NotFound(respons.Error("OTP has already been used"));
                case 4:
                    return NotFound(respons.Error("OTP expired"));
                case 5:
                    return BadRequest(respons.Error("Wrong Password No Same"));
                default:
                    return BadRequest(respons.Error("Runtime error"));
            }

        }
        catch (Exception ex)
        {
            return BadRequest(respons.Error(ex.Message));
        }

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

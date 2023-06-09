﻿using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;
using API_Web.Others;
using API_Web.Utility;
using API_Web.ViewModels.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API_Web.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly ITokenService _tokenService;


    public AccountRepository(
        BookingManagementDBContext context,
        IUniversityRepository universityRepository,
        IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository) : base(context)
    {
        _universityRepository = universityRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
    }




    public LoginVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _employeeRepository.GetAll();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new LoginVM
                    {
                        Email = emp.Email,
                        Password = acc.Password

                    };
        return query.FirstOrDefault();
    }

    int IAccountRepository.Register(RegisterVM registerVM)
    {
        try
        {
            var university = new University
            {
                Code = registerVM.Code,
                Name = registerVM.Name

            };
            _universityRepository.CreateWithValidate(university);

            var employee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            var result = _employeeRepository.Create(employee);

            //if (result != 3)
            //{
            //    return result;
            //}

            var education = new Education
            {
                Guid = employee.Guid,
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityGuid = university.Guid
            };
            _educationRepository.Create(education);

            //hashing password
            registerVM.Password = Hashing.HashPassword(registerVM.Password);
            var account = new Account
            {
                Guid = employee.Guid,
                Password = registerVM.Password,
                IsDeleted = false,
                IsUsed = true,
                OTP = 0
            };

            Create(account);
            var accountRole = new AccountRole
            {
                RoleGuid = Guid.Parse("9e8b7346-0bdb-4dda-2072-08db60bf1afd"),
                AccountGuid = employee.Guid
            };
        _context.AccountRoles.Add(accountRole);
        _context.SaveChanges();

            return 3;

        }
        catch
        {
            return 0;
        }
    }

    private string GenerateNIK()
    {
        var lastNik = _employeeRepository.GetAll().OrderByDescending(e => int.Parse(e.NIK.ToString())).FirstOrDefault();

        if (lastNik != null)
        {
            int lastNikNumber;
            if (int.TryParse(lastNik.NIK.ToString(), out lastNikNumber))
            {
                return (lastNikNumber + 1).ToString();
            }
        }

        return "100000";
    }




    public int UpdateOTP(Guid? employeeId)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        //Generate OTP
        Random rnd = new Random();
        var getOtp = rnd.Next(100000, 999999);
        account.OTP = getOtp;

        //Add 5 minutes to expired time
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        try
        {
            var check = Update(account);


            if (!check)
            {
                return 0;
            }
            return getOtp;
        }
        catch
        {
            return 0;
        }
    }

    public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        if (account == null || account.OTP != changePasswordVM.Otp)
        {
            return 2;
        }
        // Cek apakah OTP sudah digunakan
        if (account.IsUsed)
        {
            return 3;
        }
        // Cek apakah OTP sudah expired
        if (account.ExpiredTime < DateTime.Now)
        {
            return 4;
        }
        // Cek apakah NewPassword dan ConfirmPassword sesuai
        if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
        {
            return 5;
        }
        // Update password
        account.Password = changePasswordVM.NewPassword;
        account.IsUsed = true;
        try
        {
            var updatePassword = Update(account);
            if (!updatePassword)
            {
                return 0;
            }
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public IEnumerable<string> GetRoles(Guid guid)
    {
        var getAccount = GetByGuid(guid);
        if (getAccount == null) return Enumerable.Empty<string>();
    // Implement other methods of the account repository interface
        var roles = new List<string>();

        // Perform the join operation using LINQ
        var joinedRoles = from role in _context.Roles
                          join accountRole in _context.AccountRoles
                              on role.Guid equals accountRole.RoleGuid
                          where accountRole.AccountGuid == guid
                          select role.Name;

        roles.AddRange(joinedRoles);

        return roles;
        
    }
}

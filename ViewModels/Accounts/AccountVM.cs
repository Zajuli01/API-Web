using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web.ViewModels.Accounts;

using System;
using System.ComponentModel.DataAnnotations;

public class AccountVM
{
    [Display(Name = "Guid")]
    public Guid? Guid { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Is Deleted")]
    public bool IsDeleted { get; set; }

    [Display(Name = "OTP")]
    public int OTP { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; }

    [Display(Name = "Expired Time")]
    public DateTime ExpiredTime { get; set; }
}


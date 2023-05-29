using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web.ViewModels.AccountRoles;

using System;
using System.ComponentModel.DataAnnotations;

public class AccountRoleVM
{
    [Display(Name = "Guid")]
    public Guid? Guid { get; set; }

    [Required(ErrorMessage = "AccountGuid is required")]
    [Display(Name = "Account Guid")]
    public Guid AccountGuid { get; set; }

    [Required(ErrorMessage = "RoleGuid is required")]
    [Display(Name = "Role Guid")]
    public Guid RoleGuid { get; set; }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web.ViewModels.AccountRoles;

public class AccountRoleVM
{
    public Guid? Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }
}

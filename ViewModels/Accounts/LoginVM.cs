﻿using System.ComponentModel.DataAnnotations;

namespace API_Web.ViewModels.Accounts;

public class LoginVM
{
    public string Email { get; set; }

    public string Password { get; set; }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SoftNET_Project.Models
{
    public class Role : IdentityRole
    {
        public string RoleName { get; set; }
    }
}

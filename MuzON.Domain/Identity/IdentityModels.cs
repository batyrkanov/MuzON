﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Identity
{
    public class IdentityModels
    {
        // Login model
        public class CustomUserLogin : IdentityUserLogin<Guid>
        {
            [Key]
            public Guid Id { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        // Extend Identity classes to specify a Guid for the key
        public class CustomUserRole : IdentityUserRole<Guid>
        {
            [Key]
            public Guid Id { get; set; }
        }

        public class CustomUserClaim : IdentityUserClaim<Guid> { }

        public class CustomRole : IdentityRole<Guid, CustomUserRole>
        {
            public CustomRole()
            {
                Id = Guid.NewGuid();
            }
            public CustomRole(string name)
            {
                Id = Guid.NewGuid();
                Name = name;
            }
        }

        // Responsible for managing instances of the roles
        public class ApplicationRoleManager : RoleManager<CustomRole, Guid>
        {
            public ApplicationRoleManager(IRoleStore<CustomRole, Guid> roleStore)
                : base(roleStore)
            { }
        }

        // Responsible for managing instances of the user class
        public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
                : base(store)
            { }
        }
    }
}

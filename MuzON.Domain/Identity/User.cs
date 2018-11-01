using Microsoft.AspNet.Identity.EntityFramework;
using MuzON.Domain.Entities;
using MuzON.Domain.Identity;
using System;
using System.Collections.Generic;

namespace MuzON.Domain.Identity
{
    public class User : IdentityUser<Guid, UserLogin, UserRole,
    UserClaim>
    {
        public User()
        {
            Id = Guid.NewGuid();
            Comments = new List<Comment>();
            Ratings = new List<Rating>();
        }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EFEntities.Base
{
    public class AuditableUserEntity : IdentityUser
    {
        public AuditableUserEntity()
        {
            InverseInsertUser = new HashSet<Users>();
            InverseUpdateUser = new HashSet<Users>();
        }

        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }

        public virtual ICollection<Users> InverseInsertUser { get; set; }
        public virtual ICollection<Users> InverseUpdateUser { get; set; }
    }
}

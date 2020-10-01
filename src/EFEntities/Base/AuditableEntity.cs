using System;

namespace EFEntities.Base
{
    public class AuditableEntity : BaseEntity
    {
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }

        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }
    }
}

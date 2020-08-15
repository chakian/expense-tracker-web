using Microsoft.AspNetCore.Identity;

namespace EFEntities
{
    public partial class AspNetUserRoles : IdentityUserRole<string>
    {
        public virtual AspNetRoles Role { get; set; }
        public virtual Users User { get; set; }
    }
}

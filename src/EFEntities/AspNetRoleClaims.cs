using Microsoft.AspNetCore.Identity;

namespace EFEntities
{
    public partial class AspNetRoleClaims : IdentityRoleClaim<string>
    {
        public virtual AspNetRoles Role { get; set; }
    }
}

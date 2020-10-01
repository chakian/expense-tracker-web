using Microsoft.AspNetCore.Identity;

namespace EFEntities
{
    public partial class AspNetUserClaims : IdentityUserClaim<string>
    {
        public virtual Users User { get; set; }
    }
}

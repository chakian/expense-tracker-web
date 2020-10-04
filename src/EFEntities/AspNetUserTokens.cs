using Microsoft.AspNetCore.Identity;

namespace EFEntities
{
    public partial class AspNetUserTokens : IdentityUserToken<string>
    {
        public virtual Users User { get; set; }
    }
}

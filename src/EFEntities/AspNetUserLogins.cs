using Microsoft.AspNetCore.Identity;

namespace EFEntities
{
    public partial class AspNetUserLogins : IdentityUserLogin<string>
    {
        public virtual Users User { get; set; }
    }
}

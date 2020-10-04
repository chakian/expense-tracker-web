using EFEntities.Base;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class AccountTypes : BaseEntity
    {
        public AccountTypes()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int AccountTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}

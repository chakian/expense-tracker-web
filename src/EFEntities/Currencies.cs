using EFEntities.Base;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class Currencies : BaseEntity
    {
        public Currencies()
        {
            Budgets = new HashSet<Budgets>();
        }

        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string LongName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<Budgets> Budgets { get; set; }
    }
}

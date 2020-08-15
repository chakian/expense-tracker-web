using System.Collections.Generic;

namespace EFEntities
{
    public partial class Currencies
    {
        public Currencies()
        {
            Budgets = new HashSet<Budgets>();
        }

        public int CurrencyId { get; set; }
        public bool IsActive { get; set; }
        public string CurrencyCode { get; set; }
        public string LongName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<Budgets> Budgets { get; set; }
    }
}

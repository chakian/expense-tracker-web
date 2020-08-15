using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class UserInternalTokens
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Issuer { get; set; }
        public string CreatingIp { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime LastUsedDate { get; set; }
        public bool IsValid { get; set; }
        public string TokenString { get; set; }
        public string Device { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; }
        public string Zip { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}

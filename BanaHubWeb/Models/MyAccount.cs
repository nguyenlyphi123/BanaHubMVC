using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class MyAccount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Images { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalOrder { get; set; }
        public long TotalValue { get; set; }
    }
}
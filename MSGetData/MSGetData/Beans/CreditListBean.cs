using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSGetData.Beans
{
    public class CreditListBean
    {
        public int IdCredit { get; set; }
        public decimal AmountTotal { get; set; }
        public decimal PayTotal { get; set; }
        public decimal ResidueTotal { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int IdTypeCredit { get; set; }
        public string Description { get; set; }
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }
}

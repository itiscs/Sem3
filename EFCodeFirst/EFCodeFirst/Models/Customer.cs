using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Models
{
    public class Customer
    {
        [Key]
        public int Cnum { get; set; }
        public String Cname { get; set; }
        public String City { get; set; }
        public int Rating { get; set; }

        public  Salesperson Salesperson { get; set; }
        public List<Order> Orders { get; set; }

    }
}

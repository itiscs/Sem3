using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst.Models
{
    [Table("Salespeople")]
    public class Salesperson
    {
        [Key]
        public int Snum { get; set; }
        [MaxLength(30)]
        public String Sname { get; set; }
        [MaxLength(20)]
        public String City { get; set; }
        public decimal Comm { get; set; }

        public List<Order> Orders { get; set; }

    }
}

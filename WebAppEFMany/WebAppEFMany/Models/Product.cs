using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppEFMany.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(5)]
        public string Unit { get; set; }
        [Range(0,5000)]
        public decimal Price { get; set; }
      
        public int Count { get; set; }

        public List<Order> Orders { get; set; }


    }
}
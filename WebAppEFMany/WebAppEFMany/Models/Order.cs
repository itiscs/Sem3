using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppEFMany.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Дата заказа")]
        public DateTime Order_date { get; set; }
        public int Count { get; set; }
        public int Amount { get; set; }
        
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }


    }
}
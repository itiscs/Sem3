namespace EFCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ord_date { get; set; }

        public int? kolvo { get; set; }

        public decimal? amt { get; set; }

        public int id_prod { get; set; }

       public int id_cust { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}

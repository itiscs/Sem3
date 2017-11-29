namespace WpfAdo.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .Property(e => e.phone)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customers)
                .HasForeignKey(e => e.id_cust);

            modelBuilder.Entity<Orders>()
                .Property(e => e.amt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Products>()
                .Property(e => e.price)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.id_prod)
                .WillCascadeOnDelete(false);
        }
    }
}

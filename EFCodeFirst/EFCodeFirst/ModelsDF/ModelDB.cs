namespace EFCodeFirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB() : base("name=ModelDB")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.phone)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.id_cust);

            modelBuilder.Entity<Order>()
                .Property(e => e.amt)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.id_prod)
                .WillCascadeOnDelete(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppEFMany.Models
{
    public class OrdersDBInitializer:DropCreateDatabaseAlways<OrdersDB>
    {

        protected override void Seed(OrdersDB context)
        {
            context.Customers.Add(new Customer()
            {
                Name = "Покупатель 1",
                Address = "Кремлевская",
                City = "Казань",
                Phone = 11111111m
            });
            context.Customers.Add(new Customer()
            {
                Name = "Покупатель 2",
                Address = "Кремль",
                City = "Москва",
                Phone = 222222222m
            });
            context.Products.Add(new Product() { Name = "Яблоки", Unit = "кг", Price = 60m, Count = 1000 });
            context.Products.Add(new Product() { Name = "Груши", Unit = "кг", Price = 120m, Count = 500 });
            context.SaveChanges();

            context.Orders.Add(new Order()
            {
                Order_date = Convert.ToDateTime("01/01/2018"),
                Count = 10,
                Amount = 600,
                Customer = context.Customers.FirstOrDefault((c) => c.Name == "Покупатель 1"),
                Product = context.Products.FirstOrDefault((c) => c.Name == "Яблоки"),
            });
            context.Orders.Add(new Order()
            {
                Order_date = Convert.ToDateTime("02/02/2018"),
                Count = 20,
                Amount = 1000,
                CustomerID = context.Customers.FirstOrDefault((c) => c.Name == "Покупатель 2").Id,
                ProductID = context.Products.FirstOrDefault((c) => c.Name == "Груши").Id,
            });
            context.SaveChanges();
            base.Seed(context);
        }



    }

    public class OrdersDB:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        public OrdersDB()
        {
            //Database.SetInitializer<OrdersDB>(new OrdersDBInitializer());
        
        }
        

    }
}
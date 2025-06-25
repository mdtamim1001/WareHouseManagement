namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.FirstDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Models.FirstDbContext context)
        {
            // Avoid duplicate seed data
            if (context.Managers.Any()) return;

            // Add a demo Manager
            var manager = new Manager
            {
                FullName = "Tamim Yousuf",
                Email = "tamim@example.com",
                PasswordHash = "hashedpassword123"
            };
            context.Managers.Add(manager);
            context.SaveChanges();

            // Add Products
            var product1 = new Product
            {
                ProductName = "Laptop",
                SKU = "LT-001",
                Quantity = 20,
                ImportDate = DateTime.Now.AddDays(-5),
                ExpireDate = DateTime.Now.AddYears(1),
                Addedby = manager.Id
            };

            var product2 = new Product
            {
                ProductName = "Smartphone",
                SKU = "SP-002",
                Quantity = 50,
                ImportDate = DateTime.Now.AddDays(-10),
                ExpireDate = DateTime.Now.AddYears(1),
                Addedby = manager.Id
            };

            context.Products.AddOrUpdate(p => p.SKU, product1, product2);
            context.SaveChanges();

            // Add Section
            var section = new Section
            {
                Name = "Electronics A1",
                MaxQuantity = 100,
                Quantity = 70
            };
            context.Sections.Add(section);
            context.SaveChanges();

            // Add SectionProducts
            var sectionProduct1 = new SectionProducts
            {
                SectionID = section.Id,
                ProductID = product1.Id,
                Quantity = 10
            };

            var sectionProduct2 = new SectionProducts
            {
                SectionID = section.Id,
                ProductID = product2.Id,
                Quantity = 20
            };
            context.SectionProducts.AddOrUpdate(sp => new { sp.SectionID, sp.ProductID }, sectionProduct1, sectionProduct2);
            context.SaveChanges();

            // Add Shipment
            var shipment = new Shipment
            {
                Name = "Delivery to Dhaka",
                Destination = "Dhaka",
                Direction = "Out",
                Quantity = 25,
                ReleaseDate = DateTime.Now,
                ReachDate = DateTime.Now.AddDays(3),
                Status = "In Transit",
                Addedby = manager.Id
            };
            context.Shipments.Add(shipment);
            context.SaveChanges();

            // Add ShipmentProducts
            var shipmentProduct1 = new ShipmentProduct
            {
                ShipmentId = shipment.Id,
                ProductId = product1.Id,
                Quantity = 5
            };

            var shipmentProduct2 = new ShipmentProduct
            {
                ShipmentId = shipment.Id,
                ProductId = product2.Id,
                Quantity = 10
            };

            context.ShipmentProducts.AddOrUpdate(sp => new { sp.ShipmentId, sp.ProductId }, shipmentProduct1, shipmentProduct2);
            context.SaveChanges();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Models
{
    public class FirstDbContext :DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionProducts> SectionProducts { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentProduct> ShipmentProducts { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove pluralizing convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Disable cascade delete to avoid multiple cascade path issues
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.Addedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>()
                .HasRequired(s => s.Manager)
                .WithMany()
                .HasForeignKey(s => s.Addedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SectionProducts>()
                .HasRequired(sp => sp.Section)
                .WithMany(s => s.SectionProducts)
                .HasForeignKey(sp => sp.SectionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SectionProducts>()
                .HasRequired(sp => sp.Product)
                .WithMany(p => p.SectionProducts)
                .HasForeignKey(sp => sp.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShipmentProduct>()
                .HasRequired(sp => sp.Shipment)
                .WithMany(s => s.ShipmentProducts)
                .HasForeignKey(sp => sp.ShipmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShipmentProduct>()
                .HasRequired(sp => sp.Product)
                .WithMany(p => p.ShipmentProducts)
                .HasForeignKey(sp => sp.ProductId)
                .WillCascadeOnDelete(false);
        }
    }


}


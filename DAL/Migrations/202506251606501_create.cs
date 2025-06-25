namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        SKU = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ImportDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        Addedby = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manager", t => t.Addedby)
                .Index(t => t.Addedby);
            
            CreateTable(
                "dbo.SectionProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.Section", t => t.SectionID)
                .Index(t => t.SectionID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MaxQuantity = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShipmentProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShipmentId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Shipment", t => t.ShipmentId)
                .Index(t => t.ShipmentId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Shipment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        Direction = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        ReachDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        Addedby = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manager", t => t.Addedby)
                .Index(t => t.Addedby);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipmentProduct", "ShipmentId", "dbo.Shipment");
            DropForeignKey("dbo.Shipment", "Addedby", "dbo.Manager");
            DropForeignKey("dbo.ShipmentProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SectionProducts", "SectionID", "dbo.Section");
            DropForeignKey("dbo.SectionProducts", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "Addedby", "dbo.Manager");
            DropIndex("dbo.Shipment", new[] { "Addedby" });
            DropIndex("dbo.ShipmentProduct", new[] { "ProductId" });
            DropIndex("dbo.ShipmentProduct", new[] { "ShipmentId" });
            DropIndex("dbo.SectionProducts", new[] { "ProductID" });
            DropIndex("dbo.SectionProducts", new[] { "SectionID" });
            DropIndex("dbo.Product", new[] { "Addedby" });
            DropTable("dbo.Shipment");
            DropTable("dbo.ShipmentProduct");
            DropTable("dbo.Section");
            DropTable("dbo.SectionProducts");
            DropTable("dbo.Product");
            DropTable("dbo.Manager");
        }
    }
}

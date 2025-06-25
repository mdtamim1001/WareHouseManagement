namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class token : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TKey = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "ManagerId", "dbo.Manager");
            DropIndex("dbo.Token", new[] { "ManagerId" });
            DropTable("dbo.Token");
        }
    }
}

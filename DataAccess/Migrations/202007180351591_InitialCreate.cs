namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Blog_Headline = c.String(nullable: false, maxLength: 255),
                        Blog_Description = c.String(unicode: false, storeType: "text"),
                        Blog_CreatedFrom = c.String(nullable: false, maxLength: 50),
                        Blog_CreatedAt = c.DateTime(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Blogs", new[] { "Category_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.Blogs");
        }
    }
}

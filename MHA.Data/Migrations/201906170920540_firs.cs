namespace MHA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        EmailAddress = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        OnModifiedTime = c.DateTime(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        OnModifiedTime = c.DateTime(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Catagory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        URL = c.String(),
                        ParentId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        OnModifiedTime = c.DateTime(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsHeader = c.String(),
                        NewsSmallContent = c.String(),
                        NewsContent = c.String(),
                        ReadedCount = c.Int(nullable: false),
                        Image = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        OnModifiedTime = c.DateTime(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                        AppUser_Id = c.Int(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AppUser_Id)
                .ForeignKey("dbo.Catagory", t => t.Category_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsImage = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        OnModifiedTime = c.DateTime(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                        News_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.News_Id)
                .Index(t => t.News_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "News_Id", "dbo.News");
            DropForeignKey("dbo.News", "Category_Id", "dbo.Catagory");
            DropForeignKey("dbo.News", "AppUser_Id", "dbo.User");
            DropForeignKey("dbo.Catagory", "AppUser_Id", "dbo.User");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropIndex("dbo.Images", new[] { "News_Id" });
            DropIndex("dbo.News", new[] { "Category_Id" });
            DropIndex("dbo.News", new[] { "AppUser_Id" });
            DropIndex("dbo.Catagory", new[] { "AppUser_Id" });
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropTable("dbo.Images");
            DropTable("dbo.News");
            DropTable("dbo.Catagory");
            DropTable("dbo.Role");
            DropTable("dbo.User");
        }
    }
}

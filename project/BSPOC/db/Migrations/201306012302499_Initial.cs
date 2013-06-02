namespace db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Trusted = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Release = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Book_ID = c.Int(),
                        User_ID = c.Int(),
                        Author_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Authors", t => t.Author_ID)
                .Index(t => t.Book_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Author_ID);
            
            CreateTable(
                "dbo.BookShelves",
                c => new
                    {
                        Book_ID = c.Int(nullable: false),
                        Shelf_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Shelf_ID })
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("dbo.Shelves", t => t.Shelf_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Shelf_ID);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_ID = c.Int(nullable: false),
                        Author_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Author_ID })
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Author_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BookAuthors", new[] { "Author_ID" });
            DropIndex("dbo.BookAuthors", new[] { "Book_ID" });
            DropIndex("dbo.BookShelves", new[] { "Shelf_ID" });
            DropIndex("dbo.BookShelves", new[] { "Book_ID" });
            DropIndex("dbo.Comments", new[] { "Author_ID" });
            DropIndex("dbo.Comments", new[] { "User_ID" });
            DropIndex("dbo.Comments", new[] { "Book_ID" });
            DropIndex("dbo.Shelves", new[] { "User_ID" });
            DropForeignKey("dbo.BookAuthors", "Author_ID", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.BookShelves", "Shelf_ID", "dbo.Shelves");
            DropForeignKey("dbo.BookShelves", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.Comments", "Author_ID", "dbo.Authors");
            DropForeignKey("dbo.Comments", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.Shelves", "User_ID", "dbo.Users");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.BookShelves");
            DropTable("dbo.Comments");
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
            DropTable("dbo.Shelves");
            DropTable("dbo.Users");
        }
    }
}

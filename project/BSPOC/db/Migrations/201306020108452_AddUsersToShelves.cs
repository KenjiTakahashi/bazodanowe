namespace db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersToShelves : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shelves", "User_ID", "dbo.Users");
            DropIndex("dbo.Shelves", new[] { "User_ID" });
            CreateTable(
                "dbo.ShelfUsers",
                c => new
                    {
                        Shelf_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Shelf_ID, t.User_ID })
                .ForeignKey("dbo.Shelves", t => t.Shelf_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.Shelf_ID)
                .Index(t => t.User_ID);
            
            DropColumn("dbo.Shelves", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shelves", "User_ID", c => c.Int());
            DropIndex("dbo.ShelfUsers", new[] { "User_ID" });
            DropIndex("dbo.ShelfUsers", new[] { "Shelf_ID" });
            DropForeignKey("dbo.ShelfUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.ShelfUsers", "Shelf_ID", "dbo.Shelves");
            DropTable("dbo.ShelfUsers");
            CreateIndex("dbo.Shelves", "User_ID");
            AddForeignKey("dbo.Shelves", "User_ID", "dbo.Users", "ID");
        }
    }
}

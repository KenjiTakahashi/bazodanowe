namespace db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTrustedFieldAndAdminTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Trusted");
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "Trusted", c => c.Boolean(nullable: false));
        }
    }
}

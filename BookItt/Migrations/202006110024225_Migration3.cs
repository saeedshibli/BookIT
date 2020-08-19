namespace BookItt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Date", c => c.DateTime(nullable: false));
        }
    }
}

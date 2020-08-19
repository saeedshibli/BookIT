namespace BookItt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SectionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Sections", t => t.SectionID, cascadeDelete: true)
                .Index(t => t.SectionID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SectionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "SectionID", "dbo.Sections");
            DropIndex("dbo.Books", new[] { "SectionID" });
            DropTable("dbo.Sections");
            DropTable("dbo.Books");
        }
    }
}

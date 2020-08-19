namespace BookItt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderBooks", newName: "OrderBook1");
            CreateTable(
                "dbo.OrderBooks",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.BookID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderBooks");
            RenameTable(name: "dbo.OrderBook1", newName: "OrderBooks");
        }
    }
}

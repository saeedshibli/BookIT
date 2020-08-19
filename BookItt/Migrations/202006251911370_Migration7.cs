namespace BookItt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderBook1", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderBook1", "Book_BookID", "dbo.Books");
            DropIndex("dbo.OrderBook1", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderBook1", new[] { "Book_BookID" });
            CreateIndex("dbo.OrderBooks", "OrderID");
            CreateIndex("dbo.OrderBooks", "BookID");
            AddForeignKey("dbo.OrderBooks", "BookID", "dbo.Books", "BookID", cascadeDelete: true);
            AddForeignKey("dbo.OrderBooks", "OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
            DropTable("dbo.OrderBook1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderBook1",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Book_BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Book_BookID });
            
            DropForeignKey("dbo.OrderBooks", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderBooks", "BookID", "dbo.Books");
            DropIndex("dbo.OrderBooks", new[] { "BookID" });
            DropIndex("dbo.OrderBooks", new[] { "OrderID" });
            CreateIndex("dbo.OrderBook1", "Book_BookID");
            CreateIndex("dbo.OrderBook1", "Order_OrderID");
            AddForeignKey("dbo.OrderBook1", "Book_BookID", "dbo.Books", "BookID", cascadeDelete: true);
            AddForeignKey("dbo.OrderBook1", "Order_OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
    }
}

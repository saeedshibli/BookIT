namespace BookItt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        Status = c.String(),
                        CustomerEmail = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderBooks",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Book_BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Book_BookID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Book_BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderBooks", "Book_BookID", "dbo.Books");
            DropForeignKey("dbo.OrderBooks", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderBooks", new[] { "Book_BookID" });
            DropIndex("dbo.OrderBooks", new[] { "Order_OrderID" });
            DropTable("dbo.OrderBooks");
            DropTable("dbo.Orders");
        }
    }
}

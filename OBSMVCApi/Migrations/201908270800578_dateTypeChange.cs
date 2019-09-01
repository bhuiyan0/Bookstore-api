namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTypeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime());
            AlterColumn("dbo.BookReviews", "ReviewDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime());
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Feedbacks", "FeedbackDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "FeedbackDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.BookReviews", "ReviewDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime(storeType: "date"));
        }
    }
}

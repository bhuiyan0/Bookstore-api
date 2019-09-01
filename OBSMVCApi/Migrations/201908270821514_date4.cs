namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BookReviews", "ReviewDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Feedbacks", "FeedbackDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "FeedbackDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime());
            AlterColumn("dbo.BookReviews", "ReviewDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime());
        }
    }
}

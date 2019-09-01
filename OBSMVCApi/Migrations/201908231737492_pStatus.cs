namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "PurchaseStatus", c => c.String());
            DropColumn("dbo.Purchases", "PaymentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "PaymentStatus", c => c.String());
            DropColumn("dbo.Purchases", "PurchaseStatus");
        }
    }
}

namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payments", "PaymentTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PaymentTypeId", c => c.Int(nullable: false));
        }
    }
}

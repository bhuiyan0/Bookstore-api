namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userTypeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserType", c => c.Int(nullable: false));
        }
    }
}

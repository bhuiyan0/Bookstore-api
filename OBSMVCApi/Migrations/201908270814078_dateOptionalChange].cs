namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateOptionalChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DoB", c => c.DateTime());
            AlterColumn("dbo.Authors", "DoB", c => c.DateTime());
        }
    }
}

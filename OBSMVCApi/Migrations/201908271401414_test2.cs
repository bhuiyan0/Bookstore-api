namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "TranslatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "TranslatorId", c => c.Int(nullable: false));
        }
    }
}

namespace Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiKeyIsStringNow : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Logs.Messages", "ApiKey", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Logs.Messages", "ApiKey", c => c.Guid(nullable: false));
        }
    }
}

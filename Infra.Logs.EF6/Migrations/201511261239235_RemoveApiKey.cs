namespace Infra.Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApiKey : DbMigration
    {
        public override void Up()
        {
            DropColumn("Logs.Messages", "ApiKey");
        }
        
        public override void Down()
        {
            AddColumn("Logs.Messages", "ApiKey", c => c.String());
        }
    }
}

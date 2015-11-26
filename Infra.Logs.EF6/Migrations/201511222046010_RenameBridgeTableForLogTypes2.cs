namespace Infra.Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameBridgeTableForLogTypes2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Logs.[Logs.EventTypes]", newName: "EventTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "Logs.EventTypes", newName: "Logs.EventTypes");
        }
    }
}

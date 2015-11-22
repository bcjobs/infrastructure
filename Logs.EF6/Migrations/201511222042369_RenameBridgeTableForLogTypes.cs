namespace Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameBridgeTableForLogTypes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ELogMessageELogTypes", newName: "Logs.EventTypes");
            RenameTable(name: "dbo.ELogMessageELogType1", newName: "Logs.ExceptionTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ExceptionTypes", newName: "ELogMessageELogType1");
            RenameTable(name: "dbo.EventTypes", newName: "ELogMessageELogTypes");
        }
    }
}

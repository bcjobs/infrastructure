namespace Infra.Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameBridgeTableForLogTypes1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.[Logs.ExceptionTypes]", newName: "ExceptionTypes");
            MoveTable(name: "dbo.[Logs.EventTypes]", newSchema: "Logs");
            MoveTable(name: "dbo.ExceptionTypes", newSchema: "Logs");
        }
        
        public override void Down()
        {
            MoveTable(name: "Logs.ExceptionTypes", newSchema: "dbo");
            MoveTable(name: "Logs.[Logs.EventTypes]", newSchema: "dbo");
            RenameTable(name: "dbo.ExceptionTypes", newName: "Logs.ExceptionTypes");
        }
    }
}

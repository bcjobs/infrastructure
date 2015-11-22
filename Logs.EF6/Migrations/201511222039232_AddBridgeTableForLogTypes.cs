namespace Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBridgeTableForLogTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Logs.Types", "LogMessageId", "Logs.Messages");
            DropIndex("Logs.Types", new[] { "LogMessageId" });
            DropPrimaryKey("Logs.Types");
            CreateTable(
                "dbo.ELogMessageELogTypes",
                c => new
                    {
                        ELogMessage_Id = c.Int(nullable: false),
                        ELogType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ELogMessage_Id, t.ELogType_Id })
                .ForeignKey("Logs.Messages", t => t.ELogMessage_Id, cascadeDelete: true)
                .ForeignKey("Logs.Types", t => t.ELogType_Id, cascadeDelete: true)
                .Index(t => t.ELogMessage_Id)
                .Index(t => t.ELogType_Id);
            
            CreateTable(
                "dbo.ELogMessageELogType1",
                c => new
                    {
                        ELogMessage_Id = c.Int(nullable: false),
                        ELogType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ELogMessage_Id, t.ELogType_Id })
                .ForeignKey("Logs.Messages", t => t.ELogMessage_Id, cascadeDelete: true)
                .ForeignKey("Logs.Types", t => t.ELogType_Id, cascadeDelete: true)
                .Index(t => t.ELogMessage_Id)
                .Index(t => t.ELogType_Id);
            
            AddPrimaryKey("Logs.Types", "Id");
            DropColumn("Logs.Types", "LogMessageId");
        }
        
        public override void Down()
        {
            AddColumn("Logs.Types", "LogMessageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ELogMessageELogType1", "ELogType_Id", "Logs.Types");
            DropForeignKey("dbo.ELogMessageELogType1", "ELogMessage_Id", "Logs.Messages");
            DropForeignKey("dbo.ELogMessageELogTypes", "ELogType_Id", "Logs.Types");
            DropForeignKey("dbo.ELogMessageELogTypes", "ELogMessage_Id", "Logs.Messages");
            DropIndex("dbo.ELogMessageELogType1", new[] { "ELogType_Id" });
            DropIndex("dbo.ELogMessageELogType1", new[] { "ELogMessage_Id" });
            DropIndex("dbo.ELogMessageELogTypes", new[] { "ELogType_Id" });
            DropIndex("dbo.ELogMessageELogTypes", new[] { "ELogMessage_Id" });
            DropPrimaryKey("Logs.Types");
            DropTable("dbo.ELogMessageELogType1");
            DropTable("dbo.ELogMessageELogTypes");
            AddPrimaryKey("Logs.Types", new[] { "Id", "LogMessageId" });
            CreateIndex("Logs.Types", "LogMessageId");
            AddForeignKey("Logs.Types", "LogMessageId", "Logs.Messages", "Id", cascadeDelete: true);
        }
    }
}

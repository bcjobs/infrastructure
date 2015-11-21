namespace Logs.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Logs.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoggedAt = c.DateTime(nullable: false),
                        UserId = c.String(),
                        ImpersonatorId = c.String(),
                        ApiKey = c.Guid(nullable: false),
                        ClientIP = c.String(),
                        EventJson = c.String(),
                        ExceptionJson = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Logs.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogMessageId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => new { t.Id, t.LogMessageId })
                .ForeignKey("Logs.Messages", t => t.LogMessageId, cascadeDelete: true)
                .Index(t => t.LogMessageId)
                .Index(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Logs.Types", "LogMessageId", "Logs.Messages");
            DropIndex("Logs.Types", new[] { "Name" });
            DropIndex("Logs.Types", new[] { "LogMessageId" });
            DropTable("Logs.Types");
            DropTable("Logs.Messages");
        }
    }
}

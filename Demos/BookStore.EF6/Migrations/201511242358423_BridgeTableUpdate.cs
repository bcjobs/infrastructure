namespace BookStore.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BridgeTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("BookStore.BookAuthors");
            AddPrimaryKey("BookStore.BookAuthors", new[] { "EAuthor_Id", "EBook_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("BookStore.BookAuthors");
            AddPrimaryKey("BookStore.BookAuthors", new[] { "EBook_Id", "EAuthor_Id" });
        }
    }
}

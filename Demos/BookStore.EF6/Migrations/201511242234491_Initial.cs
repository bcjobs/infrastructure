namespace BookStore.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BookStore.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "BookStore.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isbn = c.String(nullable: false, maxLength: 13),
                        Title = c.String(nullable: false, maxLength: 200),
                        RegisteredAt = c.DateTime(nullable: false),
                        PublishedAt = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Isbn, unique: true)
                .Index(t => t.Title);
            
            CreateTable(
                "BookStore.BookAuthors",
                c => new
                    {
                        EBook_Id = c.Int(nullable: false),
                        EAuthor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EBook_Id, t.EAuthor_Id })
                .ForeignKey("BookStore.Books", t => t.EBook_Id, cascadeDelete: true)
                .ForeignKey("BookStore.Authors", t => t.EAuthor_Id, cascadeDelete: true)
                .Index(t => t.EBook_Id)
                .Index(t => t.EAuthor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("BookStore.BookAuthors", "EAuthor_Id", "BookStore.Authors");
            DropForeignKey("BookStore.BookAuthors", "EBook_Id", "BookStore.Books");
            DropIndex("BookStore.BookAuthors", new[] { "EAuthor_Id" });
            DropIndex("BookStore.BookAuthors", new[] { "EBook_Id" });
            DropIndex("BookStore.Books", new[] { "Title" });
            DropIndex("BookStore.Books", new[] { "Isbn" });
            DropIndex("BookStore.Authors", new[] { "Name" });
            DropTable("BookStore.BookAuthors");
            DropTable("BookStore.Books");
            DropTable("BookStore.Authors");
        }
    }
}

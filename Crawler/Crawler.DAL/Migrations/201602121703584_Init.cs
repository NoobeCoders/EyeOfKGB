namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisallowPatterns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pattern = c.String(unicode: false),
                        Agent = c.String(unicode: false),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(maxLength: 2048, unicode: false),
                        FoundDateTime = c.DateTime(nullable: false, precision: 0),
                        LastScanDate = c.DateTime(precision: 0),
                        LastProcessDate = c.DateTime(precision: 0),
                        SiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.PersonPageRanks",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.PageId })
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.PageId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2048, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2048, unicode: false, storeType: "nvarchar"),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.PersonPageRanks", "PageId", "dbo.Pages");
            DropForeignKey("dbo.PersonPageRanks", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Keywords", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.DisallowPatterns", "SiteId", "dbo.Sites");
            DropIndex("dbo.Pages", new[] { "SiteId" });
            DropIndex("dbo.PersonPageRanks", new[] { "PageId" });
            DropIndex("dbo.PersonPageRanks", new[] { "PersonId" });
            DropIndex("dbo.Keywords", new[] { "PersonId" });
            DropIndex("dbo.DisallowPatterns", new[] { "SiteId" });
            DropTable("dbo.Keywords");
            DropTable("dbo.Persons");
            DropTable("dbo.PersonPageRanks");
            DropTable("dbo.Pages");
            DropTable("dbo.Sites");
            DropTable("dbo.DisallowPatterns");
        }
    }
}

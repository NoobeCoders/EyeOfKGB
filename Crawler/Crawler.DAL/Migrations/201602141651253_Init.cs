namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.disallowpatterns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pattern = c.String(maxLength: 2048, unicode: false, storeType: "nvarchar"),
                        agent = c.String(maxLength: 256, unicode: false, storeType: "nvarchar"),
                        siteid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sites", t => t.siteid, cascadeDelete: true)
                .Index(t => t.siteid);
            
            CreateTable(
                "dbo.sites",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.pages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        url = c.String(maxLength: 2048, unicode: false),
                        founddatetime = c.DateTime(nullable: false, precision: 0),
                        lastscandate = c.DateTime(precision: 0),
                        lastprocessdate = c.DateTime(precision: 0),
                        siteid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sites", t => t.siteid, cascadeDelete: true)
                .Index(t => t.siteid);
            
            CreateTable(
                "dbo.personpageranks",
                c => new
                    {
                        personid = c.Int(nullable: false),
                        pageid = c.Int(nullable: false),
                        rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.personid, t.pageid })
                .ForeignKey("dbo.persons", t => t.personid, cascadeDelete: true)
                .ForeignKey("dbo.pages", t => t.pageid, cascadeDelete: true)
                .Index(t => t.personid)
                .Index(t => t.pageid);
            
            CreateTable(
                "dbo.persons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 2048, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.keywords",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 2048, unicode: false, storeType: "nvarchar"),
                        personid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.persons", t => t.personid, cascadeDelete: true)
                .Index(t => t.personid);
            
            CreateTable(
                "dbo.roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rolename = c.String(maxLength: 56, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 56, unicode: false, storeType: "nvarchar"),
                        password = c.String(nullable: false, maxLength: 56, unicode: false, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 56, unicode: false, storeType: "nvarchar"),
                        firstname = c.String(nullable: false, maxLength: 56, unicode: false, storeType: "nvarchar"),
                        email = c.String(unicode: false),
                        roleid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.roles", t => t.roleid, cascadeDelete: true)
                .Index(t => t.roleid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "roleid", "dbo.roles");
            DropForeignKey("dbo.disallowpatterns", "siteid", "dbo.sites");
            DropForeignKey("dbo.pages", "siteid", "dbo.sites");
            DropForeignKey("dbo.personpageranks", "pageid", "dbo.pages");
            DropForeignKey("dbo.personpageranks", "personid", "dbo.persons");
            DropForeignKey("dbo.keywords", "personid", "dbo.persons");
            DropIndex("dbo.users", new[] { "roleid" });
            DropIndex("dbo.disallowpatterns", new[] { "siteid" });
            DropIndex("dbo.pages", new[] { "siteid" });
            DropIndex("dbo.personpageranks", new[] { "pageid" });
            DropIndex("dbo.personpageranks", new[] { "personid" });
            DropIndex("dbo.keywords", new[] { "personid" });
            DropTable("dbo.users");
            DropTable("dbo.roles");
            DropTable("dbo.keywords");
            DropTable("dbo.persons");
            DropTable("dbo.personpageranks");
            DropTable("dbo.pages");
            DropTable("dbo.sites");
            DropTable("dbo.disallowpatterns");
        }
    }
}

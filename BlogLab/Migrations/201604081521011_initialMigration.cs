namespace BlogLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "ImageLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "ImageLink");
        }
    }
}

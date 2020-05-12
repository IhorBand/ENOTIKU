namespace kinotiki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "imageData", c=>c.Binary());
            AddColumn("dbo.Users", "imageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "imageData");
            DropColumn("dbo.Users", "imageMimeType");
        }
    }
}

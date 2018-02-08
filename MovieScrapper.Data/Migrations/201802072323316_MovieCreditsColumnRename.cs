namespace MovieScrapper.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieCreditsColumnRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieCredits", "ProfilePath", c => c.String());
            DropColumn("dbo.MovieCredits", "PosterPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieCredits", "PosterPath", c => c.String());
            DropColumn("dbo.MovieCredits", "ProfilePath");
        }
    }
}

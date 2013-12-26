namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Ended = c.DateTime(),
                        Type = c.Int(nullable: false),
                        User_Id = c.Int(),
                        TurnPlayer_Id = c.Int(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.TurnPlayer_Id)
                .ForeignKey("dbo.Users", t => t.Winner_Id)
                .Index(t => t.User_Id)
                .Index(t => t.TurnPlayer_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        TimeMoved = c.DateTime(nullable: false),
                        Game_Id = c.Int(),
                        Player_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Users", t => t.Player_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Winner_Id", "dbo.Users");
            DropForeignKey("dbo.Games", "TurnPlayer_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Moves", "Player_Id", "dbo.Users");
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Moves", "Game_Id", "dbo.Games");
            DropIndex("dbo.Games", new[] { "Winner_Id" });
            DropIndex("dbo.Games", new[] { "TurnPlayer_Id" });
            DropIndex("dbo.Users", new[] { "Game_Id" });
            DropIndex("dbo.Moves", new[] { "Player_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Moves", new[] { "Game_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Moves");
            DropTable("dbo.Games");
        }
    }
}

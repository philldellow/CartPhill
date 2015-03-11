namespace CartPhill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellsMeTheContextHasChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "OPP_Id", "dbo.OPPs");
            DropIndex("dbo.Carts", new[] { "OPP_Id" });
            DropColumn("dbo.Carts", "ProductId");
            RenameColumn(table: "dbo.Carts", name: "OPP_Id", newName: "ProductId");
            AlterColumn("dbo.Carts", "ProductId", c => c.Int(nullable: true));
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.OPPs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.OPPs");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            AlterColumn("dbo.Carts", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Carts", name: "ProductId", newName: "OPP_Id");
            AddColumn("dbo.Carts", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "OPP_Id");
            AddForeignKey("dbo.Carts", "OPP_Id", "dbo.OPPs", "Id");
        }
    }
}

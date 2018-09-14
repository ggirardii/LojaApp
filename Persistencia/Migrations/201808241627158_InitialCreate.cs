namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                {
                    CategoriaID = c.Long(nullable: false, identity: true),
                    Nome = c.String(),
                })
                .PrimaryKey(t => t.CategoriaID);

            CreateTable(
                "dbo.Produto",
                c => new
                {
                    ProdutoID = c.Long(nullable: false, identity: true),
                    Nome = c.String(),
                    CategoriaID = c.Long(),
                    FabricanteID = c.Long(),
                })
                .PrimaryKey(t => t.ProdutoID)
                .ForeignKey("dbo.Categoria", t => t.CategoriaID)
                .ForeignKey("dbo.Fabricante", t => t.FabricanteID)
                .Index(t => t.CategoriaID)
                .Index(t => t.FabricanteID);

            CreateTable(
                "dbo.Fabricante",
                c => new
                {
                    FabricanteID = c.Long(nullable: false, identity: true),
                    Nome = c.String(),
                })
                .PrimaryKey(t => t.FabricanteID);

            //CreateTable(
            //    "dbo.Estado",
            //    e => new
            //    {
            //    });
        }

        public override void Down()
        {
            DropForeignKey("dbo.Produto", "FabricanteID", "dbo.Fabricante");
            DropForeignKey("dbo.Produto", "CategoriaID", "dbo.Categoria");
            DropIndex("dbo.Produto", new[] { "FabricanteID" });
            DropIndex("dbo.Produto", new[] { "CategoriaID" });
            DropTable("dbo.Fabricante");
            DropTable("dbo.Produto");
            DropTable("dbo.Categoria");
        }
    }
}
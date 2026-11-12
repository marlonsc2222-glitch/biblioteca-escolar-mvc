namespace Sistema_Gestion_Biblioteca_Escolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteModeloLibros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Libroes", "CantidadDisponible", c => c.Int(nullable: false));
            DropColumn("dbo.Libroes", "CopiasDisponibles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Libroes", "CopiasDisponibles", c => c.Int(nullable: false));
            DropColumn("dbo.Libroes", "CantidadDisponible");
        }
    }
}

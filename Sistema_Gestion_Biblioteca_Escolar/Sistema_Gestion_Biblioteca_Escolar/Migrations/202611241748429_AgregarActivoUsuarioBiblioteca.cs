namespace Sistema_Gestion_Biblioteca_Escolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarActivoUsuarioBiblioteca : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioBibliotecas", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioBibliotecas", "Activo");
        }
    }
}

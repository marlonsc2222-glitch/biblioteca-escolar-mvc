namespace Sistema_Gestion_Biblioteca_Escolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CuentaPersonals",
                c => new
                    {
                        CuentaPersonalId = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        ContraseñaHash = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CuentaPersonalId)
                .Index(t => t.NombreUsuario, unique: true);
            
            CreateTable(
                "dbo.Libroes",
                c => new
                    {
                        LibroId = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 200),
                        Autor = c.String(nullable: false, maxLength: 150),
                        ISBN = c.String(nullable: false, maxLength: 13),
                        Categoria = c.String(nullable: false, maxLength: 80),
                        CopiasDisponibles = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LibroId)
                .Index(t => t.ISBN, unique: true);
            
            CreateTable(
                "dbo.Prestamoes",
                c => new
                    {
                        PrestamoId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        LibroId = c.Int(nullable: false),
                        FechaPrestamo = c.DateTime(nullable: false, storeType: "date"),
                        FechaDevolucionEsperada = c.DateTime(nullable: false, storeType: "date"),
                        FechaDevolucionReal = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.PrestamoId)
                .ForeignKey("dbo.Libroes", t => t.LibroId, cascadeDelete: true)
                .ForeignKey("dbo.UsuarioBibliotecas", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.LibroId);
            
            CreateTable(
                "dbo.UsuarioBibliotecas",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        Cedula = c.String(nullable: false, maxLength: 25),
                        Correo = c.String(nullable: false, maxLength: 150),
                        TipoUsuario = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .Index(t => t.Cedula, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prestamoes", "UsuarioId", "dbo.UsuarioBibliotecas");
            DropForeignKey("dbo.Prestamoes", "LibroId", "dbo.Libroes");
            DropIndex("dbo.UsuarioBibliotecas", new[] { "Cedula" });
            DropIndex("dbo.Prestamoes", new[] { "LibroId" });
            DropIndex("dbo.Prestamoes", new[] { "UsuarioId" });
            DropIndex("dbo.Libroes", new[] { "ISBN" });
            DropIndex("dbo.CuentaPersonals", new[] { "NombreUsuario" });
            DropTable("dbo.UsuarioBibliotecas");
            DropTable("dbo.Prestamoes");
            DropTable("dbo.Libroes");
            DropTable("dbo.CuentaPersonals");
        }
    }
}

namespace Sistema_Gestion_Biblioteca_Escolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustarNombresColumnas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuentaPersonals", "ContrasenaHash", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.CuentaPersonals", "ContraseñaHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CuentaPersonals", "ContraseñaHash", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.CuentaPersonals", "ContrasenaHash");
        }
    }
}

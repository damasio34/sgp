namespace Damasio34.SGP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracao1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trabalho", "IdNfc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trabalho", "IdNfc");
        }
    }
}

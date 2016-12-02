namespace Damasio34.SGP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContraCheque",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ValorBruto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataFinalizacao = c.DateTime(),
                        DataReferencia = c.DateTime(nullable: false),
                        IdEmprego = c.Guid(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Emprego_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabalho", t => t.Emprego_Id)
                .Index(t => t.Emprego_Id);
            
            CreateTable(
                "dbo.Trabalho",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MesesCiclo = c.Int(nullable: false),
                        HorarioEntrada = c.Time(nullable: false, precision: 7),
                        HorarioSaida = c.Time(nullable: false, precision: 7),
                        CargaHorariaDiaria = c.Time(nullable: false, precision: 7),
                        TempoAlmoco = c.Time(nullable: false, precision: 7),
                        SalarioBruto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ControlaAlmoco = c.Boolean(nullable: false),
                        IdUsuario = c.Guid(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Pessoa_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_Id)
                .Index(t => t.IdUsuario)
                .Index(t => t.Pessoa_Id);
            
            CreateTable(
                "dbo.Ponto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdTrabalho = c.Guid(nullable: false),
                        TipoDoEvento = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        Justificativa = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabalho", t => t.IdTrabalho)
                .Index(t => t.IdTrabalho);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(),
                        Senha = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoLancamento = c.Int(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        ContraCheque_Id = c.Guid(),
                        Conta_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContraCheque", t => t.ContraCheque_Id)
                .ForeignKey("dbo.Conta", t => t.Conta_Id)
                .Index(t => t.ContraCheque_Id)
                .Index(t => t.Conta_Id);
            
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Numero = c.String(),
                        Nome = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        Descricao = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conta",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        Descricao = c.String(),
                        AceitaSaldoNegativo = c.Boolean(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Agencia = c.String(),
                        DvAgencia = c.String(),
                        Conta = c.String(),
                        DvConta = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Banco_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banco", t => t.Banco_Id)
                .Index(t => t.Banco_Id);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Usuario_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.TrabalhoContraCheque",
                c => new
                    {
                        Trabalho_Id = c.Guid(nullable: false),
                        ContraCheque_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trabalho_Id, t.ContraCheque_Id })
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id, cascadeDelete: true)
                .ForeignKey("dbo.ContraCheque", t => t.ContraCheque_Id, cascadeDelete: true)
                .Index(t => t.Trabalho_Id)
                .Index(t => t.ContraCheque_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Trabalho", "Pessoa_Id", "dbo.Pessoa");
            DropForeignKey("dbo.Conta", "Banco_Id", "dbo.Banco");
            DropForeignKey("dbo.Lancamento", "Conta_Id", "dbo.Conta");
            DropForeignKey("dbo.Lancamento", "ContraCheque_Id", "dbo.ContraCheque");
            DropForeignKey("dbo.ContraCheque", "Emprego_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Ponto", "IdTrabalho", "dbo.Trabalho");
            DropForeignKey("dbo.TrabalhoContraCheque", "ContraCheque_Id", "dbo.ContraCheque");
            DropForeignKey("dbo.TrabalhoContraCheque", "Trabalho_Id", "dbo.Trabalho");
            DropIndex("dbo.TrabalhoContraCheque", new[] { "ContraCheque_Id" });
            DropIndex("dbo.TrabalhoContraCheque", new[] { "Trabalho_Id" });
            DropIndex("dbo.Pessoa", new[] { "Usuario_Id" });
            DropIndex("dbo.Conta", new[] { "Banco_Id" });
            DropIndex("dbo.Lancamento", new[] { "Conta_Id" });
            DropIndex("dbo.Lancamento", new[] { "ContraCheque_Id" });
            DropIndex("dbo.Ponto", new[] { "IdTrabalho" });
            DropIndex("dbo.Trabalho", new[] { "Pessoa_Id" });
            DropIndex("dbo.Trabalho", new[] { "IdUsuario" });
            DropIndex("dbo.ContraCheque", new[] { "Emprego_Id" });
            DropTable("dbo.TrabalhoContraCheque");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Conta");
            DropTable("dbo.Categoria");
            DropTable("dbo.Banco");
            DropTable("dbo.Lancamento");
            DropTable("dbo.Usuario");
            DropTable("dbo.Ponto");
            DropTable("dbo.Trabalho");
            DropTable("dbo.ContraCheque");
        }
    }
}

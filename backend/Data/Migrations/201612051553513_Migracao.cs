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
                        DataDeReferencia = c.DateTime(nullable: false),
                        IdTrabalho = c.Guid(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Trabalho_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id)
                .Index(t => t.Trabalho_Id);
            
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Descricao = c.String(),
                        Valor = c.Double(nullable: false),
                        TipoLancamento = c.Int(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        ContraCheque_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContraCheque", t => t.ContraCheque_Id)
                .Index(t => t.ContraCheque_Id);
            
            CreateTable(
                "dbo.Trabalho",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MesesDoCiclo = c.Int(nullable: false),
                        HorarioDeEntrada = c.Time(nullable: false, precision: 7),
                        HorarioDeSaida = c.Time(nullable: false, precision: 7),
                        HorarioDeEntradaDoAlmoco = c.Time(precision: 7),
                        HorarioDeSaidaDoAlmoco = c.Time(precision: 7),
                        SalarioBruto = c.Double(nullable: false),
                        ControlaAlmoco = c.Boolean(nullable: false),
                        IdPessoa = c.Guid(nullable: false),
                        Padrao = c.Boolean(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        Cpf = c.String(),
                        Email = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ponto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TipoDoEvento = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        Justificativa = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdPessoa = c.Guid(nullable: false),
                        Login = c.String(),
                        Senha = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
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
            
            CreateTable(
                "dbo.TrabalhoPonto",
                c => new
                    {
                        Trabalho_Id = c.Guid(nullable: false),
                        Ponto_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trabalho_Id, t.Ponto_Id })
                .ForeignKey("dbo.Trabalho", t => t.Trabalho_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ponto", t => t.Ponto_Id, cascadeDelete: true)
                .Index(t => t.Trabalho_Id)
                .Index(t => t.Ponto_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.ContraCheque", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.TrabalhoPonto", "Ponto_Id", "dbo.Ponto");
            DropForeignKey("dbo.TrabalhoPonto", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.TrabalhoContraCheque", "ContraCheque_Id", "dbo.ContraCheque");
            DropForeignKey("dbo.TrabalhoContraCheque", "Trabalho_Id", "dbo.Trabalho");
            DropForeignKey("dbo.Lancamento", "ContraCheque_Id", "dbo.ContraCheque");
            DropIndex("dbo.TrabalhoPonto", new[] { "Ponto_Id" });
            DropIndex("dbo.TrabalhoPonto", new[] { "Trabalho_Id" });
            DropIndex("dbo.TrabalhoContraCheque", new[] { "ContraCheque_Id" });
            DropIndex("dbo.TrabalhoContraCheque", new[] { "Trabalho_Id" });
            DropIndex("dbo.Usuario", new[] { "IdPessoa" });
            DropIndex("dbo.Trabalho", new[] { "IdPessoa" });
            DropIndex("dbo.Lancamento", new[] { "ContraCheque_Id" });
            DropIndex("dbo.ContraCheque", new[] { "Trabalho_Id" });
            DropTable("dbo.TrabalhoPonto");
            DropTable("dbo.TrabalhoContraCheque");
            DropTable("dbo.Usuario");
            DropTable("dbo.Ponto");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Trabalho");
            DropTable("dbo.Lancamento");
            DropTable("dbo.ContraCheque");
        }
    }
}

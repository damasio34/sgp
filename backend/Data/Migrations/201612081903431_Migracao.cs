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
                        ValorBruto = c.Double(nullable: false),
                        DataFinalizacao = c.DateTime(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        IdCiclo = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciclo", t => t.IdCiclo)
                .Index(t => t.IdCiclo);
            
            CreateTable(
                "dbo.Ciclo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdTrabalho = c.Guid(nullable: false),
                        DataDeInicio = c.DateTime(nullable: false),
                        DateDeTermino = c.DateTime(nullable: false),
                        ControlaAlmoco = c.Boolean(nullable: false),
                        CargaHorariaDiaria = c.Time(nullable: false, precision: 7),
                        TempoDeAlmoco = c.Time(nullable: false, precision: 7),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabalho", t => t.IdTrabalho)
                .Index(t => t.IdTrabalho);
            
            CreateTable(
                "dbo.Ponto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdCiclo = c.Guid(nullable: false),
                        TipoDoEvento = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        Justificativa = c.String(),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciclo", t => t.IdCiclo)
                .Index(t => t.IdCiclo);
            
            CreateTable(
                "dbo.Trabalho",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HorarioDeEntrada = c.Time(nullable: false, precision: 7),
                        HorarioDeSaida = c.Time(nullable: false, precision: 7),
                        HorarioDeEntradaDoAlmoco = c.Time(precision: 7),
                        HorarioDeSaidaDoAlmoco = c.Time(precision: 7),
                        MesesDoCiclo = c.Int(nullable: false),
                        SalarioBruto = c.Double(nullable: false),
                        ControlaAlmoco = c.Boolean(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        IdPessoa = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        IdPessoa = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.IdPessoa)
                .Index(t => t.IdPessoa);
            
            CreateTable(
                "dbo.LancamentoDoContraCheque",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdContraCheque = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false),
                        Valor = c.Double(nullable: false),
                        TipoDeLancamento = c.Int(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContraCheque", t => t.IdContraCheque)
                .Index(t => t.IdContraCheque);
            
            CreateTable(
                "dbo.Imposto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TipoDoImposto = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LancamentoDoContraCheque", "IdContraCheque", "dbo.ContraCheque");
            DropForeignKey("dbo.ContraCheque", "IdCiclo", "dbo.Ciclo");
            DropForeignKey("dbo.Ciclo", "IdTrabalho", "dbo.Trabalho");
            DropForeignKey("dbo.Trabalho", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Usuario", "IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Ponto", "IdCiclo", "dbo.Ciclo");
            DropIndex("dbo.LancamentoDoContraCheque", new[] { "IdContraCheque" });
            DropIndex("dbo.Usuario", new[] { "IdPessoa" });
            DropIndex("dbo.Trabalho", new[] { "IdPessoa" });
            DropIndex("dbo.Ponto", new[] { "IdCiclo" });
            DropIndex("dbo.Ciclo", new[] { "IdTrabalho" });
            DropIndex("dbo.ContraCheque", new[] { "IdCiclo" });
            DropTable("dbo.Imposto");
            DropTable("dbo.LancamentoDoContraCheque");
            DropTable("dbo.Usuario");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Trabalho");
            DropTable("dbo.Ponto");
            DropTable("dbo.Ciclo");
            DropTable("dbo.ContraCheque");
        }
    }
}

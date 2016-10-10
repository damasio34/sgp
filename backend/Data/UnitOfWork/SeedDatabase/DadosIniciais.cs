using System;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.SeedDatabase
{
    public sealed class DadosIniciais
    {
        private MainUnitOfWork _context;

        public void Seed(MainUnitOfWork context)
        {
            this._context = context;
            DadosSimulacao();
            //CargaInicial();
        }

        private void DadosSimulacao()
        {
            var usuario = InserirUsuario();
            var emprego = InserirEmprego(usuario);
            //InserirPontos(emprego);
        }

        private Usuario InserirUsuario()
        {
            var usuario = UsuarioFactory.Criar("damasio34", "1235");
            this._context.RegisterNew(usuario);                
            _context.Commit();

            return usuario;
        }

        private Trabalho InserirEmprego(Usuario usuario)
        {
            var trabalho = new Trabalho
            {
                Usuario = usuario,
                CargaHorariaDiaria = new TimeSpan(8, 0, 0),
                ControlaAlmoco = true,
                HorarioEntrada = new TimeSpan(9, 0, 0),
                HorarioSaida = new TimeSpan(18, 0, 0),
                TempoAlmoco = new TimeSpan(1, 0, 0),
                MesesCiclo = 4,
                SalarioBruto = 1000M                
            };
            trabalho.GerarId();

            this._context.RegisterNew(trabalho);
            _context.Commit();

            return trabalho;
        }
    }
}
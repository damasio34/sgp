using System;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;

namespace Damasio34.SGP.Data.UnitOfWork.SeedDatabase
{
    public sealed class DadosIniciais
    {
        private MainUnitOfWork _context;

        public void Seed(MainUnitOfWork context)
        {
            this._context = context;

            InserirUsuario();
        }

        private void InserirUsuario()
        {            
            var usuario = UsuarioFactory.Criar("damasio34", "1235");
            var trabalho = TrabalhoFactory.Criar(usuario, 1000, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0),
                4, new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0));

            var rnd = new Random();
            var dataAtual = new DateTime(2016, 11, 1);
            var dataFinal = new DateTime(2016, 11, 30);
            while (dataAtual <= dataFinal)
            {
                if (!dataAtual.DayOfWeek.Equals(DayOfWeek.Sunday) && !dataAtual.DayOfWeek.Equals(DayOfWeek.Saturday))
                {                    
                    var minutos = rnd.Next(30, 59);
                    trabalho.AdicionarPonto(TipoDoEvento.Entrada, 
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 8, minutos, 0));

                    trabalho.AdicionarPonto(TipoDoEvento.EntradaDoAlmoco,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 12, 0, 0));

                    trabalho.AdicionarPonto(TipoDoEvento.SaidaDoAlmoco,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 13, 0, 0));

                    minutos = rnd.Next(30, 59);
                    trabalho.AdicionarPonto(TipoDoEvento.Saida,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 18, minutos, 0));
                }

                dataAtual = dataAtual.AddDays(1);
            }

            this._context.RegisterNew(trabalho);
            this._context.RegisterNew(usuario);
            this._context.Commit();
        }
    }
}
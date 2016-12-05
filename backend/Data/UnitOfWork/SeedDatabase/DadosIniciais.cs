using System;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
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
            var trabalho = TrabalhoFactory.Criar(1000, new TimeSpan(8, 0, 0), new TimeSpan(1, 0, 0),
                new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 4);
            var usuario = UsuarioFactory.Criar("damasio34", "1235", trabalho);

            this._context.RegisterNew(trabalho);
            this._context.RegisterNew(usuario);
            this._context.Commit();
        }
    }
}
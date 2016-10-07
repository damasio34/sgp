using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;

namespace Damasio34.SGP.Data.UnitOfWork.SeedDatabase
{
    public sealed class DadosIniciais
    {
        private MainUnitOfWork Context;

        public void Seed(MainUnitOfWork context)
        {
            this.Context = context;
            DadosSimulacao();
            //CargaInicial();
        }

        private void DadosSimulacao()
        {
            var usuario = InserirUsuario();
            //InserirEmprego(usuario);
            //InserirPontos(emprego);
        }

        private Usuario InserirUsuario()
        {
            var usuario = UsuarioFactory.Criar("damasio34", "1235");
            this.Context.RegisterNew(usuario);                
            Context.SaveChanges();            
            Context.Commit();

            return usuario;
        }
    }
}
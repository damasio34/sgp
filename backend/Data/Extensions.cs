using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Repositories;

namespace Damasio34.SGP.Data
{
    public static class Extensions
    {
        public static IQueryable<TEntidade> Paginar<TEntidade>(this IRepository<TEntidade> repository, uint pagina, uint quantidadePorPagina)
    where TEntidade : EntidadeBase
        {
            var query = repository.BaseQuery;
            return query.Paginar(pagina, quantidadePorPagina);
        }

        public static IQueryable<TEntidade> Paginar<TEntidade>(this IQueryable<TEntidade> query, uint pagina, uint quantidadePorPagina)
            where TEntidade : EntidadeBase
        {
            var salto = GetSalto(pagina, quantidadePorPagina);
            return query.Skip(salto).Take((int)quantidadePorPagina);
        }

        private static int GetSalto(uint pagina, uint quantidadePorPagina)
        {
            return (int)(pagina - 1) * (int)quantidadePorPagina;
        }
    }
}

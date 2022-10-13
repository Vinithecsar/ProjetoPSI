using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Contexts;
using Modelo.Tabelas;
using System.Linq;

namespace Persistência.DAL.Tabelas
{
    class CategoriaDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Categoria> ObterCategoriasClassificasPorNome()
        {
            return context.Categorias.OrderBy(b => b.Nome);
        }
    }
}

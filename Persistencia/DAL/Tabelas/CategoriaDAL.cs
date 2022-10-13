using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Contexts;
using Modelo.Tabelas;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(b => b.Nome);
        }
    }
}

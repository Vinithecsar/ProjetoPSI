using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Contexts;
using Modelo.Cadastros;
using System.Linq;

namespace Persistencia.DAL.Cadastros
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(b => b.Nome);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Modelo.Cadastros;
using Modelo.Tabelas;
using Servico.Cadastros;
using Servico.Tabelas;
using System.Net;
using System.IO;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        // GET: Home
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }
    }
}

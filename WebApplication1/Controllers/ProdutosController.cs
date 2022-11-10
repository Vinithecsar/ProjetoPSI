using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Modelo.Cadastros;
using Modelo.Tabelas;
//using Persistencia.Contexts;
using Servico.Cadastros;
using Servico.Tabelas;
using System.Net;

namespace WebApplication1.Controllers
{
    public class ProdutosController : Controller
    {
        //metodos privados (inicio)
        private byte[] SetLogotipo(HttpPostedFileBase logotipo)
        {
            var bytesLogotipo = new byte[logotipo.ContentLength];
            logotipo.InputStream.Read(bytesLogotipo, 0, logotipo.ContentLength);
            return bytesLogotipo;
        }
        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase logotipo, string chkRemoverImagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (chkRemoverImagem != null)
                    {
                        produto.Logotipo = null;
                    }
                    if (logotipo != null)
                    {
                        produto.LogotipoMimeType = logotipo.ContentType;
                        produto.Logotipo = SetLogotipo(logotipo);
                    }
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);
            }
        }
        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = produtoServico.ObterProdutoPorId((long)id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome", produto.FabricanteId);
            }


        }

        //metodos privados (fim)


        //private EFContext context = new EFContext();
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Produtos
        public ActionResult Index()
        {
            //var produtos = context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante). OrderBy(n => n.Nome);
            //return View(produtos);
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            //ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome");
            //ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome");
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
            //try
            //{
            //    // TODO: Add insert logic here
            //    context.Produtos.Add(produto);
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View(produto);
            //}
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Produto produto = context.Produtos.Find(id);
            //if (produto == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.CategoriaId = new SelectList(context.Categorias.OrderBy(b => b.Nome), "CategoriaId", "Nome", produto.CategoriaId);
            //ViewBag.FabricanteId = new SelectList(context.Fabricantes.OrderBy(b => b.Nome), "FabricanteId", "Nome", produto.FabricanteId);
            //return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            return GravarProduto(produto, logotipo, chkRemoverImagem);
        }
        //try
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Entry(produto).State = EntityState.Modified;
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(produto);
        //}
        //catch
        //{
        //    return View(produto);
        //}

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Produto produto = context.Produtos.Where(p => p.ProdutoId == id).
            //Include(c => c.Categoria).Include(f => f.Fabricante).First();
            //if (produto == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(produto);
        }

        // GET: Produtos/Delete/5
        
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Produto produto = context.Produtos.Where(p => p.ProdutoId == id).
            //Include(c => c.Categoria).Include(f => f.Fabricante).First();
            //if (produto == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            //try
            //{
            //    Produto produto = context.Produtos.Find(id);
            //    context.Produtos.Remove(produto);
            //    context.SaveChanges();
            //    TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

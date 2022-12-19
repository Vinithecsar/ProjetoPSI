using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persistencia.Contexts;
using Modelo.Tabelas;
using Servico.Tabelas;

namespace WebApplication1.Areas.Tabelas.Controllers
{
    public class CategoriasController : Controller
    {
        private CategoriaServico categoriaServico = new CategoriaServico();
        private ActionResult ObterVisaoCategoriaPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = categoriaServico.ObterCategoriaPorId((long)id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }
        private ActionResult GravarCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriaServico.GravarCategoria(categoria);
                    return RedirectToAction("index");
                }
                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }
        }
        // GET: Categorias
        public ActionResult Index()
        {
            return View(categoriaServico.ObterCategoriasClassificadasPorNome());
            //return View(context.Categorias.OrderBy(c => c.Nome));
            //return View(cat);
        }
        // Create get
        public ActionResult Create()
        {
            return View();
        }
        // Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            // IEnumerable<Categoria> a = cat.Where(c => c.CategoriaId >0);
            //context.Categorias.Add(ca);
            //context.SaveChanges();
            //ca.CategoriaId = cat.Select(c => c.CategoriaId).Max() + 1;// type ;  1, 2, 3, 4
            //cat.Add(ca);
            //return RedirectToAction("Index");
            return GravarCategoria(categoria);
        }
        //Edit alone
        public ActionResult Edit(long? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }
        //Edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            return GravarCategoria(categoria);
            
        }
        //Details alone
        public ActionResult Details(long? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }
        //Delete alone
        public ActionResult Delete(long? id)
        {
            return ObterVisaoCategoriaPorId(id);
        }
        //Delete post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Categoria categoria = categoriaServico.EliminarCategoriaPorId(id);
                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removido";
                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }
    }
}
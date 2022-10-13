using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persistencia.Contexts;
using Modelo.Tabelas;

namespace WebApplication1.Controllers
{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();

        private static IList<Categoria> cat = new List<Categoria>()
        {
            new Categoria() {CategoriaId = 1, Nome ="Notebooks"},
            new Categoria() {CategoriaId = 2, Nome = "Monitores"},
            new Categoria() {CategoriaId = 3, Nome=  "Desktops"}
        };
        // GET: Categorias
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
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
        public ActionResult Create(Categoria ca)
        {
            // IEnumerable<Categoria> a = cat.Where(c => c.CategoriaId >0);

            context.Categorias.Add(ca);
            context.SaveChanges();
            //ca.CategoriaId = cat.Select(c => c.CategoriaId).Max() + 1;// type ;  1, 2, 3, 4
            //cat.Add(ca);
            return RedirectToAction("Index");
        }
        //Edit alone
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            //return View(cat.Where(m => m.CategoriaId == id).First());//true or false ; 1 ,notebook
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }
        //Edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria ca)
        {
            if (ModelState.IsValid)
            {
                context.Entry(ca).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ca);
            //cat.Remove(cat.Where(c => c.CategoriaId == ca.CategoriaId).First());//true or false; 1 , notebook
            //cat.Add(ca);//colocar o novo id=1
            
        }
        //Details alone
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categorias = context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Fabricante").First();
            //Categoria categorias = context.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
            // View(cat.Where(c => c.CategoriaId == id).First());
        }
        //Delete alone
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
            //Categoria a = cat.Where(c => c.CategoriaId == id).First();
            //return View(cat.Where(c => c.CategoriaId == id).First());
        }
        //Delete post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = context.Categorias.Find(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removido";
            //Categoria a = cat.Where(c => c.CategoriaId == ca.CategoriaId).First();
            //cat.Remove(a);
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FabricanteController : Controller
    {
        private EFContext context = new EFContext();

        private static IList<Fabricante> fab = new List<Fabricante>()
        {
            new Fabricante() {FabricanteId = 1, Nome ="LG"},
            new Fabricante() {FabricanteId = 2, Nome = "Facebook"},
            new Fabricante() {FabricanteId = 3, Nome=  "Alphabet"},
            new Fabricante() {FabricanteId = 4, Nome = "Microsoft"}
        };
        // GET: Fabricante
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(c => c.Nome));
            //return View(fab); /*context.Fabricantes.OrderBy(c=> c.Nome)*/
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabri)
        {

            context.Fabricantes.Add(fabri);
            context.SaveChanges();

            //fab.Add(fabri);
            //fabri.FabricanteId = fab.Select(f => f.FabricanteId).Max() + 1; // type ;  1, 2, 3, 4
            return RedirectToAction("Index");
        }
        // GET: Fabricantes/Edit/5
        /*public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }*/
        // POST: Fabricantes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            //Fabricante fabricante = fab.Where(m => m.FabricanteId == id).First();
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }
        /*public ActionResult Edit(Fabricante fa)
        {
            fab.Remove(fab.Where(c => c.FabricanteId == fa.FabricanteId).First());
            fab.Add(fa);//colocar o novo id=1
            return RedirectToAction("Index");
        }*/

        // GET: Fabricantes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            //Fabricante fabricante = fab.Where(m => m.FabricanteId == id).First();
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }
        // GET: Fabricantes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            //Fabricante fabricante = fab.Where(m => m.FabricanteId == id).First();
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }
        // POST: Fabricantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            
            //Fabricante fabricante = fab.Where(m => m.FabricanteId == id).First();
            //fab.Remove(fabricante);
            return RedirectToAction("Index");
        }
    }
}
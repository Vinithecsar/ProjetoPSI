using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(fab); /*context.Fabricantes.OrderBy(c=> c.Nome)*/
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

            //context.Fabricantes.Add(fabri);
            //ontext.SaveChanges();

            fabri.FabricanteId = fab.Select(c => c.FabricanteId).Max() + 1; // type ;  1, 2, 3, 4
            fab.Add(fabri);
            return RedirectToAction("Index");
        }
    }
}
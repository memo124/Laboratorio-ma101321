using MA101321.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MA101321.Controllers
{
    public class AutorController : Controller
    {
        // GET: AutoController1
        public ActionResult Index()
        {
            var listaAutor = from autor in RecuperaAutores()
                              orderby autor.Id 
                              select autor;
            return View(listaAutor);
        }

        // GET: AutoController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AutoController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var nuevoAutor = new autor();
                    nuevoAutor.nombre = collection["nombre"];
                    nuevoAutor.apellido = collection["apellido"];
                    nuevoAutor.nacionalidad = collection["nacionalidad"];
                    context.autor.Add(nuevoAutor);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoController1/Edit/5
        public ActionResult Edit(int id)
        {
            var autor = this.RecuperaAutor(id);
            return View(autor);
        }

        // POST: AutoController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var nuevoAutor = context.autor.Find(id);
                    nuevoAutor.nombre = collection["nombre"];
                    nuevoAutor.apellido = collection["apellido"];
                    nuevoAutor.nacionalidad = collection["nacionalidad"];
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoController1/Delete/5
        public ActionResult Delete(int id)
        {
            var autor  = this.RecuperaAutor(id);
            return View(autor);
        }

        // POST: AutoController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var autor = context.autor.Find(id);
                    context.autor.Remove(autor);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        public List<autor> RecuperaAutores()
        {
            using (var context = new Conexion())
            {
                var lista = context.autor.ToList();
                return lista;
            }
        }

        [NonAction]
        public autor RecuperaAutor(int id)
        {
            using (var context = new Conexion())
            {
                var lista = context.autor.Find(id);
                return lista;
            }
        }
    }
}

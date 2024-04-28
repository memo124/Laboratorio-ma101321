using MA101321.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MA101321.Controllers
{
    public class LibroController : Controller
    {
        // GET: LibroController
        public ActionResult Index()
        {

            var listaLibros = from libro in RecuperaLibros()
                              orderby libro.Id
                              select libro;
            return View(listaLibros);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            GenerarCombo();
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var nuevoLibro = new libro();
                    nuevoLibro.titulo = collection["titulo"];
                    nuevoLibro.isbn = int.Parse(collection["isbn"]);
                    nuevoLibro.anio_edicion = int.Parse(collection["anio_edicion"]);
                    nuevoLibro.editorial = collection["editorial"];
                    nuevoLibro.descripcion = collection["descripcion"];
                    nuevoLibro.id_autor = int.Parse(collection["id_autor"]);
                    context.libro.Add(nuevoLibro);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            GenerarCombo();
            var libro = RecuperaLibro(id);
            return View(libro);
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var nuevoLibro = context.libro.Find(id);
                    nuevoLibro.titulo = collection["titulo"];
                    nuevoLibro.isbn = int.Parse(collection["isbn"]);
                    nuevoLibro.anio_edicion = int.Parse(collection["anio_edicion"]);
                    nuevoLibro.editorial = collection["editorial"];
                    nuevoLibro.descripcion = collection["descripcion"];
                    nuevoLibro.id_autor = int.Parse(collection["id_autor"]);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            var libro = RecuperaLibro(id);
            return View(libro);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var context = new Conexion())
                {
                    var nuevoLibro = context.libro.Find(id);
                    context.Remove(nuevoLibro);
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
        public void GenerarCombo()
        {
            SelectList lista;
            using (var context = new Conexion())
            {
                var autores = context.autor.ToList();
                lista = new SelectList(autores, "Id", "nombre");
            }
            ViewBag.Autores = lista;
        }

        [NonAction]
        public List<libro> RecuperaLibros()
        {
            using (var context = new Conexion())
            {
                var lista = context.libro.Include(x=>x.Autor).ToList();
                return lista;
            }
        }

        [NonAction]
        public libro RecuperaLibro(int id)
        {
            using (var context = new Conexion())
            {
                var lista = context.libro.Where(x=>x.Id == id).Include(x => x.Autor).FirstOrDefault();
                return lista;
            }
        }
    }
}

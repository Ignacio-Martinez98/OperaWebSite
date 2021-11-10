using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;//agregar
using OperaWebSite.Models;//agregar
using OperaWebSite.Data;//agregar
using System.Diagnostics;//para Debug.WriteLine
using OperaWebSite.Filters;//Agregar para usar MyFilterAction (filtros)

namespace OperaWebSite.Controllers
{
    [MyFilterAction]//Para aplicar el/los filtros
    //Este filtro esta declarado a nivel formulario, tambien podemos usarlos para metodos especificos
    public class OperaController : Controller
    {
        //Crear instancia del DbContext
        private OperaDbContext context = new OperaDbContext();

        // GET: Opera o /Opera/Index
        public ActionResult Index()
        {
            //Traer todas las operas usando EF
            var operas = context.Operas.ToList();
            //Tambien podria ser List<Opera> operas=context.Operas.ToList();

            //El controller retorna una vista "Index" con la lista de operas
            return View("Index", operas);
        }

        //Crear dos metodos para la insercion de la opera en la DB

        //primer metodo por GET para retornar la vista de registro
        [HttpGet]//El Get es implicito, asi y todo lo podemos indicar
        public ActionResult Create()
        {
            //Creamos la instancia sin valores en las propiedades
            Opera opera = new Opera();

            //Retornamos la vista "Create", que tiene el objeto opera
            return View("Create", opera);
        }

        //Segundo Create es por Post, para insertar la nueva Opera en la base
        //Cuando el usuario en la vista Create hace click en enviar
        [HttpPost]
        public ActionResult Create(Opera opera)
        {
            if (ModelState.IsValid) //.IsValid chequea las propiedades del modelo, por ej--> si una propiedad tiene [Required] comprueba que ese campo este completo
            {
                context.Operas.Add(opera);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", opera);
        }

        //Trae una opera por Id
        //Por defecto es GET,entonces [HttpGet] es opcional
        //Ruta de navegacion--> /Opera/Detail/id<--id= un numero
        public ActionResult Detail(int id)
        {
            Opera opera = context.Operas.Find(id); //Buscamos por id
            if (opera!=null)
            {
                return View("Detail", opera); //Si opera tiene un elemento con ese id retorna una vista
            }
            else
            {
                return HttpNotFound(); //si no se encuentra una opera con ese id retorna un notfound
            }
        }

        //Get  /Opera/Delete/id
        //Get por default [HttpGet] opcional
        public ActionResult Delete(int id)
        {
            Opera opera = context.Operas.Find(id);
            if (opera != null)
            {
                return View("Delete", opera);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //Ruta de navegacion--> /Opera/Delete
        [HttpPost]//Aca no es opcional 
        [ActionName("Delete")]//Decoracion para no tener problemas en la url
        public ActionResult DeleteConfirmed(int id)
        {
            Opera opera = context.Operas.Find(id);
            context.Operas.Remove(opera);
            context.SaveChanges();
            return RedirectToAction("Index");//Despues de eliminar la opera retornamos a la pagina principal
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Opera opera = context.Operas.Find(id);
            if (opera != null)
            {
                return View("Edit", opera);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(Opera opera)
        {
            if (ModelState.IsValid)
            {
                context.Entry(opera).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Edit", opera);
        }

        //Filtros---- Quedan comentados porque los pase a MyFilterAction

        ////ocurre antes
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //controller/action
        //    //{controller}/{action}
        //    //Opera/Create
        //    var controller = filterContext.RouteData.Values["controller"];
        //    var action = filterContext.RouteData.Values["action"];
        //    Debug.WriteLine("Controller: " + controller+ " Action: " + action+ " Paso por OnActionExecuting");
                
        //}

        ////ocurre despues
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    var controller = filterContext.RouteData.Values["controller"];
        //    var action = filterContext.RouteData.Values["action"];
        //    Debug.WriteLine("Controller: " + controller + " Action: " + action + " Paso por OnActionExecuted");
        //}
    }   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (CrudEntities1 db = new CrudEntities1())
            {
               
                lst = (from d in db.Tabla
                           select new ListTablaViewModel
                           {
                               Id = d.id,
                               Nombre = d.nombre,
                               Fecha_Nacimiento = d.fecha_nacimiento,
                               Correo = d.correo
                           }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = new Tabla();
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        db.Tabla.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ActionResult Editar(int id)
        {
            TablaViewModel model = new TablaViewModel();
            using (CrudEntities1 db = new CrudEntities1())
            {
                var oTabla = db.Tabla.Find(id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento = oTabla.fecha_nacimiento;
                model.Id = oTabla.id;
            }
                return View(model);
        }
        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = db.Tabla.Find(model.Id);
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        db.Entry(oTabla).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            using (CrudEntities1 db = new CrudEntities1())
            {
                
                var oTabla = db.Tabla.Find(id);
                db.Tabla.Remove(oTabla);
                db.SaveChanges();
            }
            return Redirect("~/Tabla/");
        }
    }
}
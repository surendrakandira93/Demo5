using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo5.DataLayr;
using Demo5.Models;

namespace Demo5.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var db = new Demo5Entities())
            { 
                var dt = db.Categories.Select(s => new CategoryDto() { Id = s.Id, Name = s.Name }).ToList();
                return View(dt);
            }

        }
        public ActionResult AddEdit(int? id)
        {
            CategoryDto model = new CategoryDto();
            if (id.HasValue && id > 0)
            {
                using (var db = new Demo5Entities())
                {
               var dbCategary=     db.Categories.Find(id.Value);
                    model.Id = dbCategary.Id;   
                    model.Name = dbCategary.Name;   
                }

            }
           
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
             
                using (var db = new Demo5Entities())
                {
                    if (category.Id.HasValue)
                    {
                        var dbCategary = db.Categories.Find((category.Id));
                        dbCategary.Name = category.Name;
                        
                    }
                    else
                    {
                        db.Categories.Add(new Category() {Name= category.Name });
                    }
                    db.SaveChanges();
                   
                }
                return RedirectToAction("index");
            }

            return View(category);

        }
        public ActionResult Delete(int id)
        {
            using (var db = new Demo5Entities())
            {
                var dbCategary = db.Categories.Find(id);
                db.Categories.Remove(dbCategary);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}


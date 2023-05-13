using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo5.DataLayr;
using Demo5.Models;

namespace Demo5.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var db = new Demo5Entities())
            {
                var dt = db.SubCategories.Select(s => new SubcategoryDto() { Id = s.Id, Name = s.Name,Description=s.Description }).ToList();
                return View(dt);
            }

        }
        public ActionResult AddEdit(int? id)
        {
            SubcategoryDto model = new SubcategoryDto();
            if (id.HasValue && id > 0)
            {
                using (var db = new Demo5Entities())
                {
                    var dbSubCategary = db.SubCategories.Find(id.Value);
                    model.Id = dbSubCategary.Id;
                    model.Name = dbSubCategary.Name;
                    model.Description = dbSubCategary.Description;
                }

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(SubcategoryDto subcategory)
        {
            if (ModelState.IsValid)
            {

                using (var db = new Demo5Entities())
                {
                    if (subcategory.Id.HasValue)
                    {
                        var subcategoryDb = db.SubCategories.Find((subcategory.Id));
                        subcategoryDb.Name = subcategory.Name;
                        subcategoryDb.Description = subcategory.Description;

                    }
                    else
                    {
                        db.SubCategories.Add(new SubCategory() { Name = subcategory.Name,Description=subcategory.Description });
                    }
                    db.SaveChanges();

                }
                return RedirectToAction("index");
            }

            return View(subcategory);

        }
        public ActionResult Delete(int id)
        {
            using (var db = new Demo5Entities())
            {
                var dbsubCategary = db.SubCategories.Find(id);
                db.SubCategories.Remove(dbsubCategary);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

    
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Demo5.DataLayr;
using Demo5.Models;
using Group = Demo5.DataLayr.Group;

namespace Demo5.Controllers
{
    public class TagController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var db = new Demo5Entities())
            {
                var dt = db.Tags.Select(s => new Tagdto() { Id = s.Id, Name = s.Name, Description = s.Discription }).ToList();
                return View(dt);
            }

        }
        public ActionResult AddEdit(int? id)
        {
            Tagdto model = new Tagdto();
          
            if (id.HasValue && id > 0)
            {
                using (var db = new Demo5Entities())
                {
                    var Tag = db.Tags.Find(id);
                    model.Id = Tag.Id;
                    model.Name = Tag.Name;
                    model.Description = Tag.Discription;

                }

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(Tagdto tag)
        {
            if (ModelState.IsValid)
            {

                using (var db = new Demo5Entities())
                {
                    if (tag.Id.HasValue)
                    {
                        var Tagdto = db.Tags.Find((tag.Id));
                        Tagdto.Name = tag.Name;
                        Tagdto.Discription = tag.Description;
                    }
                    else
                    {
                        Tag tags = db.Tags.Add(new Tag() { Name = tag.Name,Discription=tag.Description});
                    }
                   db.SaveChanges();

                }
                return RedirectToAction("index");
            }

            return View(tag);

        }
        public ActionResult Delete(int id)
        {
            using (var db = new Demo5Entities())
            {
                var dbCategary = db.Tags.Find(id);
                db.Tags.Remove(dbCategary);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}


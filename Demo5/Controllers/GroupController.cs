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
    public class GroupController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var db = new Demo5Entities())
            {
                var dt = db.Groups.Select(s => new GroupDto() { Id = s.Id, Name = s.Name,ParentName = s.Group2.Name }).ToList();
                return View(dt);
            }

        }
        public ActionResult AddEdit(int? id)
        {
            GroupDto model = new GroupDto();
            using (var db = new Demo5Entities())
            {
                var dt = db.Groups.Where(m=>m.Id!= id).Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Name}).ToList();
                model.ParentList = dt;
            }
            if (id.HasValue && id > 0)
            {
                using (var db = new Demo5Entities())
                {
                    var group = db.Groups.Find(id.Value);
                    model.Id =group.Id;
                    model.Name = group.Name;
                    model.ParentId = group.ParentId;

                }

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddEdit(GroupDto group)
        {
            if (ModelState.IsValid)
            {

                using (var db = new Demo5Entities())
                {
                    if (group.Id.HasValue)
                    {
                        var dbCategary = db.Groups.Find((group.Id));
                        dbCategary.Name = group.Name;
                        dbCategary.ParentId = group.ParentId;
                    }
                    else
                    {
                        db.Groups.Add(new DataLayr.Group() { Name = group.Name,ParentId=group.ParentId });
                    }
                    db.SaveChanges();

                }
                return RedirectToAction("index");
            }

            return View(group);

        }
        public ActionResult Delete(int id)
        {
            using (var db = new Demo5Entities())
            {
                var dbCategary = db.Groups.Find(id);
                db.Groups.Remove(dbCategary);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}


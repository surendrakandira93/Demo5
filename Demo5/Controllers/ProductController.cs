using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Demo5.DataLayr;
using Demo5.Models;
using Group = Demo5.DataLayr.Group;


namespace Demo5.Controllers
{
    public class ProductController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var db = new Demo5Entities())
            {
                var dt = db.Products.Select(s => new ProductDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Tittle = s.Tittle,
                    Description = s.Description,
                    Url = s.Url,
                    Price = s.Price,
                    OldPrice = s.OldPrice,
                    Shipping = s.Shipping,
                    IsStock = s.IsStock,
                }).ToList();
                return View(dt);
            }

        }
        public ActionResult AddEdit(int? id)
        {
            ProductDto model = new ProductDto();
            using (var db = new Demo5Entities())
            {
                var dt = db.Categories.Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Name }).ToList();
                model.Categories = dt;
                dt = db.Tags.Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Name }).ToList();
                model.Taglist = dt;
            }
            if (id.HasValue && id > 0)
            {
                using (var db = new Demo5Entities())
                {
                    var product = db.Products.Find(id.Value);
                    model.Id = product.Id;
                    model.Name = product.Name;
                    model.Tittle = product.Tittle;
                    model.Description = product.Description;
                    model.Url = product.Url;
                    model.Price = product.Price;
                    model.OldPrice = product.OldPrice;
                    model.Shipping = product.Shipping;
                    model.IsStock = product.IsStock;



                }

            }

            return View(model);
        }

        [HttpPost]

        public ActionResult AddEdit(ProductDto product, List<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {

                List<string> imagelist=new List<string>();

                foreach (var files in file)
                {
                    if (files != null)
                    {
                        string pic = System.IO.Path.GetFileName(files.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage"), pic);
                        files.SaveAs(path);
                        imagelist.Add(pic);
                         
                    }
                }
               


                using (var db = new Demo5Entities())
                {
                    if (product.Id.HasValue)
                    {
                        var dbprouduct = db.Products.Find((product.Id));
                        dbprouduct.Name = product.Name;
                        dbprouduct.Tittle = product.Tittle;

                        dbprouduct.Description = product.Description;
                        dbprouduct.Url = product.Url;
                        dbprouduct.Price = product.Price.Value;
                        dbprouduct.OldPrice = product.OldPrice;
                        dbprouduct.Shipping = product.Shipping.Value;
                        dbprouduct.IsStock = product.IsStock;
                        dbprouduct.ProductTags = product.Tag.Select(s => new ProductTag() { TagId = s }).ToList();
                        dbprouduct.ProductImgages = imagelist.Select(s => new ProductImgage() { ImageName = s }).ToList();

                    }
                    else
                    {
                        db.Products.Add(new DataLayr.Product()
                        {
                            Name = product.Name,
                            Tittle = product.Tittle,
                            Description = product.Description,
                            Url = product.Url,
                            Price = product.Price.Value,
                            OldPrice =
                            product.OldPrice.Value,
                            Shipping = product.Shipping.Value,
                            IsStock = product.IsStock,
                            CategoryId = product.CategoryId.Value,
                            ProductImgages=imagelist.Select(s => new ProductImgage() {ImageName=s}).ToList(),
                            ProductTags = product.Tag.Select(s => new ProductTag () { TagId = s }).ToList(),


                        });

                       // db.ProductImgages.Add(new ProductImgage() {  })


                    }
                    db.SaveChanges();

                }
                return RedirectToAction("index");
            }

            return View(product);

        }
        public ActionResult Delete(int id)
        {
            using (var db = new Demo5Entities())
            {
                var dbproduct = db.Products.Find(id);
                db.Products.Remove(dbproduct);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}


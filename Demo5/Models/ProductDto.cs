using Demo5.DataLayr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo5.Models
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public decimal? Price { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? Shipping { get; set; }
        public bool IsStock { get; set; }


        public List<string> ImageName { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Taglist { get; set; }
        public int ? CategoryId { get; set; }   



        public List<int> Tag { get; set; }
        public List<ProductspecifictionDto> Specifiction { get; set; }

    }

    public class ProductspecifictionDto
    {
        public string Name { get; set; }
        public string value {get; set; }    
        
           
        }

    }

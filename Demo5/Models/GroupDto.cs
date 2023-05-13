using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo5.Models
{
    public class GroupDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<SelectListItem> ParentList { get; set; }
        public string ParentName { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo5.DataLayr
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductImgage
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ImageName { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

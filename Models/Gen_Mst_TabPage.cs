//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gen_Mst_TabPage
    {
        public string ProductCode { get; set; }
        public string ModuleCode { get; set; }
        public string ScreenCode { get; set; }
        public string TabPagecode { get; set; }
        public string TabPageName { get; set; }
        public string Comments { get; set; }
        public Nullable<int> TabPageOrder { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int RowNum { get; set; }
    
        public virtual Gen_Mst_Screen Gen_Mst_Screen { get; set; }
    }
}

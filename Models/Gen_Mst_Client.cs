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
    
    public partial class Gen_Mst_Client
    {
        public string ProductCode { get; set; }
        public string ClientCode { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool SendConfirm { get; set; }
        public string MedazVersion { get; set; }
        public string Sqlversion { get; set; }
    
        public virtual Gen_Mst_Product Gen_Mst_Product { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster1 { get; set; }
    }
}

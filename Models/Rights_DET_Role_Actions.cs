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
    
    public partial class Rights_DET_Role_Actions
    {
        public string RoleId { get; set; }
        public string ScreenId { get; set; }
        public string ActionId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual Rights_MST_Action Rights_MST_Action { get; set; }
        public virtual Rights_MST_Role Rights_MST_Role { get; set; }
        public virtual Rights_MST_Screen Rights_MST_Screen { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster1 { get; set; }
    }
}
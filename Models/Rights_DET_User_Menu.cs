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
    
    public partial class Rights_DET_User_Menu
    {
        public string UserId { get; set; }
        public string MenuID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> AuditRecord { get; set; }
    
        public virtual Rights_MST_Menu Rights_MST_Menu { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster1 { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster2 { get; set; }
    }
}

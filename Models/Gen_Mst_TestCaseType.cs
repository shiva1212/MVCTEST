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
    
    public partial class Gen_Mst_TestCaseType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gen_Mst_TestCaseType()
        {
            this.Gen_Mst_GeneralTestCases = new HashSet<Gen_Mst_GeneralTestCases>();
        }
    
        public string TestCaseCode { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gen_Mst_GeneralTestCases> Gen_Mst_GeneralTestCases { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster1 { get; set; }
    }
}
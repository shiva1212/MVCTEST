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
    
    public partial class Employee_Mst_Personal
    {
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> Age { get; set; }
        public string ResAddr { get; set; }
        public string CommAddr { get; set; }
        public string Mobile { get; set; }
        public string LandLine { get; set; }
        public string Extension { get; set; }
        public string EmailID { get; set; }
        public string BloodGroup { get; set; }
        public string UGQualification { get; set; }
        public string PGQualification { get; set; }
        public string Team { get; set; }
        public string Designation { get; set; }
        public string BankAccNo { get; set; }
        public string BankName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster { get; set; }
        public virtual Rights_MST_UserMaster Rights_MST_UserMaster1 { get; set; }
    }
}
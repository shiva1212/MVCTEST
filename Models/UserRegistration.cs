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
    
    public partial class UserRegistration
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public Nullable<System.Guid> ActivationCode { get; set; }
    }
}

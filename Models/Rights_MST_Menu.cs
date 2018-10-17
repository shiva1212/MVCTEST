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
    
    public partial class Rights_MST_Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rights_MST_Menu()
        {
            this.Rights_DET_Role_Menu = new HashSet<Rights_DET_Role_Menu>();
            this.Rights_DET_User_Menu = new HashSet<Rights_DET_User_Menu>();
            this.Rights_MST_Menu1 = new HashSet<Rights_MST_Menu>();
            this.Rights_MST_Screen = new HashSet<Rights_MST_Screen>();
        }
    
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDesc { get; set; }
        public bool TopLevel { get; set; }
        public string ParentId { get; set; }
        public bool HasChild { get; set; }
        public string MenuObjectName { get; set; }
        public int MenuOrder { get; set; }
        public bool IsAvailable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rights_DET_Role_Menu> Rights_DET_Role_Menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rights_DET_User_Menu> Rights_DET_User_Menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rights_MST_Menu> Rights_MST_Menu1 { get; set; }
        public virtual Rights_MST_Menu Rights_MST_Menu2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rights_MST_Screen> Rights_MST_Screen { get; set; }
    }
}

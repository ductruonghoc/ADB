//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADB
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOPHAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOPHAN()
        {
            this.BOPHAN_CHINHANH = new HashSet<BOPHAN_CHINHANH>();
        }
    
        public byte MaBoPhan { get; set; }
        public string TenBoPhan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOPHAN_CHINHANH> BOPHAN_CHINHANH { get; set; }
    }
}
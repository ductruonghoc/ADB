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
    
    public partial class THUCDON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THUCDON()
        {
            this.MUCTHUCDONs = new HashSet<MUCTHUCDON>();
        }
    
        public int MaTD { get; set; }
        public string TenThucDon { get; set; }
        public string MoTaThucDon { get; set; }
        public byte MaCN { get; set; }
    
        public virtual CHINHANH CHINHANH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUCTHUCDON> MUCTHUCDONs { get; set; }
    }
}

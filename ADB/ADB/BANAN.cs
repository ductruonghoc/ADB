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
    
    public partial class BANAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BANAN()
        {
            this.BAN_BANAN = new HashSet<BAN_BANAN>();
            this.HOADONs = new HashSet<HOADON>();
            this.PHIEUDATs = new HashSet<PHIEUDAT>();
        }
    
        public int MaBanAn { get; set; }
        public Nullable<int> SoLuongKhach { get; set; }
        public Nullable<System.DateTime> ThoiGianDen { get; set; }
        public string GhiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAN_BANAN> BAN_BANAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDAT> PHIEUDATs { get; set; }
    }
}
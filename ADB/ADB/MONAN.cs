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
    
    public partial class MONAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONAN()
        {
            this.BANAN_MONAN = new HashSet<BANAN_MONAN>();
        }
    
        public int ID { get; set; }
        public int MaMuc { get; set; }
        public int MaMonAn { get; set; }
        public Nullable<double> Gia { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<bool> DangDung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANAN_MONAN> BANAN_MONAN { get; set; }
        public virtual KHOMONAN KHOMONAN { get; set; }
        public virtual MUCTHUCDON MUCTHUCDON { get; set; }
    }
}
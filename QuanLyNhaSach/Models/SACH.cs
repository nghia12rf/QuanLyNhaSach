namespace QuanLyNhaSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SACH")]
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            THAMGIAs = new HashSet<THAMGIA>();
        }

        [Key]
        [StringLength(5)]
        public string MASACH { get; set; }

        [StringLength(100)]
        public string TENSACH { get; set; }

        [Column(TypeName = "money")]
        public decimal? GIABAN { get; set; }

        [StringLength(500)]
        public string MOTA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYCAPNHAT { get; set; }

        [StringLength(50)]
        public string ANHBIA { get; set; }

        public int? SOLUONGTON { get; set; }

        [StringLength(5)]
        public string MACHUDE { get; set; }

        [StringLength(5)]
        public string MANXB { get; set; }

        public bool? MOI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        public virtual CHUDE CHUDE { get; set; }

        public virtual NHAXUATBAN NHAXUATBAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THAMGIA> THAMGIAs { get; set; }
    }
}

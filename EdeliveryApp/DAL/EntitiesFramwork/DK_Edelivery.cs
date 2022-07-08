namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DK_Edelivery
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public Guid IDEdelivery { get; set; }

        [StringLength(500)]
        public string MaKhachHang { get; set; }

        [StringLength(1000)]
        public string TenKhachHang { get; set; }

        [StringLength(50)]
        public string MaNoiGiaoNhan { get; set; }

        public string TenNoiGiaoNhan { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayGiaoNhan { get; set; }

        [StringLength(50)]
        public string MaDonViVanChuyen { get; set; }

        public string DonViVanChuyen { get; set; }

        [StringLength(50)]
        public string BienSo_SoHieu { get; set; }

        [StringLength(50)]
        public string RoMooc { get; set; }

        [StringLength(500)]
        public string TaiXe_ThuyenTruong { get; set; }

        [StringLength(50)]
        public string CMND_CCCD { get; set; }

        public DateTime? NgayCapCMND_CCCD { get; set; }

        [StringLength(50)]
        public string NoiCapCMND_CCCD { get; set; }

        [StringLength(50)]
        public string SONumber { get; set; }

        [StringLength(50)]
        public string SOItems { get; set; }

        [StringLength(50)]
        public string MaHangHoa { get; set; }

        [StringLength(500)]
        public string TenHangHoa { get; set; }

        public decimal? SOBC { get; set; }

        public decimal? SOCAY { get; set; }

        public decimal? SOCAYLE { get; set; }

        public decimal? TRONGLUONG { get; set; }

        public decimal? SOLUONGBEBO { get; set; }

        public Guid? VehicleKey { get; set; }

        public bool? IsDelete { get; set; }

        public int? IDCompanyCode { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string Instance { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }

        [StringLength(50)]
        public string SONumberBPM { get; set; }

        [StringLength(50)]
        public string NhaMaySanXuat { get; set; }

        [StringLength(500)]
        public string CreateUser { get; set; }

        [StringLength(500)]
        public string CreateUserId { get; set; }

        public string Note { get; set; }

        public DateTime? NgayTao { get; set; }

        public Guid rowguid { get; set; }

        [StringLength(100)]
        public string CTLXH { get; set; }
    }
}

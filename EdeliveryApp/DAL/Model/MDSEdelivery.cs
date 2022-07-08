using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MDSEdelivery
    {
        public Guid IDEdelivery { get; set; }

        public string MaKhachHang { get; set; }

        public string TenKhachHang { get; set; }

        public DateTime? NgayGiaoNhan { get; set; }

        public string TenNoiGiaoNhan { get; set; }

        public string DonViVanChuyen { get; set; }

        public decimal? Trongluongtong { get; set; }

        public string Status { get; set; }

        public string MaDonViVanChuyen { get; set; }

        public string MaNoiGiaoNhan { get; set; }

        public int? IDCompanyCode { get; set; }

        public DateTime? NgayTao { get; set; }
    }

    public class SearchMDSEdelivery
    {
        public string BranchCode { get; set; }

        public string MaKhachHang { get; set; }

        public string MaNoiGiaoNhan { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        public int? LoaiVanChuyen { get; set; }

        public int? TrangThai { get; set; }
    }
}

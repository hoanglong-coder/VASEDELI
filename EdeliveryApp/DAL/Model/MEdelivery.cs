using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MEdelivery
    {
        public string MAKHACHANG { get; set; }

        public string TENKHACHHANG { get; set; }

        public string TENKHACHHANG_MAKHACHANG { get; set; }

        public string MADONVIVANCHUYEN { get; set; }

        public string TENDONVIVANCHUYEN { get; set; }

        public string TENDONVIVANCHUYEN_MADONVIVANCHUYEN { get; set; }

    }

    /// <summary>
    /// Class tìm kiếm kế hoạch giao nhận
    /// </summary>
    public class SearchEdelivery
    {
        public string BranchCode { get; set; }

        public string MaKhachHang { get; set; }

        public string SONumber { get; set; }

        public string IDCompanyCode { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }


    }

    /// <summary>
    /// Class hiển thị dữ liệu kế hoạch giao nhận phía người dùng
    /// </summary>
    public class ViewKeHoachGiaoNhan
    {
        public Guid IDEdelivery { get; set; }

        public DateTime? NgayGiaoNhan { get; set; }

        public string SONumber { get; set; }

        public string SOItems { get; set; }

        public string BienSo_SoHieu { get; set; }

        public string RoMooc { get; set; }

        public string TaiXe_ThuyenTruong { get; set; }

        public string CMND_CCCD { get; set; }

        public string TenHangHoa { get; set; }

        public decimal? TRONGLUONG { get; set; }

        public decimal? SOBC { get; set; }

        public decimal? SOCAY { get; set; }

        public decimal? SOCAYLE { get; set; }

        public decimal? SOLUONGBEBO { get; set; }

        public string MaKhachHang { get; set; }

        public string TenKhachHang { get; set; }

        public string MaNoiGiaoNhan { get; set; }

        public string TenNoiGiaoNhan { get; set; }

        public string MaDonViVanChuyen { get; set; }

        public string DonViVanChuyen { get; set; }

        public DateTime? NgayCapCMND_CCCD { get; set; }

        public string NoiCapCMND_CCCD { get; set; }

        public string MaHangHoa { get; set; }

        public Guid? VehicleKey { get; set; }

        public int? IDCompanyCode { get; set; }

        public string Status { get; set; }

    }
}

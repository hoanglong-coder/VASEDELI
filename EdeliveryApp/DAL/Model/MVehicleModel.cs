using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MVehicleModel
    {
        public string VehicleId { get; set; }

        public string VehicleNumber { get; set; }

        public string Type { get; set; }

        public string KieuXe { get; set; }

        public string VehicleWeight { get; set; }

        public string TrongLuongDangKiem { get; set; }

        public decimal? TyLeVuot { get; set; }

        public string DonViVanChuyen { get; set; }

        public string Khoa { get; set; }

    }

    public class MVehicleModelSearch
    {
        public string VehicleNumber { get; set; }

        public int? Type { get; set; }

        public string DonViVanChuyen { get; set; }

        public int? KieuXe { get; set; }

        public bool? Khoa { get; set; }

        public bool? KhoaChinhSua { get; set; }

    }
}

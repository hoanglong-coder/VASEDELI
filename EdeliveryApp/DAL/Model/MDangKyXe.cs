using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MDangKyXe
    {
        public string BienSo_SoHieu { get; set; }

        public string TaiXe_ThuyenTruong { get; set; }

        public string CMND_CCCD { get; set; }

        public DateTime? NgayCapCMND_CCCD { get; set; }

        public string NoiCapCMND_CCCD { get; set; }

        public string Romooc { get; set; }

        public string SONumber { get; set; }

        public string SOItems { get; set; }

        public string MaHangHoa { get; set; }

        public string TenHangHoa { get; set; }

        public decimal? SOBC { get; set; }

        public decimal? SOCAY { get; set; }

        public decimal? SOCAYLE { get; set; }

        public decimal? TRONGLUONG { get; set; }

        public decimal? SOLUONGBEBO { get; set; }



        //Key phân biệt trùng xe nhưng khác sản phẩm
        public Guid VehicelID { get; set; }

        public bool? IsDelete { get; set; }

        public int? Status { get; set; }

        public string SONumberPBM { get; set; }

        public Guid rowguid { get; set; }

        public string Note { get; set; }

    }
}

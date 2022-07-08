using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public class ConstantVehicle
    {
        public const int NoiBo = 1; //Nội bộ

        public const int KhachHang = 2; //Khách hàng

        public const int DonViVanChuyen = 3;//Đơn vị vận chuyển

        public const int XeThuong = 0; //Xe thường

        public const int DauKeo = 1; //Xe đầu kéo

        public const int Romoc = 2; //Xe rơ móc
    }

    public class ConstantStatusDK_EDelivery
    {
        public const int DangChoDuyetChoPhepSua = 0; // Đang chờ phê duyệt

        public const int DangXacNhan = 1;//Đang xác nhận

        public const int DaXacNhan = 2;//Đã xác nhận

        public const int DangPheDuyet = 3;//Đang phê duyệt

        public const int PheDuyetThanhCong_HoanThanh = 4;//Phê Duyệt thành công - hoành thành

    }
    public class LoaiVanChuyen
    {
        public const int NoiBo = 0; // Loại vận chuyển nội bộ

        public const int KhachHang = 1;//Loại vận chuyển khách hàng
    }
}

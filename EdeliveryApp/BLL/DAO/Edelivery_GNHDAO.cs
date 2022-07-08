using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntitiesFramwork;
using DAL.Model;
using Dapper;

namespace BLL.DAO
{
    public class Edelivery_GNHDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        /// <summary>
        /// Lấy data khách hàng từ BPM trả về => chỉ load danh sách khách hàng của riêng branch code ví dụ 1000,3000,4000...
        /// </summary>
        /// <returns> danh sách khách hàng trong PBM</returns>
        /// 
        public IEnumerable<MEdelivery> GetThongTinKhachHang(string BranchCode)
        {
            using (DbConnection db = new DbConnection())
            {
                var edelivery_GNH = db.Edelivery_GNH.Where(t=>t.BranchCode==BranchCode).Select(t => t).GroupBy(t => t.MAKHACHANG).Select(t => t.FirstOrDefault());

                var result = new List<MEdelivery>();

                foreach (var item in edelivery_GNH)
                {

                    MEdelivery mEdelivery = new MEdelivery();

                    mEdelivery.MAKHACHANG = item.MAKHACHANG;
                    mEdelivery.TENKHACHHANG = item.TENKHACHHANG;
                    mEdelivery.TENKHACHHANG_MAKHACHANG = item.TENKHACHHANG + "_" + item.MAKHACHANG;

                    result.Add(mEdelivery);

                }
                return result.ToArray();
            }
        }

        public string[] GetThongTinKhachHangUsingDapper(object BranchCode, object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);
                p.Add("branchcode", (string)BranchCode);
                string[] arr = connection.Query<string>(@"select DISTINCT top 10 @text + ' | ' + CustomerName+ ' | ' + CustomerCode  from CustomerModel
                where (CustomerName + ' | ' + CustomerCode) like N'%' + @text + '%'", p).ToArray();

                return arr;
            }

        }

        public string[] GetSONumberUsingDapper(object branchCode, object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);
                p.Add("branchcode", (string)branchCode);
                string[] arr = connection.Query<string>(@"select DISTINCT top 10 @text + ' | ' + SONumber from Edelivery_GNH
                where BranchCode = @branchcode and SONumber like N'%' + @text + '%'", p).ToArray();

                return arr;
            }
        }


        /// <summary>
        /// Thêm xe cho SO
        /// </summary>
        /// <param name="BienSoXe"></param>
        /// <param name="Romooc"></param>
        /// <param name="CMND"></param>
        /// <param name="Hoten"></param>
        /// <param name="NoiCap"></param>
        /// <param name="NgayCap"></param>
        /// <returns></returns>
        public List<MDangKyXe> InsertThongTinTaiXe(string BienSoXe, string Romooc, string CMND, string Hoten, string NoiCap, DateTime NgayCap)
        {
            List<MDangKyXe> lstrs = new List<MDangKyXe>();


            MDangKyXe rs = new MDangKyXe();
            rs.BienSo_SoHieu = BienSoXe;
            rs.Romooc = Romooc;
            rs.CMND_CCCD = CMND;
            rs.TaiXe_ThuyenTruong = Hoten;
            rs.NoiCapCMND_CCCD = NoiCap;
            rs.NgayCapCMND_CCCD = NgayCap.Date;
            rs.VehicelID = Guid.NewGuid();

            lstrs.Add(rs);

            return lstrs;

        }

        public decimal GetSOBC(string SONumber, string MaHH)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Edelivery_GNH.Where(t => t.SONumber == SONumber && t.MAHANGHOA == MaHH).FirstOrDefault().SOBC.Value;
            }
           
        }

        public decimal GetSOCAYLE(string SONumber, string MaHH)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Edelivery_GNH.Where(t => t.SONumber == SONumber && t.MAHANGHOA == MaHH).FirstOrDefault().SOCAYLE.Value;

            }
        }

        public decimal GetSOLUONGBEBO(string SONumber, string MaHH)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Edelivery_GNH.Where(t => t.SONumber == SONumber && t.MAHANGHOA == MaHH).FirstOrDefault().SOLUONGBEBO.Value;
            }

        }

        public decimal GetTrongLuong(string SONumber, string MaHH)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.Edelivery_GNH.Where(t => t.SONumber == SONumber && t.MAHANGHOA == MaHH).FirstOrDefault().TRONGLUONG.Value;
            }
        }

       

        /// <summary>
        /// Lấy data đơn hàng/mặt hàng theo khách hàng
        /// </summary>
        /// <param name="MaKhachHang">Mã khách hàng</param>
        /// <returns>danh sách các đơn hàng/mặt hàng của khách hàng</returns>
        public List<MEdelivery_GNH> LoadDsMatHang(string MaKhachHang,string BranchCode,string NoiGiaoNhanHang, List<MDangKyXe> input, string Search = "")
        {

            //using (DbConnection db = new DbConnection())
            //{
            //    ////list edelivery đã đăng ký
            //    //var dk = db.DK_Edelivery.Select(t => t).Where(t => t.MaKhachHang == MaKhachHang && t.BranchCode == BranchCode && t.NhaMaySanXuat == NoiGiaoNhanHang && t.IsDelete == false).ToList();

            //    //List<MDangKyXe> kq = new List<MDangKyXe>();

            //    //foreach (var item in dk)
            //    //{
            //    //    MDangKyXe t = new MDangKyXe();

            //    //    t.BienSo_SoHieu = item.BienSo_SoHieu;
            //    //    t.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
            //    //    t.CMND_CCCD = item.CMND_CCCD;
            //    //    t.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;
            //    //    t.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
            //    //    t.Romooc = item.RoMooc;
            //    //    t.SONumber = item.SONumber;
            //    //    t.SONumberPBM = item.SONumberBPM;
            //    //    t.SOItems = item.SOItems;
            //    //    t.MaHangHoa = item.MaHangHoa;
            //    //    t.TenHangHoa = item.TenHangHoa;
            //    //    t.SOBC = item.SOBC;
            //    //    t.SOCAY = item.SOCAY;
            //    //    t.SOCAYLE = item.SOCAYLE;
            //    //    t.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3); //sửa trọng lượng

            //    //    var a = Math.Round((double)item.TRONGLUONG, 0);

            //    //    t.SOLUONGBEBO = item.SOLUONGBEBO;

            //    //    kq.Add(t);
            //    //}


            //    //var inputs = input.Concat(kq).ToList();


            //    //danh sách mặt hàng lấy từ data
            //    var ds = db.Edelivery_GNH.Select(t => t).Where(t => t.MAKHACHANG == MaKhachHang && t.BranchCode == BranchCode && t.NhaMaySanXuat == NoiGiaoNhanHang).ToList();

            //    List<MEdelivery_GNH> dsout = new List<MEdelivery_GNH>();
            //    foreach (var item in ds)
            //    {

            //        MEdelivery_GNH edelivery_GNH_Test = new MEdelivery_GNH();
            //        edelivery_GNH_Test.ID = item.ID;
            //        edelivery_GNH_Test.SONumber = item.SONumber;
            //        edelivery_GNH_Test.SONumberBPM = item.SONumberBPM;
            //        edelivery_GNH_Test.SOItems = item.SOItems;
            //        edelivery_GNH_Test.MADONVICUNGCAP = item.MADONVICUNGCAP;
            //        edelivery_GNH_Test.TENDONVICUNGCAP = item.TENDONVICUNGCAP;
            //        edelivery_GNH_Test.MAKHACHANG = item.MAKHACHANG;
            //        edelivery_GNH_Test.TENKHACHHANG = item.TENKHACHHANG;
            //        edelivery_GNH_Test.MAHANGHOA = item.MAHANGHOA;
            //        edelivery_GNH_Test.TENHANGHOA = item.TENHANGHOA;
            //        edelivery_GNH_Test.SOBC = item.SOBC;
            //        edelivery_GNH_Test.SOCAY = item.SOCAY;
            //        edelivery_GNH_Test.SOCAYLE = item.SOCAYLE;
            //        edelivery_GNH_Test.TRONGLUONG = LayTrongLuongBanDau(edelivery_GNH_Test.MAHANGHOA, edelivery_GNH_Test.SONumber);//sử dụng dapper nhanh hơn
            //        //edelivery_GNH_Test.DAKHAIBAO = Math.Round(TrongLuongKhaiBao(inputs, edelivery_GNH_Test.SONumber, edelivery_GNH_Test.MAHANGHOA), 3);    
            //        edelivery_GNH_Test.CONLAI = db.Edelivery_GNH.Where(t => t.MAHANGHOA == edelivery_GNH_Test.MAHANGHOA && t.SONumber == edelivery_GNH_Test.SONumber).FirstOrDefault().TRONGLUONG;
            //        edelivery_GNH_Test.DAKHAIBAO = (decimal)Math.Round((double)(edelivery_GNH_Test.TRONGLUONG.Value - edelivery_GNH_Test.CONLAI), 3);
            //        dsout.Add(edelivery_GNH_Test);
            //    }

            //    return dsout;
            //}


            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("MaKhachHang", MaKhachHang);
                p.Add("BranchCode", BranchCode);
                p.Add("NoiGiaoNhanHang", NoiGiaoNhanHang);
                p.Add("Search", Search);

                var arr = connection.Query<Edelivery_GNH>(@"SELECT * FROM Edelivery_GNH WHERE MAKHACHANG = @MaKhachHang AND TRONGLUONG >= 0 AND BranchCode = @BranchCode AND NhaMaySanXuat = @NoiGiaoNhanHang AND (SONumber + ' | ' + SONumberBPM) like + '%' + @Search + '%'", p).ToList();

                List<MEdelivery_GNH> dsout = new List<MEdelivery_GNH>();
                foreach (var item in arr.OrderByDescending(t=>t.CreateDate))
                {

                    MEdelivery_GNH edelivery_GNH_Test = new MEdelivery_GNH();
                    edelivery_GNH_Test.ID = item.ID;
                    edelivery_GNH_Test.SONumber = item.SONumber;
                    edelivery_GNH_Test.SONumberBPM = item.SONumberBPM;
                    edelivery_GNH_Test.SOItems = item.SOItems;
                    edelivery_GNH_Test.MADONVICUNGCAP = item.MADONVICUNGCAP;
                    edelivery_GNH_Test.TENDONVICUNGCAP = item.TENDONVICUNGCAP;
                    edelivery_GNH_Test.MAKHACHANG = item.MAKHACHANG;
                    edelivery_GNH_Test.TENKHACHHANG = item.TENKHACHHANG;
                    edelivery_GNH_Test.MAHANGHOA = item.MAHANGHOA;
                    edelivery_GNH_Test.TENHANGHOA = item.TENHANGHOA;
                    edelivery_GNH_Test.SOBC = item.SOBC;
                    edelivery_GNH_Test.SOCAY = item.SOCAY;
                    edelivery_GNH_Test.SOCAYLE = item.SOCAYLE;
                    edelivery_GNH_Test.TRONGLUONG = LayTrongLuongBanDau(edelivery_GNH_Test.MAHANGHOA, edelivery_GNH_Test.SONumber);//sử dụng dapper nhanh hơn
                                                                                                                                   //edelivery_GNH_Test.DAKHAIBAO = Math.Round(TrongLuongKhaiBao(inputs, edelivery_GNH_Test.SONumber, edelivery_GNH_Test.MAHANGHOA), 3);    
                    var p2 = new DynamicParameters();
                    p2.Add("MAHANGHOA", edelivery_GNH_Test.MAHANGHOA);
                    p2.Add("SONumber", edelivery_GNH_Test.SONumber);


                    //edelivery_GNH_Test.CONLAI = connection.Query<Edelivery_GNH>(@"SELECT * FROM Edelivery_GNH WHERE MAHANGHOA = @MAHANGHOA AND SONumber = @SONumber", p2).FirstOrDefault().TRONGLUONG;

                    edelivery_GNH_Test.CONLAI = item.TRONGLUONG;

                    edelivery_GNH_Test.DAKHAIBAO = (decimal)Math.Round((double)(edelivery_GNH_Test.TRONGLUONG.Value - edelivery_GNH_Test.CONLAI), 3);
                    dsout.Add(edelivery_GNH_Test);
                }

                return dsout;
            }

        }

        public decimal LayTrongLuongBanDau(string MAHANGHOA, string SONumber)
        {
            using (DbConnection db = new DbConnection())
            {
                var trongluong = db.SOLineModels.Where(t => t.ProductCode == MAHANGHOA && t.SONumber == SONumber);

                if (trongluong.Count() != 0)
                {
                    return Math.Round(trongluong.FirstOrDefault().Qty.Value / 1000 ,3);

                }else
                {
                    return 0;
                }
            }

        }


        /// <summary>
        /// Lấy data đơn hàng/mặt hàng theo khách hàng để sửa
        /// </summary>
        /// <param name="MaKhachHang">Mã khách hàng</param>
        /// <returns>danh sách các đơn hàng/mặt hàng của khách hàng</returns>
        public List<MEdelivery_GNH> LoadDsMatHangSua(string MaKhachHang, string BranchCode, string NoiGiaoNhanHang, List<MDangKyXe> input, string Search = "")
        {

            //using (DbConnection db = new DbConnection())
            //{
            //    //list edelivery đã đăng ký
            //    //var dk = db.DK_Edelivery.Select(t => t).Where(t => t.MaKhachHang == MaKhachHang && t.BranchCode == BranchCode && t.NhaMaySanXuat == NoiGiaoNhanHang && t.IsDelete == false &&t.IDEdelivery!=edelivery).ToList();

            //    //List<MDangKyXe> kq = new List<MDangKyXe>();


            //    //foreach (var item in dk)
            //    //{
            //    //    MDangKyXe t = new MDangKyXe();

            //    //    t.BienSo_SoHieu = item.BienSo_SoHieu;
            //    //    t.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
            //    //    t.CMND_CCCD = item.CMND_CCCD;
            //    //    t.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;
            //    //    t.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
            //    //    t.Romooc = item.RoMooc;
            //    //    t.SONumber = item.SONumber;
            //    //    t.SONumberPBM = item.SONumberBPM;
            //    //    t.SOItems = item.SOItems;
            //    //    t.MaHangHoa = item.MaHangHoa;
            //    //    t.TenHangHoa = item.TenHangHoa;
            //    //    t.SOBC = item.SOBC;
            //    //    t.SOCAY = item.SOCAY;
            //    //    t.SOCAYLE = item.SOCAYLE;
            //    //    t.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3);//sửa trọng lượng

            //    //    var a = Math.Round((double)item.TRONGLUONG, 0);

            //    //    t.SOLUONGBEBO = item.SOLUONGBEBO;

            //    //    kq.Add(t);
            //    //}

            //    var inputs = input.Concat(kq).ToList();

            //    //danh sách mặt hàng lấy từ data
            //    var ds = db.Edelivery_GNH.Select(t => t).Where(t => t.MAKHACHANG == MaKhachHang && t.BranchCode == BranchCode && t.NhaMaySanXuat == NoiGiaoNhanHang).ToList();

            //    List<MEdelivery_GNH> dsout = new List<MEdelivery_GNH>();
            //    foreach (var item in ds)
            //    {


            //        MEdelivery_GNH edelivery_GNH_Test = new MEdelivery_GNH();
            //        edelivery_GNH_Test.ID = item.ID;
            //        edelivery_GNH_Test.SONumber = item.SONumber;
            //        edelivery_GNH_Test.SONumberBPM = item.SONumberBPM;
            //        edelivery_GNH_Test.SOItems = item.SOItems;
            //        edelivery_GNH_Test.MADONVICUNGCAP = item.MADONVICUNGCAP;
            //        edelivery_GNH_Test.TENDONVICUNGCAP = item.TENDONVICUNGCAP;
            //        edelivery_GNH_Test.MAKHACHANG = item.MAKHACHANG;
            //        edelivery_GNH_Test.TENKHACHHANG = item.TENKHACHHANG;
            //        edelivery_GNH_Test.MAHANGHOA = item.MAHANGHOA;
            //        edelivery_GNH_Test.TENHANGHOA = item.TENHANGHOA;
            //        edelivery_GNH_Test.SOBC = item.SOBC;
            //        edelivery_GNH_Test.SOCAY = item.SOCAY;
            //        edelivery_GNH_Test.SOCAYLE = item.SOCAYLE;
            //        edelivery_GNH_Test.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3);//sửa trọng lượng
            //        edelivery_GNH_Test.DAKHAIBAO = Math.Round(TrongLuongKhaiBao(kq, input, edelivery_GNH_Test.SONumber, edelivery_GNH_Test.MAHANGHOA, edelivery_GNH_Test.TRONGLUONG), 3);
            //        edelivery_GNH_Test.CONLAI = (decimal)Math.Round((double)(edelivery_GNH_Test.TRONGLUONG.Value - edelivery_GNH_Test.DAKHAIBAO), 3);
            //        dsout.Add(edelivery_GNH_Test);
            //    }

            //    return dsout;
            //}

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("MaKhachHang", MaKhachHang);
                p.Add("BranchCode", BranchCode);
                p.Add("NoiGiaoNhanHang", NoiGiaoNhanHang);
                p.Add("Search", Search);

                var arr = connection.Query<Edelivery_GNH>(@"SELECT * FROM Edelivery_GNH WHERE MAKHACHANG = @MaKhachHang  AND TRONGLUONG >= 0 AND BranchCode = @BranchCode  AND NhaMaySanXuat = @NoiGiaoNhanHang AND (SONumber + ' | ' + SONumberBPM) like + '%' + @Search + '%'", p).ToList();

                List<MEdelivery_GNH> dsout = new List<MEdelivery_GNH>();
                foreach (var item in arr.OrderByDescending(t => t.CreateDate))
                {

                    MEdelivery_GNH edelivery_GNH_Test = new MEdelivery_GNH();
                    edelivery_GNH_Test.ID = item.ID;
                    edelivery_GNH_Test.SONumber = item.SONumber;
                    edelivery_GNH_Test.SONumberBPM = item.SONumberBPM;
                    edelivery_GNH_Test.SOItems = item.SOItems;
                    edelivery_GNH_Test.MADONVICUNGCAP = item.MADONVICUNGCAP;
                    edelivery_GNH_Test.TENDONVICUNGCAP = item.TENDONVICUNGCAP;
                    edelivery_GNH_Test.MAKHACHANG = item.MAKHACHANG;
                    edelivery_GNH_Test.TENKHACHHANG = item.TENKHACHHANG;
                    edelivery_GNH_Test.MAHANGHOA = item.MAHANGHOA;
                    edelivery_GNH_Test.TENHANGHOA = item.TENHANGHOA;
                    edelivery_GNH_Test.SOBC = item.SOBC;
                    edelivery_GNH_Test.SOCAY = item.SOCAY;
                    edelivery_GNH_Test.SOCAYLE = item.SOCAYLE;
                    edelivery_GNH_Test.TRONGLUONG = LayTrongLuongBanDau(edelivery_GNH_Test.MAHANGHOA, edelivery_GNH_Test.SONumber);//sử dụng dapper nhanh hơn
                                                                                                                                   //edelivery_GNH_Test.DAKHAIBAO = Math.Round(TrongLuongKhaiBao(inputs, edelivery_GNH_Test.SONumber, edelivery_GNH_Test.MAHANGHOA), 3);    
                    var p2 = new DynamicParameters();
                    p2.Add("MAHANGHOA", edelivery_GNH_Test.MAHANGHOA);
                    p2.Add("SONumber", edelivery_GNH_Test.SONumber);


                    edelivery_GNH_Test.CONLAI = connection.Query<Edelivery_GNH>(@"SELECT * FROM Edelivery_GNH WHERE MAHANGHOA = @MAHANGHOA AND SONumber = @SONumber", p2).FirstOrDefault().TRONGLUONG;


                    edelivery_GNH_Test.DAKHAIBAO = (decimal)Math.Round((double)(edelivery_GNH_Test.TRONGLUONG.Value - edelivery_GNH_Test.CONLAI), 3);
                    dsout.Add(edelivery_GNH_Test);
                }

                return dsout;
            }
        }


        /// <summary>
        /// Trả về trọng lượng đã đăng ký 
        /// </summary>
        /// <param name="lstinput">danh sách edelivery đã đăng ký</param>
        /// <param name="MaSO">Mã đơn hàng</param>
        /// <param name="MaMH">Mã mặt hàng</param>
        /// <returns>số trọng lượng</returns>
        public decimal TrongLuongKhaiBao(List<MDangKyXe> lstinput, string MaSO, string MaMH)
        {
            decimal trongluong = 0;

            foreach (var item in lstinput)
            {
                if (item.SONumber == MaSO && item.MaHangHoa == MaMH)
                    trongluong = trongluong + item.TRONGLUONG.Value;
            }

            return trongluong;

        }

        /// <summary>
        /// Trả về trọng lượng đã đăng ký sửa mặt hàng
        /// </summary>
        /// <param name="lstinput">danh sách edelivery đã đăng ký</param>
        /// <param name="MaSO">Mã đơn hàng</param>
        /// <param name="MaMH">Mã mặt hàng</param>
        /// <returns>số trọng lượng</returns>
        public decimal TrongLuongKhaiBao(List<MDangKyXe> lstEdelivery, List<MDangKyXe> lsttemp, string MaSO, string MaMH, decimal? trongluongppm)
        {
            decimal trongluong = 0;

            if (KiemTraMatHang(lsttemp, MaSO, MaMH))
            {
                var lst = lsttemp.Concat(lstEdelivery).ToList();
                foreach (var item in lst)
                {
                    if (item.SONumber == MaSO && item.MaHangHoa == MaMH)
                    {
                        trongluong += item.TRONGLUONG.Value;
                    }
                }
                decimal ketqua = trongluong - trongluongppm.Value;
                return trongluong;
            }
            else
            {
                foreach (var item in lstEdelivery)
                {
                    if (item.SONumber == MaSO && item.MaHangHoa == MaMH)
                    {
                        trongluong += item.TRONGLUONG.Value;
                    }
                }
                return trongluong;
            }

        }

        /// <summary>
        /// Kiểm tra mặt hàng có trong danh sách đăng ký tạm trên form
        /// </summary>
        /// <param name="lsttemp">danh sách đăng ký tạm</param>
        /// <param name="MaSO">Mã đơn hàng</param>
        /// <param name="MaMH">Mã mặt hàng</param>
        /// <returns>True => có mặt hàng này trong danh sách tạm | ngược lại không có</returns>
        public bool KiemTraMatHang(List<MDangKyXe> lsttemp, string MaSO, string MaMH)
        {
            foreach (var item in lsttemp)
            {
                if (item.SONumber == MaSO && item.MaHangHoa == MaMH)
                {
                    return true;
                }
            }

            return false;
        }


        ///// <summary>
        ///// Tìm kiếm danh sách mặt hàng theo đơn hàng
        ///// </summary>
        ///// <param name="MaKhachHang"></param>
        ///// <param name="SO"> Mã đơn hàng</param>
        ///// <returns></returns>
        //public List<MEdelivery_GNH> LoadDsMatHang(string MaKhachHang,string BrandCOde, List<MDangKyXe> input, string SO)
        //{

        //    using (DbConnection db = new DbConnection())
        //    {
        //        //list edelivery đã đăng ký
        //        var dk = db.DK_Edelivery.Select(t => t).Where(t => t.MaKhachHang == MaKhachHang && t.BranchCode==BrandCOde && t.IsDelete == false&&t.SONumber.Contains(SO)).ToList();

        //        List<MDangKyXe> kq = new List<MDangKyXe>();

        //        foreach (var item in dk)
        //        {
        //            MDangKyXe t = new MDangKyXe();

        //            t.BienSo_SoHieu = item.BienSo_SoHieu;
        //            t.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
        //            t.CMND_CCCD = item.CMND_CCCD;
        //            t.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;
        //            t.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
        //            t.Romooc = item.RoMooc;
        //            t.SONumber = item.SONumber;
        //            t.SOItems = item.SOItems;
        //            t.MaHangHoa = item.MaHangHoa;
        //            t.TenHangHoa = item.TenHangHoa;
        //            t.SOBC = item.SOBC;
        //            t.SOCAY = item.SOCAY;
        //            t.SOCAYLE = item.SOCAYLE;
        //            t.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3);//sửa trọng lượng

        //            var a = Math.Round((double)item.TRONGLUONG, 0);

        //            t.SOLUONGBEBO = item.SOLUONGBEBO;

        //            kq.Add(t);
        //        }


        //        var inputs = input.Concat(kq).ToList();


        //        //danh sách mặt hàng lấy từ data
        //        var ds = db.Edelivery_GNH.Select(t => t).Where(t => t.MAKHACHANG == MaKhachHang && t.BranchCode == BrandCOde && t.SONumber.Contains(SO)).ToList();

        //        List<MEdelivery_GNH> dsout = new List<MEdelivery_GNH>();
        //        foreach (var item in ds)
        //        {

        //            MEdelivery_GNH edelivery_GNH_Test = new MEdelivery_GNH();
        //            edelivery_GNH_Test.ID = item.ID;
        //            edelivery_GNH_Test.SONumber = item.SONumber;
        //            edelivery_GNH_Test.SOItems = item.SOItems;
        //            edelivery_GNH_Test.MADONVICUNGCAP = item.MADONVICUNGCAP;
        //            edelivery_GNH_Test.TENDONVICUNGCAP = item.TENDONVICUNGCAP;
        //            edelivery_GNH_Test.MAKHACHANG = item.MAKHACHANG;
        //            edelivery_GNH_Test.TENKHACHHANG = item.TENKHACHHANG;
        //            edelivery_GNH_Test.MAHANGHOA = item.MAHANGHOA;
        //            edelivery_GNH_Test.TENHANGHOA = item.TENHANGHOA;
        //            edelivery_GNH_Test.SOBC = item.SOBC;
        //            edelivery_GNH_Test.SOCAY = item.SOCAY;
        //            edelivery_GNH_Test.SOCAYLE = item.SOCAYLE;
        //            edelivery_GNH_Test.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3); // sửa trọng lượng
        //            edelivery_GNH_Test.DAKHAIBAO = Math.Round(TrongLuongKhaiBao(inputs, edelivery_GNH_Test.SONumber, edelivery_GNH_Test.MAHANGHOA), 3);
        //            edelivery_GNH_Test.CONLAI = (decimal)Math.Round((double)(edelivery_GNH_Test.TRONGLUONG.Value - edelivery_GNH_Test.DAKHAIBAO), 3);
        //            dsout.Add(edelivery_GNH_Test);
        //        }

        //        return dsout;
        //    }

        //}

        /// <summary>
        /// Thêm sản phẩm vào xe
        /// </summary>
        /// <param name="input">danh sách Edilivery chỉ có biển số xe</param>
        /// <param name="MaDonHang">Mã Đơn hàng</param>
        /// <param name="MaMatHang">Mặt mặt hàng</param>
        /// <param name="BienSoXe">Biển số xe</param>
        /// <returns></returns>
        public List<MDangKyXe> InsertThongTinSO(string MaDonHangSAP,string MaDonHangBPM, string MaMatHang, string BienSoXe,string Romooc, string tenhang, string soitem, string tentx, string cmnd, DateTime ngaycap, string noicap, Guid vehicelID, Guid UserId)
        {

            List<MDangKyXe> lstrs = new List<MDangKyXe>();

            MDangKyXe rs = new MDangKyXe();
            rs.TaiXe_ThuyenTruong = tentx;
            rs.CMND_CCCD = cmnd;
            rs.NgayCapCMND_CCCD = ngaycap;
            rs.NoiCapCMND_CCCD = noicap;
            rs.SONumber = MaDonHangSAP;
            rs.SONumberPBM = MaDonHangBPM;
            rs.MaHangHoa = MaMatHang;
            rs.BienSo_SoHieu = BienSoXe;
            rs.Romooc = Romooc;
            rs.TenHangHoa = tenhang;
            rs.TRONGLUONG = 0;
            rs.SOItems = soitem;
            rs.SOBC = 0;
            rs.SOCAY = 0;
            rs.SOCAYLE = 0;
            rs.SOLUONGBEBO = 0;
            rs.VehicelID = vehicelID;
            rs.Note = "";
            lstrs.Add(rs);


            //Thêm UserLockId vào khi chọn SO
            using (DbConnection db = new DbConnection())
            {
                var EDelivery_GNH = db.Edelivery_GNH.Where(e => e.SONumber == MaDonHangSAP && e.MAHANGHOA == MaMatHang).FirstOrDefault();


                EDelivery_GNH.UserLockId = UserId.ToString();

                db.SaveChanges();

            }
           

            return lstrs;

        }
        /// <summary>
        /// Xóa SO + Mặt hàng ra khỏi Xe
        /// </summary>
        /// <param name="mDangKyXes"></param>
        /// <param name="biensoxe"></param>
        /// <param name="SOnumber"></param>
        /// <param name="MaMh"></param>
        /// <param name="vehicleid"></param>
        /// <returns></returns>
        public List<MDangKyXe> DeleteThinTinSo(ref List<MDangKyXe> mDangKyXes, string biensoxe, string SOnumber, string MaMh, Guid vehicleid)
        {
            var obj = mDangKyXes.Where(t => t.BienSo_SoHieu == biensoxe && t.SONumber == SOnumber && t.MaHangHoa == MaMh && t.VehicelID == vehicleid).FirstOrDefault();

            
            mDangKyXes.Remove(obj);
            //Xóa UserLockId
            //Nếu trong danh sách đăng ký vẫn còn SO + Mặt hàng đó thì không xóa UserLockId
            //Ngược lại trong danh sách đăng ký không còn SO + Mặt hàng đó thì xóa UserLockId
            using (DbConnection db = new DbConnection())
            {
                var lstdk = mDangKyXes.Where(t => t.SONumber == SOnumber && t.MaHangHoa == MaMh).ToList();

                //Kiểm tra nếu trong danh sách không còn SO + mặt hàng thì UserLockId = null
                if (lstdk.Count == 0)
                {
                    var SOItems = db.Edelivery_GNH.Where(t => t.SONumber == SOnumber && t.MAHANGHOA == MaMh).FirstOrDefault();

                    SOItems.UserLockId = null;

                    db.SaveChanges();
                }      

            }

            return mDangKyXes;

        }

        /// <summary>
        /// Xóa xe ra khỏi đăng ký
        /// </summary>
        /// <param name="lstxe">xe đăng ký</param>
        /// <param name="lstMH">danh sách mặt hàng</param>
        /// <param name="BienSoXe">MẶT HÀNG THEO BIỂN SỐ</param>
        /// <returns></returns>
        public void DeleteXe(ref List<MDangKyXe> lstxe, ref List<MDangKyXe> lstMH, string BienSoXe, string VehicleID)
        {

            var obj = lstxe.Where(t => t.BienSo_SoHieu == BienSoXe&&t.VehicelID == Guid.Parse(VehicleID)).FirstOrDefault();

            lstxe.Remove(obj);

            var obj2 = lstMH.Where(t => t.BienSo_SoHieu == BienSoXe && t.VehicelID == Guid.Parse(VehicleID)).ToList();

            foreach (var item in obj2)
            {
                //Sau khi xóa item đó khỏi danh sách THÌ KIỂM TRA

                lstMH.Remove(item);

                using (DbConnection db = new DbConnection())
                {
                    // XEM COI DANH SÁCH ĐÓ CÓ SO + mặt hàng đó nữa hay không 
                    var obj3 = lstMH.Where(t => t.SONumber == item.SONumber && t.MaHangHoa == item.MaHangHoa).Count();

                    //nếu không còn thì xóa
                    if (obj3 == 0)
                    {
                        var SOItems = db.Edelivery_GNH.Where(t => t.SONumber == item.SONumber && t.MAHANGHOA == item.MaHangHoa).FirstOrDefault();

                        SOItems.UserLockId = null;

                        db.SaveChanges();
                    }
                }
              
            }

            
        }

        public string[] GetDonViVanChuyenKhachHangUsingDapper(object BranchCode, object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);
                p.Add("branchcode", (string)BranchCode);
                string[] arr = connection.Query<string>(@"select DISTINCT top 10 @text + ' | ' + TENKHACHHANG+ ' | ' +'C'+ MAKHACHANG  from Edelivery_GNH
                where BranchCode = @branchcode and (TENKHACHHANG + ' | ' + MAKHACHANG) like N'%' + @text + '%'", p).ToArray();

                return arr;
            }

        }

        /// <summary>
        /// Kiểm tra SO đó có đang mở bởi User nào hay không
        /// </summary>
        /// <param name="SONumber">Mã đơn hàng</param>
        /// <returns>True = đang giữ SO | False = không có user nào đang giữ</returns>
        public UserModel KiemTraSONumberLockByUserId(string SONumber, string MaHangHoa)
        {
            using (DbConnection db = new DbConnection())
            {

                var SOItems = db.Edelivery_GNH.Where(t => t.SONumber == SONumber && t.MAHANGHOA == MaHangHoa).FirstOrDefault();

                if (SOItems.UserLockId == null)
                {
                    return null;
                }
                else
                {
                    return db.UserModels.Find(Guid.Parse(SOItems.UserLockId));
                }

            }
        }

        /// <summary>
        /// Xóa tất cả UserID của SO đang được user đăng nhập nắm giữ
        /// </summary>
        /// <param name="UserId"></param>
        public void DeleteUserLockId(string UserId)
        {
            using (DbConnection db = new DbConnection())
            {

                var SOLine = db.Edelivery_GNH.Where(t => t.UserLockId == UserId);


                foreach (var item in SOLine)
                {
                    item.UserLockId = null;
                }

                db.SaveChanges();

            }
        }
    }
}

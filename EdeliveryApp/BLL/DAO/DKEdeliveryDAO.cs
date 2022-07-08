using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.EntitiesFramwork;
using DAL.Model;
using PagedList;
namespace BLL.DAO
{
    public class DKEdeliveryDAO
    {
        /// <summary>
        /// Số bó cuộng đẵ đăng ký
        /// </summary>
        /// <returns></returns>
        public decimal SoBCDaDK(List<MDangKyXe> input, string MaDH, string MaHH, string BienSoXe)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal sobc = 0;
                var litst = db.DK_Edelivery.Where(t => t.MaHangHoa == MaHH && t.SONumber == MaDH && t.IsDelete == false).ToList();

                var listsosnh = new List<MDangKyXe>();


                foreach (var item in litst)
                {
                    MDangKyXe mDangKyXe = new MDangKyXe();

                    mDangKyXe.MaHangHoa = item.MaHangHoa;

                    mDangKyXe.SONumber = item.SONumber;

                    mDangKyXe.SOBC = item.SOBC;

                    mDangKyXe.BienSo_SoHieu = item.BienSo_SoHieu;


                    listsosnh.Add(mDangKyXe);


                }


                listsosnh = listsosnh.Concat(input).ToList();


                foreach (var item in listsosnh)
                {
                    //kiểm tra bằng mã hàng và bằng đơn hàng(so) và không cần so biển số phải khác vì trong một xe đã không thể thêm mặt hàng trùng
                    if (item.MaHangHoa == MaHH && item.SONumber == MaDH)
                    {
                        sobc += item.SOBC.Value;
                    }
                }

                return sobc;
            }

        }

        /// <summary>
        /// Số cây lẽ đẵ đăng ký
        /// </summary>
        /// <returns></returns>
        public decimal SoCayLeDaDK(List<MDangKyXe> input, string MaDH, string MaHH, string BienSoXe)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal socayle = 0;
                var litst = db.DK_Edelivery.Where(t => t.MaHangHoa == MaHH && t.SONumber == MaDH && t.IsDelete == false).ToList();

                var listsosnh = new List<MDangKyXe>();


                foreach (var item in litst)
                {
                    MDangKyXe mDangKyXe = new MDangKyXe();

                    mDangKyXe.MaHangHoa = item.MaHangHoa;

                    mDangKyXe.SONumber = item.SONumber;

                    mDangKyXe.SOBC = item.SOBC;

                    mDangKyXe.BienSo_SoHieu = item.BienSo_SoHieu;

                    mDangKyXe.SOCAYLE = item.SOCAYLE;


                    listsosnh.Add(mDangKyXe);


                }


                listsosnh = listsosnh.Concat(input).ToList();


                foreach (var item in listsosnh)
                {

                    if (item.MaHangHoa == MaHH && item.SONumber == MaDH)
                    {
                        socayle += item.SOCAYLE.Value;
                    }
                }

                return socayle;
            }

        }
        /// <summary>
        /// Số lượng bẻ bó đã đăng ký
        /// </summary>
        /// <returns></returns>
        public decimal SoLuongBeBoDaDK(List<MDangKyXe> input, string MaDH, string MaHH, string BienSoXe)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal soluongbebo = 0;
                var litst = db.DK_Edelivery.Where(t => t.MaHangHoa == MaHH && t.SONumber == MaDH && t.IsDelete == false).ToList();

                var listsosnh = new List<MDangKyXe>();


                foreach (var item in litst)
                {
                    MDangKyXe mDangKyXe = new MDangKyXe();

                    mDangKyXe.MaHangHoa = item.MaHangHoa;

                    mDangKyXe.SONumber = item.SONumber;

                    mDangKyXe.SOBC = item.SOBC;

                    mDangKyXe.SOCAYLE = item.SOCAYLE;

                    mDangKyXe.SOLUONGBEBO = item.SOLUONGBEBO;

                    mDangKyXe.BienSo_SoHieu = item.BienSo_SoHieu;


                    listsosnh.Add(mDangKyXe);


                }


                listsosnh = listsosnh.Concat(input).ToList();


                foreach (var item in listsosnh)
                {
                    //kiểm tra bằng mã hàng và bằng đơn hàng(so) và không cần so biển số phải khác vì trong một xe đã không thể thêm mặt hàng trùng
                    if (item.MaHangHoa == MaHH && item.SONumber == MaDH)
                    {
                        soluongbebo += item.SOLUONGBEBO.Value;
                    }
                }

                return soluongbebo;
            }

        }



        /// <summary>
        /// Số bó cuộng đẵ đăng ký(Sửa đăng ký)
        /// </summary>
        /// <returns></returns>
        public decimal SoTRONGLUONGDaDKSuaDangKy(string MaDH, string MaHH, Guid VehicleId ,Guid EdeliveryID)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal trongluong = 0;
                var litst = db.DK_Edelivery.Where(t => t.MaHangHoa == MaHH && t.SONumber == MaDH && t.IsDelete == false && t.IDEdelivery == EdeliveryID && t.VehicleKey == VehicleId).ToList();

                trongluong = litst.Sum(t => t.TRONGLUONG).Value;

                return trongluong;
            }

        }

        /// <summary>
        /// Số cây lẽ đẵ đăng ký(Sửa đăng ký)
        /// </summary>
        /// <returns></returns>
        public decimal SoCayLeDaDKSuaDangKy(List<MDangKyXe> input, string MaDH, string MaHH, string BienSoXe, Guid EdeliveryID)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal socayle = 0;
                var litst = db.DK_Edelivery.Where(t => t.MaHangHoa == MaHH && t.SONumber == MaDH && t.IsDelete == false && t.IDEdelivery != EdeliveryID).ToList();

                var listsosnh = new List<MDangKyXe>();


                foreach (var item in litst)
                {
                    MDangKyXe mDangKyXe = new MDangKyXe();

                    mDangKyXe.MaHangHoa = item.MaHangHoa;

                    mDangKyXe.SONumber = item.SONumber;

                    mDangKyXe.SOBC = item.SOBC;

                    mDangKyXe.BienSo_SoHieu = item.BienSo_SoHieu;

                    mDangKyXe.SOCAYLE = item.SOCAYLE;


                    listsosnh.Add(mDangKyXe);


                }


                listsosnh = listsosnh.Concat(input).ToList();


                foreach (var item in listsosnh)
                {

                    if (item.MaHangHoa == MaHH && item.SONumber == MaDH)
                    {
                        socayle += item.SOCAYLE.Value;
                    }
                }

                return socayle;
            }

        }

        /// <summary>
        /// Lưu thông tin đăng ký vào database
        /// </summary>
        /// <param name="edelivery">đối tượng đăng ký</param>
        /// <param name="lst"> danh sách các mặt hàng theo biển số</param>
        /// <returns>True => thành công | False => thất bại</returns>
        public string InsertDKEdelivery(DK_Edelivery edelivery, List<MDangKyXe> lst, ref List<string> ListSO, UserModel user)
        {
            string IDEdelivery = "";

            using (DbConnection db = new DbConnection())
            {
                BaremDAO baremDAO = new BaremDAO();

                try
                {
                    DK_Edelivery dK_Edelivery = new DK_Edelivery();

                    dK_Edelivery.IDEdelivery = Guid.NewGuid();

                    IDEdelivery = dK_Edelivery.IDEdelivery.ToString();

                    dK_Edelivery.MaKhachHang = edelivery.MaKhachHang;

                    dK_Edelivery.TenKhachHang = edelivery.TenKhachHang;

                    //truyền vào ID của CompanyCode trên Form => lấy CompanyCode 
                    dK_Edelivery.IDCompanyCode = int.Parse(edelivery.MaNoiGiaoNhan);

                    //Lấy CompanyCode
                    dK_Edelivery.MaNoiGiaoNhan = FindCompanyCode(int.Parse(edelivery.MaNoiGiaoNhan)).CompanyCode;

                    dK_Edelivery.TenNoiGiaoNhan = edelivery.TenNoiGiaoNhan;

                    dK_Edelivery.NgayGiaoNhan = edelivery.NgayGiaoNhan;

                    dK_Edelivery.MaDonViVanChuyen = edelivery.MaDonViVanChuyen;

                    dK_Edelivery.DonViVanChuyen = edelivery.DonViVanChuyen;

                    dK_Edelivery.NhaMaySanXuat = edelivery.NhaMaySanXuat;

                    dK_Edelivery.CreateUser = user.FullName;

                    dK_Edelivery.CreateUserId = user.UserId.ToString();

                    dK_Edelivery.NgayTao = DateTime.Now;

                    //ListSO gửi cho BPM
                    List<string> ListSOCALAPI = new List<string>();

                    foreach (var item in lst)
                    {
                        DK_Edelivery obj = dK_Edelivery;

                        obj.BienSo_SoHieu = item.BienSo_SoHieu;

                        obj.RoMooc = item.Romooc;

                        obj.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;

                        obj.CMND_CCCD = item.CMND_CCCD;

                        obj.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;

                        obj.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;

                        obj.SONumber = item.SONumber;

                        obj.SONumberBPM = item.SONumberPBM;

                        obj.SOItems = item.SOItems;

                        obj.MaHangHoa = item.MaHangHoa;

                        obj.TenHangHoa = item.TenHangHoa;

                        obj.SOBC = item.SOBC;

                        obj.SOCAY = item.SOCAY;

                        obj.SOCAYLE = item.SOCAYLE != null ? item.SOCAYLE.Value : 0;

                        var barem = baremDAO.FindBaremWithAPI(user.CompanyCode,obj.MaHangHoa);

                        if (barem == null)
                        {
                            obj.TRONGLUONG = item.TRONGLUONG;
                        }
                        else
                        {
                            obj.TRONGLUONG = (decimal)Math.Round((decimal)(obj.SOCAY + obj.SOCAYLE) * barem.BAREM1.Value / 1000, 3);

                        }

                        obj.SOLUONGBEBO = item.SOLUONGBEBO;

                        obj.VehicleKey = item.VehicelID;

                        obj.IsDelete = false;

                        obj.Status = Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua;

                        obj.BranchCode = dK_Edelivery.MaNoiGiaoNhan;

                        obj.Note = item.Note;

                        obj.rowguid = Guid.NewGuid();

                        db.DK_Edelivery.Add(obj);

                        ListSOCALAPI.Add(item.SONumber);

                        


                        //xóa UserLockId

                        var SOItems = db.Edelivery_GNH.Where(t => t.SONumber == item.SONumber && t.MAHANGHOA == item.MaHangHoa).FirstOrDefault();

                        SOItems.UserLockId = null;

                        db.SaveChanges();


                    }

                    Thread.Sleep(5000);

                    CallAPIBPM callAPIBPM = new CallAPIBPM();

                    ListSO = ListSOCALAPI.Distinct().ToList();

                    var listxe = lst.GroupBy(t => t.VehicelID);

                    string txt = "";

                    foreach (var item in listxe)
                    {
                        var lstSO = lst.Where(t=>t.VehicelID==item.Key).Select(t=>t.SONumber).ToList();

                        txt = callAPIBPM.CallComplateUsingParams("C", IDEdelivery, item.Key.ToString(), lstSO.Distinct().ToList());
                    }

                    var listInstance = db.DK_Edelivery.Where(t => t.IDEdelivery == Guid.Parse(IDEdelivery)).Select(t => t.Instance);

                    //while (listInstance!=null)
                    //{
                    //    listInstance = db.DK_Edelivery.Where(t => t.IDEdelivery == Guid.Parse(IDEdelivery)).Select(t => t.Instance);
                    //}

                    //string ListSOreturn = "";

                    //foreach (var item in listInstance)
                    //{
                    //    ListSOreturn += item + ", ";
                    //}

                    return txt;
                }
                catch (DbEntityValidationException e)
                {
                    //Console.WriteLine(e);

                    return string.Empty;
                }
            }


        }

        public CompanyModel FindCompanyCode(int id)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.CompanyModels.Find(id);
            }
        }

        /// <summary>
        /// Lấy danh sách khách hàng đã đăng ký
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DK_Edelivery> GetKhachHangDangKy()
        {
            using (DbConnection db = new DbConnection())
            {
                var lstkh = db.DK_Edelivery.Where(t => t.IsDelete == false).GroupBy(t => t.MaKhachHang).Select(t => t.FirstOrDefault()).ToList();

                return lstkh;
            }


        }

        /// <summary>
        /// Hủy Edelivery ==> Bỏ không sài
        /// </summary>
        /// <param name="IDEdelivery">Mã Edelivery</param>
        /// <param name="BienSoXe">Biển số xe</param>
        /// <param name="SONumber">Mã đơn hàng</param>
        /// <param name="MatHang">Mã mặt hàng</param>
        /// <param name="keyVehicel">Key định danh mặt hàng trong xe nào|vì xe có thể trùng</param>
        /// <returns></returns>
        public bool DeleteEdelivery(string IDEdelivery, string BienSoXe, string SONumber, string MatHang, string keyVehicel)
        {
            using (DbConnection db = new DbConnection())
            {
                Guid idEdelivery = Guid.Parse(IDEdelivery);

                Guid keyvehicle = Guid.Parse(keyVehicel);

                var obj = db.DK_Edelivery.Where(t => t.IDEdelivery == idEdelivery && t.BienSo_SoHieu == BienSoXe && t.SONumber == SONumber && t.MaHangHoa == MatHang && t.VehicleKey == keyvehicle).FirstOrDefault();

                //không tìm thấy
                if (obj == null)
                {
                    return false;
                }


                obj.IsDelete = true;

                db.SaveChanges();

                return true;
            }


        }

        /// <summary>
        /// Lấy danh sách giao nhận đã đăng ký kế hoạch tổng hợp
        /// </summary>
        /// <returns></returns>
        public IPagedList<ViewKeHoachGiaoNhan> GetAllKeHoachTongHop(SearchEdelivery search, int pageNumber = 1, int pageSize = 100)
        {
            using (DbConnection db = new DbConnection())
            {
                var lst = db.DK_Edelivery.Where(t => t.IsDelete == false).AsQueryable();

                List<MDSEdelivery> mDSEdeliveries = new List<MDSEdelivery>();

                if (!string.IsNullOrEmpty(search.BranchCode))
                {
                    lst = lst.Where(t => t.BranchCode == search.BranchCode);
                }
                if (!string.IsNullOrEmpty(search.MaKhachHang))
                {
                    lst = lst.Where(t => t.MaKhachHang == search.MaKhachHang);
                }
                if (!string.IsNullOrEmpty(search.SONumber))
                {
                    lst = lst.Where(t => t.SONumber == search.SONumber);
                }
                if (!string.IsNullOrEmpty(search.IDCompanyCode))
                {
                    int MaNoiGiaoNhan = int.Parse(search.IDCompanyCode);
                    lst = lst.Where(t => t.IDCompanyCode == MaNoiGiaoNhan);
                }
                if (search.NgayBatDau.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(search.NgayBatDau.Value, DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value) <= 0);
                }
                if (search.NgayKetThuc.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value, search.NgayKetThuc.Value) <= 0);
                }
                if (search.TrangThai.HasValue)
                {
                    if (search.TrangThai.Value != -1)
                    {
                        lst = lst.Where(t => t.Status == search.TrangThai.Value);
                    }
                }

                return ChangeListDBtoView(lst).ToList().OrderBy(t => t.IDEdelivery).ToPagedList(pageNumber, pageSize);
            }
        }

        public IEnumerable<MDSEdelivery> GETDSKHAIBAO()
        {
            using (DbConnection db = new DbConnection())
            {
                var lst = db.DK_Edelivery.Where(t => t.IsDelete == false).Select(t => t).GroupBy(t => t.IDEdelivery).Select(t => t.FirstOrDefault()).ToList();

                List<MDSEdelivery> mDSEdeliveries = new List<MDSEdelivery>();

                foreach (var item in lst)
                {
                    MDSEdelivery mDSEdelivery = new MDSEdelivery();
                    mDSEdelivery.IDEdelivery = item.IDEdelivery;
                    mDSEdelivery.MaKhachHang = item.MaKhachHang;
                    mDSEdelivery.TenKhachHang = item.TenKhachHang;
                    mDSEdelivery.NgayGiaoNhan = item.NgayGiaoNhan;
                    mDSEdelivery.DonViVanChuyen = item.DonViVanChuyen;
                    mDSEdelivery.TenNoiGiaoNhan = item.TenNoiGiaoNhan;
                    mDSEdelivery.Trongluongtong = TrongLuongTong(item.IDEdelivery);
                    mDSEdelivery.Status = GetStatusDisplay(item.Status.Value);
                    mDSEdelivery.MaNoiGiaoNhan = item.MaNoiGiaoNhan;
                    mDSEdelivery.MaDonViVanChuyen = item.MaDonViVanChuyen;
                    mDSEdelivery.IDCompanyCode = item.IDCompanyCode;
                    mDSEdeliveries.Add(mDSEdelivery);

                }
                return mDSEdeliveries;

            }
        }
        /// <summary>
        /// trả về danh sách khai báo
        /// </summary>
        /// <param name="search">tìm kiếm</param>
        /// <param name="UserId">user đăng nhập| user nào tạo thì chỉ thấy dk của user đó</param>
        /// <returns></returns>
        public IEnumerable<MDSEdelivery> GETDSKHAIBAO(SearchMDSEdelivery search, string UserId)
        {
            using (DbConnection db = new DbConnection())
            {

                var lst = db.DK_Edelivery.Where(t => t.IsDelete == false && t.CreateUserId == UserId).AsQueryable();


                List<MDSEdelivery> mDSEdeliveries = new List<MDSEdelivery>();

                if (!string.IsNullOrEmpty(search.BranchCode))
                {
                    lst = lst.Where(t => t.BranchCode == search.BranchCode);
                }
                if (!string.IsNullOrEmpty(search.MaKhachHang))
                {
                    lst = lst.Where(t => t.MaKhachHang == search.MaKhachHang);
                }
                if (!string.IsNullOrEmpty(search.MaNoiGiaoNhan))
                {
                    int MaNoiGiaoNhan = int.Parse(search.MaNoiGiaoNhan);
                    lst = lst.Where(t => t.IDCompanyCode == MaNoiGiaoNhan);
                }
                if (search.TuNgay.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(search.TuNgay.Value, DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value) <= 0);
                }
                if (search.DenNgay.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value, search.DenNgay.Value) <= 0);
                }
                if (search.LoaiVanChuyen.HasValue)
                {
                    if (search.LoaiVanChuyen.Value != 1)
                    {
                        //khách hàng
                        if (search.LoaiVanChuyen.Value == 2)
                        {
                            lst = lst.Where(t => t.MaKhachHang == t.MaDonViVanChuyen.Substring(1));
                        }
                        //Nội bộ
                        if (search.LoaiVanChuyen.Value == 3)
                        {
                            lst = lst.Where(t => t.MaKhachHang != t.MaDonViVanChuyen.Substring(1));
                        }
                    }


                }
                if (search.TrangThai.HasValue)
                {
                    if (search.TrangThai.Value != -1)
                    {
                        lst = lst.Where(t => t.Status == search.TrangThai.Value);
                    }
                }

                var DsDK = lst.Select(t => t).GroupBy(t => t.IDEdelivery).Select(t => t.FirstOrDefault()).OrderByDescending(t=>t.NgayTao).ToList();

                foreach (var item in DsDK)
                {
                    MDSEdelivery mDSEdelivery = new MDSEdelivery();
                    mDSEdelivery.IDEdelivery = item.IDEdelivery;
                    mDSEdelivery.MaKhachHang = item.MaKhachHang;
                    mDSEdelivery.TenKhachHang = item.TenKhachHang;
                    mDSEdelivery.NgayGiaoNhan = item.NgayGiaoNhan;
                    mDSEdelivery.DonViVanChuyen = item.DonViVanChuyen;
                    mDSEdelivery.TenNoiGiaoNhan = item.TenNoiGiaoNhan;
                    mDSEdelivery.Trongluongtong = TrongLuongTong(item.IDEdelivery);
                    mDSEdelivery.Status = GetStatusDisplay(item.Status.Value);
                    mDSEdelivery.MaNoiGiaoNhan = item.MaNoiGiaoNhan;
                    mDSEdelivery.MaDonViVanChuyen = item.MaDonViVanChuyen;
                    mDSEdelivery.IDCompanyCode = item.IDCompanyCode;
                    mDSEdelivery.NgayTao = item.NgayTao;
                    mDSEdeliveries.Add(mDSEdelivery);

                }
                return mDSEdeliveries;

            }
        }


        /// <summary>
        /// trả về danh sách khai báo
        /// </summary>
        /// <param name="search">tìm kiếm</param>
        /// <param name="UserId">user đăng nhập| user nào tạo thì chỉ thấy dk của user đó</param>
        /// <returns></returns>
        public IEnumerable<MDSEdelivery> GETDSKHAIBAOCHOKHO(SearchMDSEdelivery search, string NhaMaySanXuat)
        {
            using (DbConnection db = new DbConnection())
            {

                var lst = db.DK_Edelivery.Where(t => t.IsDelete == false).AsQueryable();

                List<MDSEdelivery> mDSEdeliveries = new List<MDSEdelivery>();

                if (!string.IsNullOrEmpty(search.MaKhachHang))
                {
                    lst = lst.Where(t => t.MaKhachHang == search.MaKhachHang);
                }
                if (search.TuNgay.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(search.TuNgay.Value, DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value) <= 0);
                }
                if (search.DenNgay.HasValue)
                {
                    lst = lst.Where(t => DateTime.Compare(DbFunctions.TruncateTime(t.NgayGiaoNhan.Value).Value, search.DenNgay.Value) <= 0);
                }
                if (search.LoaiVanChuyen.HasValue)
                {
                    if (search.LoaiVanChuyen.Value != 1)
                    {
                        //khách hàng
                        if (search.LoaiVanChuyen.Value == 2)
                        {
                            lst = lst.Where(t => t.MaKhachHang == t.MaDonViVanChuyen.Substring(1));
                        }
                        //Nội bộ
                        if (search.LoaiVanChuyen.Value == 3)
                        {
                            lst = lst.Where(t => t.MaKhachHang != t.MaDonViVanChuyen.Substring(1));
                        }
                    }


                }
                if (search.TrangThai.HasValue)
                {
                    if (search.TrangThai.Value != -1)
                    {
                        lst = lst.Where(t => t.Status == search.TrangThai.Value);
                    }
                }

                var DsDK = lst.Select(t => t).GroupBy(t => t.IDEdelivery).Select(t => t.FirstOrDefault()).OrderByDescending(t => t.NgayTao).ToList();

                foreach (var item in DsDK)
                {
                    MDSEdelivery mDSEdelivery = new MDSEdelivery();
                    mDSEdelivery.IDEdelivery = item.IDEdelivery;
                    mDSEdelivery.MaKhachHang = item.MaKhachHang;
                    mDSEdelivery.TenKhachHang = item.TenKhachHang;
                    mDSEdelivery.NgayGiaoNhan = item.NgayGiaoNhan;
                    mDSEdelivery.DonViVanChuyen = item.DonViVanChuyen;
                    mDSEdelivery.TenNoiGiaoNhan = item.TenNoiGiaoNhan;
                    mDSEdelivery.Trongluongtong = TrongLuongTong(item.IDEdelivery);
                    mDSEdelivery.Status = GetStatusDisplay(item.Status.Value);
                    mDSEdelivery.MaNoiGiaoNhan = item.MaNoiGiaoNhan;
                    mDSEdelivery.MaDonViVanChuyen = item.MaDonViVanChuyen;
                    mDSEdelivery.IDCompanyCode = item.IDCompanyCode;
                    mDSEdelivery.NgayTao = item.NgayTao;
                    mDSEdeliveries.Add(mDSEdelivery);

                }
                return mDSEdeliveries;

            }
        }

        public decimal? TrongLuongTong(Guid IDEdelivery)
        {
            using (DbConnection db = new DbConnection())
            {
                var tong = db.DK_Edelivery.Where(t => t.IDEdelivery == IDEdelivery && t.IsDelete == false).Sum(t=>t.TRONGLUONG);
                return db.DK_Edelivery.Where(t => t.IDEdelivery == IDEdelivery && t.IsDelete == false).Sum(t => t.TRONGLUONG);

            }
        }

        /// <summary>
        /// Đổi data ra view hiển thị
        /// </summary>
        /// <param name="dKEdeliveryDAO"></param>
        /// <returns></returns>
        public IEnumerable<ViewKeHoachGiaoNhan> ChangeListDBtoView(IEnumerable<DK_Edelivery> dKEdeliveryDAO)
        {
            var lstview = new List<ViewKeHoachGiaoNhan>();


            foreach (var item in dKEdeliveryDAO)
            {

                var t = new ViewKeHoachGiaoNhan();
                t.IDEdelivery = item.IDEdelivery;
                t.MaKhachHang = item.MaKhachHang;
                t.TenKhachHang = item.TenKhachHang;
                t.MaNoiGiaoNhan = item.MaNoiGiaoNhan;
                t.TenNoiGiaoNhan = item.TenNoiGiaoNhan;
                t.NgayGiaoNhan = item.NgayGiaoNhan;
                t.MaDonViVanChuyen = item.MaDonViVanChuyen;
                t.DonViVanChuyen = item.DonViVanChuyen;
                t.BienSo_SoHieu = item.BienSo_SoHieu;
                t.RoMooc = item.RoMooc;
                t.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
                t.CMND_CCCD = item.CMND_CCCD;
                t.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;
                t.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
                t.SONumber = item.SONumber;
                t.SOItems = item.SOItems;
                t.MaHangHoa = item.MaHangHoa;
                t.TenHangHoa = item.TenHangHoa;
                t.SOBC = item.SOBC;
                t.SOCAY = item.SOCAY;
                t.SOCAYLE = item.SOCAYLE;
                t.TRONGLUONG = (decimal)item.TRONGLUONG;
                t.SOLUONGBEBO = item.SOLUONGBEBO;
                t.VehicleKey = item.VehicleKey;
                t.IDCompanyCode = item.IDCompanyCode;
                t.Status = GetStatusDisplay(item.Status.Value);
                lstview.Add(t);
            }

            return lstview;
        }

        /// <summary>
        /// Hiển thị trạng thái
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string GetStatusDisplay(int Status)
        {
            switch (Status)
            {
                case Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua:
                    return "Tạo mới";
                case Constant.ConstantStatusDK_EDelivery.DangXacNhan:
                    return "Đang xử lý";
                case Constant.ConstantStatusDK_EDelivery.DaXacNhan:
                    return "Đã chuyển xử lý";
                case Constant.ConstantStatusDK_EDelivery.DangPheDuyet:
                    return "Đang phê duyệt";
                case Constant.ConstantStatusDK_EDelivery.PheDuyetThanhCong_HoanThanh:
                    return "Phê duyệt thành công - Hoàn thành";
            }
            return String.Empty;
        }

        public IEnumerable<MDangKyXe> GetDKEdelivery(string _EdeliveryID)
        {
            Guid EdeliveryID = Guid.Parse(_EdeliveryID);
            using (DbConnection db = new DbConnection())
            {
                var lstDk_edelivery = db.DK_Edelivery.Where(t => t.IsDelete == false && t.IDEdelivery == EdeliveryID).Select(t => t).ToList();

                List<MDangKyXe> lstrs = new List<MDangKyXe>();
                foreach (var item in lstDk_edelivery)
                {

                    MDangKyXe rs = new MDangKyXe();
                    rs.BienSo_SoHieu = item.BienSo_SoHieu;
                    rs.Romooc = item.RoMooc;
                    rs.CMND_CCCD = item.CMND_CCCD;
                    rs.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
                    rs.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
                    rs.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD.Value.Date;
                    rs.VehicelID = item.VehicleKey.Value;

                    lstrs.Add(rs);

                }

                return lstrs;
            }
        }


        public IEnumerable<MDangKyXe> GetDKEdeliverySua(string _EdeliveryID, string UserModel)
        {
            Guid EdeliveryID = Guid.Parse(_EdeliveryID);
            using (DbConnection db = new DbConnection())
            {
                var lstDk_edelivery = db.DK_Edelivery.Where(t => t.IsDelete == false && t.IDEdelivery == EdeliveryID).Select(t => t).ToList();

                List<MDangKyXe> lstrs = new List<MDangKyXe>();
                foreach (var item in lstDk_edelivery)
                {

                    MDangKyXe rs = new MDangKyXe();
                    rs.BienSo_SoHieu = item.BienSo_SoHieu;
                    rs.Romooc = item.RoMooc;
                    rs.CMND_CCCD = item.CMND_CCCD;
                    rs.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;
                    rs.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;
                    rs.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD.Value.Date;
                    rs.SONumber = item.SONumber;
                    rs.SONumberPBM = item.SONumberBPM;
                    rs.SOItems = item.SOItems;
                    rs.MaHangHoa = item.MaHangHoa;
                    rs.TenHangHoa = item.TenHangHoa;
                    rs.SOBC = item.SOBC;
                    rs.SOCAY = item.SOCAY;
                    rs.SOCAYLE = item.SOCAYLE;
                    rs.TRONGLUONG = (decimal)Math.Round((double)item.TRONGLUONG, 3);
                    rs.SOLUONGBEBO = item.SOLUONGBEBO;
                    rs.VehicelID = item.VehicleKey.Value;
                    rs.IsDelete = item.IsDelete;
                    rs.Status = item.Status;
                    rs.rowguid = item.rowguid;
                    lstrs.Add(rs);

                    var Edelivery_GNH = db.Edelivery_GNH.Where(t => t.SONumber == item.SONumber && t.SOItems == item.SOItems).FirstOrDefault();

                    Edelivery_GNH.UserLockId = UserModel;

                }
                db.SaveChanges();
                return lstrs;
            }
        }

        /// <summary>
        /// Cập nhật thông tin đăng ký vào database || delete hết cái cũ ròi thêm lại
        /// </summary>
        /// <param name="edelivery">đối tượng đăng ký</param>
        /// <param name="lst"> danh sách các mặt hàng theo biển số</param>
        /// <returns>True => thành công | False => thất bại</returns>
        public string UpdateDKEdelivery(DK_Edelivery edelivery, List<MDangKyXe> lst, Guid EdeliveryID, UserModel user)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    BaremDAO baremDAO = new BaremDAO();

                    var dkEdelivery = db.DK_Edelivery.Where(t => t.IDEdelivery == EdeliveryID && t.IsDelete == false).ToList();

                    //Danh sách đăng ký cũ
                    var lstDangKyCu = dkEdelivery;

                    var instance = dkEdelivery.FirstOrDefault().Instance;// sai do trường hợp nhiều SO

                    //ngày tạo đăng ký
                    var NgayTao = dkEdelivery.FirstOrDefault().NgayTao;

                    #region Bước 1 : Xóa hết danh sách cũ, set trạng thái IsDelete = true

                    foreach (var item in dkEdelivery)
                    {
                        item.IsDelete = true;
                    }

                    db.SaveChanges();

                    #endregion Bước 1 : Xóa hết danh sách cũ, set trạng thái IsDelete = true

                    #region Bước 2 : Tạo mới danh sách với IDEdelivery cũ - Tạo Header

                    DK_Edelivery dK_Edelivery = new DK_Edelivery();

                    dK_Edelivery.IDEdelivery = EdeliveryID;

                    dK_Edelivery.MaKhachHang = edelivery.MaKhachHang;

                    dK_Edelivery.TenKhachHang = edelivery.TenKhachHang;

                    //truyền vào ID của CompanyCode trên Form => lấy CompanyCode 
                    dK_Edelivery.IDCompanyCode = int.Parse(edelivery.MaNoiGiaoNhan);

                    //Lấy CompanyCode
                    dK_Edelivery.MaNoiGiaoNhan = FindCompanyCode(int.Parse(edelivery.MaNoiGiaoNhan)).CompanyCode;

                    dK_Edelivery.TenNoiGiaoNhan = edelivery.TenNoiGiaoNhan;

                    dK_Edelivery.NgayGiaoNhan = edelivery.NgayGiaoNhan;

                    dK_Edelivery.MaDonViVanChuyen = edelivery.MaDonViVanChuyen;

                    dK_Edelivery.DonViVanChuyen = edelivery.DonViVanChuyen;

                    dK_Edelivery.NhaMaySanXuat = edelivery.NhaMaySanXuat;

                    dK_Edelivery.CreateUser = user.FullName;

                    dK_Edelivery.CreateUserId = user.UserId.ToString();

                    dK_Edelivery.NgayTao = NgayTao;

                    #region Bước 2.1 : Tạo mới danh sách với IDEdelivery cũ - Tạo Nội dung đăng ký

                    foreach (var item in lst)
                    {
                        DK_Edelivery obj = dK_Edelivery; 

                        obj.BienSo_SoHieu = item.BienSo_SoHieu;

                        obj.RoMooc = item.Romooc;

                        obj.TaiXe_ThuyenTruong = item.TaiXe_ThuyenTruong;

                        obj.CMND_CCCD = item.CMND_CCCD;

                        obj.NgayCapCMND_CCCD = item.NgayCapCMND_CCCD;

                        obj.NoiCapCMND_CCCD = item.NoiCapCMND_CCCD;

                        obj.SONumber = item.SONumber;

                        obj.SONumberBPM = item.SONumberPBM;

                        obj.SOItems = item.SOItems;

                        obj.MaHangHoa = item.MaHangHoa;

                        obj.TenHangHoa = item.TenHangHoa;

                        obj.SOBC = item.SOBC;

                        obj.SOCAY = item.SOCAY;

                        obj.SOCAYLE = item.SOCAYLE != null ? item.SOCAYLE.Value : 0;

                        var barem = baremDAO.FindBaremWithAPI(user.CompanyCode,obj.MaHangHoa);

                        if (barem == null)
                        {
                            obj.TRONGLUONG = item.TRONGLUONG;
                        }
                        else
                        {
                            obj.TRONGLUONG = (decimal)Math.Round((double)((obj.SOCAY + obj.SOCAYLE) * (barem.BAREM1 != null ? (decimal)(Math.Round((barem.BAREM1.Value / 1000), 4)) : 0)), 3);

                        }


                        //obj.TRONGLUONG = (decimal)Math.Round((double)((obj.SOCAY + obj.SOCAYLE) * (baremDAO.FindBarem(obj.MaHangHoa).BAREM1 != null ? (decimal)(Math.Round((baremDAO.FindBarem(obj.MaHangHoa).BAREM1.Value / 1000), 4)) : 0)), 3);

                        obj.SOLUONGBEBO = item.SOLUONGBEBO;

                        obj.VehicleKey = item.VehicelID;

                        obj.IsDelete = false;

                        obj.Status = Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua;

                        var instancenew = lstDangKyCu.Where(t => t.SONumber == item.SONumber && t.VehicleKey== item.VehicelID);

                        if (instancenew.Count() != 0)
                        {
                            obj.Instance = instancenew.FirstOrDefault().Instance;
                        }
                        else
                        {
                            obj.Instance = null;
                        }

                        obj.BranchCode = dK_Edelivery.MaNoiGiaoNhan;

                        //obj.NhaMaySanXuat = item;

                        obj.rowguid = Guid.NewGuid();

                        db.DK_Edelivery.Add(obj);

                        db.SaveChanges();
                    }

                    #endregion Bước 2.1 : Tạo mới danh sách với IDEdelivery cũ - Tạo Nội dung đăng ký

                    #endregion Bước 2 : Tạo mới danh sách với IDEdelivery cũ - Tạo Header

                    #region Bước 3 : Xác định Nội dung thay đổi

                    var listSOdangkycu = lstDangKyCu.GroupBy(t => new {t.SONumber, t.VehicleKey}).ToList();

                    var lstSOdangkymoi = db.DK_Edelivery.Where(t => t.IDEdelivery == EdeliveryID && t.IsDelete == false).GroupBy(t => new { t.SONumber, t.VehicleKey }).ToList();

                    #region Bước 3.1 : Tạo mới SO - (SO mới không có trong List đăng ký cũ):

                    var listSONew = lstSOdangkymoi.Where(t => listSOdangkycu.All(t2 => t2.Key.SONumber != t.Key.SONumber || t2.Key.VehicleKey != t.Key.VehicleKey));

                    #endregion Bước 3.1 : Tạo mới SO - (SO mới không có trong List đăng ký cũ):

                    #region Bước 3.2 : Cập nhật SO - (SO mới có trong List đăng ký cũ):

                    //Chỉ cần gửi IDedelivery

                    #endregion Bước 3.2 : Cập nhật SO - (SO mới có trong List đăng ký cũ):

                    #region Bước 3.3 : Hủy SO - (Không có SO cũ trong cập nhật mới):
                    
                    var listSOld = listSOdangkycu.Where(t => lstSOdangkymoi.All(t2 => t2.Key.SONumber != t.Key.SONumber || t2.Key.VehicleKey != t.Key.VehicleKey));

                    #endregion Bước 3.3 : Hủy SO - (Không có SO cũ trong cập nhật mới):

                    #endregion Bước 3 : Xác định Nội dung thay đổi
                    
                    Thread.Sleep(5000);
                   
                    #region Bước 4 : Call API các trạng thái

                    CallAPIBPM callAPIBPM = new CallAPIBPM();

                    #region Bước 4.1 : Tạo mới
                   

                    if (listSONew.Count() != 0)
                    {
                       // var ListSO = callAPIBPM.ListSOTOParams(listSONew.Select(t => t.Key.SONumber).ToList());

                       // callAPIBPM.CallComplate("C", EdeliveryID.ToString(), ListSO);


                        var listxeSO = listSONew.GroupBy(t => new { t.Key.SONumber, t.Key.VehicleKey });

                        var listXe = listxeSO.GroupBy(t => t.Key.VehicleKey);

                        foreach (var item in listXe)
                        {
                            var lstSO = listxeSO.Where(t => t.Key.VehicleKey == item.Key).Select(t => t.Key.SONumber).ToList();

                            callAPIBPM.CallComplateUsingParams("C", EdeliveryID.ToString(), item.Key.ToString(), lstSO.Distinct().ToList());
                        }
                    }

                    #endregion Bước 4.1 : Tạo mới

                    #region Bước 4.2 : Cập nhật

                    var ldkc = listSOdangkycu.Select(t=>t.Key).ToList();

                    var ldkm = lstSOdangkymoi.Select(t => t.Key).ToList();

                    var ListSOnEWbANGvOIsoOLD = ldkc.Where(p => ldkm.Exists(p2 => p2.VehicleKey == p.VehicleKey && p2.SONumber == p.SONumber)).GroupBy(t => new { t.SONumber, t.VehicleKey }).ToList();

                    var listXeCapnhat = ListSOnEWbANGvOIsoOLD.GroupBy(t => t.Key.VehicleKey);
                    //string capnhat = callAPIBPM.ListSOTOParams(ListSOnEWbANGvOIsoOLD.Select(t => t).ToList());

                    foreach (var item in listXeCapnhat)
                    {

                        var lstSO = ListSOnEWbANGvOIsoOLD.Where(t => t.Key.VehicleKey == item.Key).ToList();
                        callAPIBPM.CallComplateUsingParams("U", EdeliveryID.ToString(),item.Key.ToString(), lstSO.Select(t=>t.Key.SONumber).Distinct().ToList());
                    }

                    

                    #endregion Bước 4.2 : Cập nhật

                    #region Bước 4.3 : Hủy

                    foreach (var item in listSOld)
                    {
                        var instanceold = lstDangKyCu.Where(p => p.SONumber == item.Key.SONumber).FirstOrDefault().Instance;

                        //callAPIBPM.CallComplate("D", instanceold, capnhat);

                        callAPIBPM.CallComplateUsingParams("D", instanceold, item.Key.VehicleKey.ToString(),new List<string>());
                    }

                    #endregion Bước 4.3 : Hủy

                    #endregion Bước 4 : Call API các trạng thái

                    return EdeliveryID.ToString();
                    
                }
               
            }
            catch (Exception)
            {
                return string.Empty;
            }

            //vasgroup/longlh
        }



        /// <summary>
        /// Huy đăng ký EDelivery || chỉ cập nhật trạng thái IsDelete = true
        /// </summary>
        /// <param name="edelivery">đối tượng đăng ký</param>
        /// <returns>Instance => thành công | String.emty => thất bại</returns>
        public string HuyDKEdelivery(Guid EdeliveryID)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {

                    var dkEdelivery = db.DK_Edelivery.Where(t => t.IDEdelivery == EdeliveryID && t.IsDelete == false).ToList();

                    var sohuy = dkEdelivery.GroupBy(t => t.SONumber);

                    if (dkEdelivery.FirstOrDefault().Instance == null || dkEdelivery.FirstOrDefault().Instance == "")
                    {
                        return string.Empty;
                    }
                    else
                    {
                        foreach (var item in dkEdelivery)
                        {
                            item.IsDelete = true;
                          
                        }

                        var listInstanceSO = dkEdelivery.GroupBy(t=>new { t.Instance, t.VehicleKey }).ToList();

                        foreach (var item in listInstanceSO)
                        {
                            var t = item.Key;

                            CallAPIBPM callAPIBPM = new CallAPIBPM();

                            string listSO = callAPIBPM.ListSOTOParams(sohuy.Select(p=>p.Key).ToList());

                            callAPIBPM.CallComplateUsingParams("D", item.Key.Instance,item.Key.VehicleKey.ToString(), new List<string>());
                        }


                        db.SaveChanges();

                     

                        return dkEdelivery.FirstOrDefault().Instance;
                    }


                }

            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        #region Trả về trạng thái đăng ký
        public int TrangThaiDkEdelivery(Guid IDEdelivery)
        {
            using (DbConnection db = new DbConnection())
            {
                var EDelivery = db.DK_Edelivery.Where(t => t.IDEdelivery == IDEdelivery && t.IsDelete == false).FirstOrDefault();

                return EDelivery.Status.Value;
            }
        }
        #endregion Trả về trạng thái đăng ký

        //Kiểm tra loại đơn vị vận chuyển
        public int LoaiVanChuyen(Guid IDEdelivery)
        {
            using (DbConnection db = new DbConnection())
            {
                var EDelivery = db.DK_Edelivery.Where(t => t.IDEdelivery == IDEdelivery && t.IsDelete == false).FirstOrDefault();

                if (EDelivery.MaKhachHang == EDelivery.MaDonViVanChuyen.Substring(1))
                {
                    return Constant.LoaiVanChuyen.KhachHang;
                }
                else
                {
                    return Constant.LoaiVanChuyen.NoiBo;
                }

            }
        }

        #region Trả về user đang thao tác trên danh sách SO + Item
        public List<UserModel> UserDangThaoTac(Guid IDEdelivery)
        {
            List<UserModel> EDeliverys = new List<UserModel>();
            using (DbConnection db = new DbConnection())
            {
                //danh sách EDelivery Đăng ký đang chọn
                var EDelivery = db.DK_Edelivery.Where(t => t.IDEdelivery == IDEdelivery && t.IsDelete == false);

                foreach (var item in EDelivery)
                {
                    //Danh sách SOline với SONumber + ItEM = ĐĂNG KÝ
                    var EDelivery_GNH = db.Edelivery_GNH.Where(t => t.SONumber == item.SONumber && t.MAHANGHOA == item.MaHangHoa).FirstOrDefault();

                    if (EDelivery_GNH != null)
                    {
                        if (!string.IsNullOrEmpty(EDelivery_GNH.UserLockId))
                        {
                            EDeliverys.Add(db.UserModels.Find(Guid.Parse(EDelivery_GNH.UserLockId)));
                        }
                    }
                }
            }
            return EDeliverys;
        }

        #endregion

        public List<MInstance> ShowInstance(Guid EDeliveryId)
        {
            using (DbConnection db = new DbConnection())
            {
                var lstrs = new List<MInstance>();

                var lst = db.DK_Edelivery.Where(t=>t.IDEdelivery == EDeliveryId && t.IsDelete == false).GroupBy(t => t.Instance).Select(t => t.FirstOrDefault()).ToList();

                // xe nào// so / instance


                foreach (var item in lst)
                {
                    var instance = new MInstance();
                    instance.BienSo = item.BienSo_SoHieu;
                    instance.SOSAP = item.SONumber;
                    instance.SOBPM = item.SONumberBPM;
                    instance.KhachHang = item.TenKhachHang;
                    instance.Instance = item.Instance;
                    lstrs.Add(instance);
                    
                }
                return lstrs;
            }
        }
    }
}

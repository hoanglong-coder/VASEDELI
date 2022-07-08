using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntitiesFramwork;
using DAL.Model;
using Dapper;
using PagedList;
namespace BLL.DAO
{
    public class VehicleModelDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        /// <summary>
        /// Get list data vehicle
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VehicleModel> GetALL(string MaDVVC)
        {
            using (DbConnection db = new DbConnection())
            {
                if (MaDVVC == string.Empty)
                {

                    List<VehicleModel> list = new List<VehicleModel>();

                    return list;
                }

                // Return list data vehicle of unit 
                var query = db.VehicleModels.Select(t => t).Where(t => t.VehicleOwner.Contains(MaDVVC) && t.isRoMooc == 0).ToList();

                return query;
            }
        }

        public string[] GetALLUsingDapper(object MaDVVC , object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("vehicleOwner", (string)MaDVVC);
                p.Add("text", (string)obj);

                string[] arr = connection.Query<string>(@"select top 10 @text+ ' | ' + VehicleNumber from  VehicleModel with(nolock)
                where VehicleOwner = @vehicleOwner
                AND (isRoMooc is null or isRoMooc = 0)
                AND (VehicleNumber like '%' + @text + '%' OR
                REPLACE(REPLACE( VehicleNumber,'-',''),'.','') like '%' + @text + '%')", p).ToArray();
                return arr;
            }
        }

        public bool IsDauKeo(object MaDVVC,object BienSoXe)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("vehicleOwner", (string)MaDVVC);
                p.Add("text", (string)BienSoXe);

                var arr = connection.Query<VehicleModel>(@"select * from  VehicleModel with(nolock)
                where VehicleOwner = @vehicleOwner and VehicleNumber = @text", p).FirstOrDefault();

                if (arr != null)
                {
                    if (arr.isDauKeo.HasValue)
                    {
                        if (arr.isDauKeo.Value)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }else
                    {
                        return false;
                    }
                }else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Return Vehicle number Ro mooc 
        /// </summary>
        /// <param name="biensoxe"></param>
        /// <returns></returns>
        public string ListVehicleNumberRomooc(string biensoxe)
        {
            //using (DbConnection db = new DbConnection())
            //{
            //    var idbiensoxe = db.VehicleModels.Where(t => t.VehicleNumber == biensoxe).FirstOrDefault();

            //    var kiemtracoromoc = db.VehicleInfoMappings.Where(t => t.VehicleId == idbiensoxe.VehicleId).FirstOrDefault();

            //    if (kiemtracoromoc != null)
            //    {
            //        if (kiemtracoromoc.RomoocId != null)
            //        {

            //            return db.VehicleModels.Where(t => t.VehicleId == kiemtracoromoc.RomoocId.Value).FirstOrDefault().VehicleNumber;


            //        }
            //    }

            //    return String.Empty;
            //}


            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("VehicleNumber", (string)biensoxe);

                var idbiensoxe = connection.Query<VehicleModel>(@"select * from  VehicleModel with(nolock)
                where VehicleNumber = @VehicleNumber ", p).FirstOrDefault();

                var p2 = new DynamicParameters();
                p2.Add("VehicleId", idbiensoxe.VehicleId.ToString());

                var kiemtracoromoc = connection.Query<VehicleInfoMapping>(@"select * from  VehicleInfoMapping with(nolock)
                where VehicleId = @VehicleId ", p2).FirstOrDefault();

                
                if (kiemtracoromoc != null)
                {
                    if(kiemtracoromoc.RomoocId != null)
                    {
                        p2.Add("VehicleId", kiemtracoromoc.RomoocId.Value.ToString());
                        return connection.Query<VehicleModel>(@"select * from  VehicleModel with(nolock)
                        where VehicleId = @VehicleId ", p2).FirstOrDefault().VehicleNumber;
                    }
                }

                return String.Empty;

            }


        }

        /// <summary>
        /// Lấy danh sách ro móc
        /// </summary>
        /// <param name="MaDVVC"> mã đơn vị vẩn chuyển</param>
        /// <returns></returns>
        public IEnumerable<VehicleModel> GetALLRomoc(string MaDVVC)
        {
            using (DbConnection db = new DbConnection())
            {
                if (MaDVVC == string.Empty)
                {

                    List<VehicleModel> list = new List<VehicleModel>();

                    return list;
                }

                // Câu truy vấn trả về danh sách biển số xe theo đơn vị vẩn chuyển
                var query = db.VehicleModels.Select(t => t).Where(t => t.VehicleOwner.Contains(MaDVVC) && t.isRoMooc == 1).ToList();

                return query;
            }
            

        }

        public string[] GetALLRomocUsingDapper(object MaDVVC, object obj)
        {

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("vehicleOwner", (string)MaDVVC);
                p.Add("text", (string)obj);

                string[] arr = connection.Query<string>(@"select top 10 @text+ ' | ' + VehicleNumber from  VehicleModel with(nolock)
                where VehicleOwner = @vehicleOwner
                AND (isRoMooc is null or isRoMooc = 1)
                AND (VehicleNumber like '%' + @text + '%' OR
                REPLACE(REPLACE( VehicleNumber,'-',''),'.','') like '%' + @text + '%')", p).ToArray();
                return arr;
            }
        }

        /// <summary>
        /// Kiểm tra trọng lượng đơn hàng có lớn hơn trộng lượng đăng kiểm hay không
        /// </summary>
        /// <param name="BienSoXe">Biển số xe kiểm tra</param>
        /// <param name="trongluong">Trộng lượng kiểm tra</param>
        /// <returns>True => cho phép | False => không cho phép</returns>
        public bool KiemTraTrongLuongDangKiem(string BienSoXe, float trongluong)
        {
            using (DbConnection db = new DbConnection())
            {
                decimal TrongLuongdangkiem = (db.VehicleModels.Where(t => t.VehicleNumber == BienSoXe).FirstOrDefault().TrongLuongDangKiem.Value);

                double trongluongvuot = Math.Round((double)TrongLuongdangkiem * 0.1, 0);

                decimal TrongluongTong = TrongLuongdangkiem + (decimal)trongluongvuot;


                if ((trongluong * 1000) <= (float)TrongluongTong)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            

        }
        //SỬ DỤNG DAPPER
        public bool KiemTraXeCoTrongData(string BienSoXe, string _MaDVVC)
        {
            //using (DbConnection db = new DbConnection())
            //{
            //    var vehicle = db.VehicleModels.Where(t => t.VehicleNumber == BienSoXe).FirstOrDefault();

            //    if (vehicle != null)
            //    {
            //        return true;
            //    }
            //    return false;
            //}

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var p2 = new DynamicParameters();
                p2.Add("vehicleOwner", _MaDVVC);
                p2.Add("text", BienSoXe);

                string[] arr = connection.Query<string>(@"select top 10 @text+ ' | ' + VehicleNumber from  VehicleModel with(nolock)
                where VehicleOwner = @vehicleOwner
                AND (isRoMooc is null or isRoMooc = 0)
                AND (VehicleNumber like '%' + @text + '%' OR
                REPLACE(REPLACE( VehicleNumber,'-',''),'.','') like '%' + @text + '%')", p2).ToArray();


                if (arr.Count() != 0)
                {
                    return true;
                }
                return false;
            }

        }

        /// <summary>
        /// Lấy danh sách tất cả xe
        /// </summary>
        /// <returns></returns>
        public IPagedList<MVehicleModel> GetAllDsXe(int pageNumber =1, int pageSize = 27)
        {
            var lstReturn = new List<MVehicleModel>();

            VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();

            using (DbConnection db = new DbConnection())
            {
                var lstxe = db.VehicleModels.Select(t => t).Where(p=>p.VehicleOwner.StartsWith("C")||p.VehicleOwner.StartsWith("S"));

                var lstdvvc = db.VehicleOwnerModels.Select(t => t);


                var result = from dataxe in lstxe
                             join datadvvc in lstdvvc on dataxe.VehicleOwner equals datadvvc.VehicleOwner
                             
                             select new 
                             {
                                 VehicleNumber = dataxe.VehicleNumber,

                                 Type = dataxe.Type,

                                 VehicleWeight = dataxe.VehicleWeight,

                                 TrongLuongDangKiem = dataxe.TrongLuongDangKiem,

                                 DonViVanChuyen = datadvvc.VehicleOwnerName,

                                 TyLeVuot = dataxe.TyLeVuot,

                                 Khoa = dataxe.isLock.Value == true ? "Khóa" : "Không khóa"
                             };


                foreach (var item in result)
                {
                    MVehicleModel mVehicleModel = new MVehicleModel();

                    mVehicleModel.VehicleNumber = item.VehicleNumber;
                    
                    mVehicleModel.Type = GetLoaiXe(item.Type.Value);

                    mVehicleModel.VehicleWeight = item.VehicleWeight.Value.ToString("N0");

                    mVehicleModel.TrongLuongDangKiem = item.TrongLuongDangKiem.Value.ToString("N0");

                    mVehicleModel.DonViVanChuyen = item.DonViVanChuyen;

                    mVehicleModel.TyLeVuot = item.TyLeVuot;

                    mVehicleModel.Khoa = item.Khoa;

                    lstReturn.Add(mVehicleModel);

                }
            }

            return lstReturn.OrderBy(t=>t.VehicleWeight).ToPagedList(pageNumber,pageSize);
        }


        /// <summary>
        /// Search Danh sách xe => sử dụng dapper
        /// </summary>
        /// <returns></returns>
        public IPagedList<MVehicleModel> GetAllDsXe(MVehicleModelSearch mVehicleModelSearch,int pageNumber = 1, int pageSize = 27)
        {
            var lstReturn = new List<MVehicleModel>();

            VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();

            using (DbConnection db = new DbConnection())
            {

                var lstxe = db.VehicleModels.Select(t => t).Where(p => p.VehicleOwner.StartsWith("C") || p.VehicleOwner.StartsWith("S")).AsQueryable();

                var lstdvvc = db.VehicleOwnerModels.Select(t => t);

                if (!string.IsNullOrEmpty(mVehicleModelSearch.VehicleNumber))
                {
                    lstxe = lstxe.Where(t => t.VehicleNumber.Contains(mVehicleModelSearch.VehicleNumber));
                }

                if (!string.IsNullOrEmpty(mVehicleModelSearch.DonViVanChuyen))
                {
                    lstxe = lstxe.Where(t => t.VehicleOwner == mVehicleModelSearch.DonViVanChuyen);
                }

                if (mVehicleModelSearch.Type != null)
                {
                    if(mVehicleModelSearch.Type != -1)
                    {
                        lstxe = lstxe.Where(t => t.Type == mVehicleModelSearch.Type);
                    }
                   
                }

                if (mVehicleModelSearch.KieuXe != null)
                {
                    if (mVehicleModelSearch.KieuXe.Value == Constant.ConstantVehicle.XeThuong)
                    {
                        lstxe = lstxe.Where(t => t.isDauKeo == false && t.isRoMooc==0);
                    }else if (mVehicleModelSearch.KieuXe.Value == Constant.ConstantVehicle.DauKeo)
                    {
                        lstxe = lstxe.Where(t => t.isDauKeo==true);
                    }else if (mVehicleModelSearch.KieuXe.Value == Constant.ConstantVehicle.Romoc)
                    {
                        lstxe = lstxe.Where(t => t.isRoMooc ==1);
                    }
                }

                var result = from dataxe in lstxe.ToList()
                             join datadvvc in lstdvvc on dataxe.VehicleOwner equals datadvvc.VehicleOwner into ps
                             from dataxeleft in ps.DefaultIfEmpty()
                             select new
                             {
                                 VehicleId = dataxe.VehicleId,

                                 VehicleNumber = dataxe.VehicleNumber,                           

                                 Type = dataxe.Type,

                                 VehicleWeight = dataxe.VehicleWeight,

                                 TrongLuongDangKiem = dataxe.TrongLuongDangKiem,

                                 DonViVanChuyen = dataxeleft!=null? dataxeleft .VehicleOwnerName:"",

                                 TyLeVuot = dataxe.TyLeVuot,

                                 Khoa = dataxe.isLock.Value == true ? "Khóa" : "Không khóa",

                                 KieuXe = dataxe.isDauKeo == true ? "Đầu kéo": dataxe.isRoMooc == 1?"Rơ móc":"Xe thường"

                             };


                foreach (var item in result)
                {
                    MVehicleModel mVehicleModel = new MVehicleModel();

                    mVehicleModel.VehicleId = item.VehicleId.ToString();

                    mVehicleModel.VehicleNumber = item.VehicleNumber;

                    mVehicleModel.Type = GetLoaiXe(item.Type.Value);

                    mVehicleModel.VehicleWeight = item.VehicleWeight.Value.ToString("N0");

                    mVehicleModel.TrongLuongDangKiem = item.TrongLuongDangKiem.Value.ToString("N0");

                    mVehicleModel.DonViVanChuyen = item.DonViVanChuyen;

                    mVehicleModel.TyLeVuot = item.TyLeVuot;

                    mVehicleModel.Khoa = item.Khoa;

                    mVehicleModel.KieuXe = item.KieuXe;

                    lstReturn.Add(mVehicleModel);

                }
            }



            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var lstxe = connection.Query<VehicleModel>(@"select * from VehicleModel where VehicleOwner like 'C%' or VehicleOwner like 'S%'").AsQueryable();

                
            }

            return lstReturn.OrderBy(t => t.VehicleWeight).ToPagedList(pageNumber, pageSize); ;
        }

        public string GetLoaiXe(int type)
        {
            if (type == Constant.ConstantVehicle.NoiBo)
            {
                return "Xe nội bộ";
            }
            else if (type == Constant.ConstantVehicle.KhachHang)
            {
                return "Xe khách hàng";
            }
            else
            {
                return "Xe đơn vị vận chuyển";
            }
        }

        public VehicleModel FindXe(Guid VehicleId)
        {
            using (DbConnection db = new DbConnection())
            {
                return db.VehicleModels.Find(VehicleId);
            } 
        }

        public bool AddXe(VehicleModel vehicleModel, string Romoc)
        {
            using (DbConnection db = new DbConnection())
            {
                try
                {
                    db.VehicleModels.Add(vehicleModel);

                    db.SaveChanges();

                    if(vehicleModel.isDauKeo.Value == true)
                    {
                        var idromoc = db.VehicleModels.Where(t => t.VehicleNumber == Romoc.Trim()).FirstOrDefault();

                        VehicleInfoMapping vehicle = new VehicleInfoMapping();

                        vehicle.VehicleId = vehicleModel.VehicleId;
                        vehicle.VehicleOwner = vehicleModel.VehicleOwner;
                        vehicle.RomoocId = idromoc.VehicleId;

                        db.VehicleInfoMappings.Add(vehicle);

                        db.SaveChanges();

                    }

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            
        }
        
        /// <summary>
        /// Kiểm tra trùng biển số xe
        /// </summary>
        /// <param name="BienSoXe">Biển số xe</param>
        /// <returns>false là đã có biển số không thêm được | ngược lại</returns>
        public bool KiemTraTrungBienSoXwe(string BienSoXe, string VehiclaOwner)
        {
            using (DbConnection db = new DbConnection())
            {

                var count = db.VehicleModels.Where(t => t.VehicleNumber == BienSoXe && t.VehicleOwner== VehiclaOwner).Count();


                if (count > 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public bool RemoveXe(string vehicleId)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    var vehicle = db.VehicleModels.Find(Guid.Parse(vehicleId));

                    db.VehicleModels.Remove(vehicle);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool UpdateXe(VehicleModel vehicleModel)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    var vehicleModelEdit = db.VehicleModels.Where(t => t.VehicleNumber == vehicleModel.VehicleNumber).FirstOrDefault();

                    vehicleModelEdit.VehicleOwner = vehicleModel.VehicleOwner;

                    vehicleModelEdit.VehicleWeight = vehicleModel.VehicleWeight;

                    //vehicleModelEdit.isRoMooc = vehicleModel.isRoMooc;

                    vehicleModelEdit.TrongLuongDangKiem = vehicleModel.TrongLuongDangKiem;

                    vehicleModelEdit.TyLeVuot = vehicleModel.TyLeVuot;

                    vehicleModelEdit.isLock = vehicleModel.isLock;

                    vehicleModelEdit.isLockEdit = vehicleModel.isLockEdit;

                    vehicleModelEdit.LastEditTime = vehicleModel.LastEditTime;

                    //vehicleModelEdit.isDauKeo = vehicleModel.isDauKeo;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}

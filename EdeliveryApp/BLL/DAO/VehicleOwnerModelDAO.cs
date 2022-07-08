using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.EntitiesFramwork;
using System.Data.Entity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace BLL.DAO
{
    public class VehicleOwnerModelDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        /// <summary>
        /// Lấy thông tin đơn vị vẩn chuyển
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VehicleOwnerModel> GetALL()
        {

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var arr = connection.Query<VehicleOwnerModel>(@"select * from VehicleOwnerModel
                where Actived = 1").ToList();

                return arr;
            }
        }
        public IEnumerable<VehicleOwnerModel> GetALL(string MaDonViVanChuyen, string TenDonViVanChuyen)
        {

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var p = new DynamicParameters();
                p.Add("VehicleOwner", MaDonViVanChuyen);
                p.Add("VehicleOwnerName", TenDonViVanChuyen);

                var arr = connection.Query<VehicleOwnerModel>(@"select * from VehicleOwnerModel
                where Actived = 1 and VehicleOwner like '%' + @VehicleOwner + '%' and VehicleOwnerName like N'%' + @VehicleOwnerName + '%'", p).ToList();

                return arr;
            }

        }
        public string[] GetALLUsingDapper(object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);

                string[] arr = connection.Query<string>(@"select top 10 @text + ' | ' + VehicleOwnerName+ ' | ' + VehicleOwner  from VehicleOwnerModel
                where Actived = 1 and VehicleOwner like 'S%'  and (VehicleOwnerName + ' | ' + VehicleOwner) like '%' +  @text + '%'", p).ToArray();

                return arr;
            }
        }

        /// <summary>
        /// AutoComplete Đơn vị vận chuyển khách hàng
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string[] GetALLUsingDapperKhachHang(object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);

                string[] arr = connection.Query<string>(@"select top 10 @text + ' | ' + VehicleOwnerName+ ' | ' + VehicleOwner  from VehicleOwnerModel
                where Actived = 1 and VehicleOwner like 'C%'  and (VehicleOwnerName + ' | ' + VehicleOwner) like '%' +  @text + '%'", p).ToArray();

                return arr;
            }
        }


        /// <summary>
        /// Tìm mã đơn vị vẩn chuyển từ mã khách hàng
        /// </summary>
        /// <param name="MaKH">Mã khách hàng giao nhận </param>
        /// <returns>Mã đơn vị vận chuyển</returns>
        public string GetMaDVVC(string MaKH)
        {
            using (DbConnection db = new DbConnection())
            {
                var donvivanchuyen = db.VehicleOwnerModels.Select(t => t.VehicleOwner).ToList();

                foreach (var item in donvivanchuyen)
                {
                    if (item.Substring(1) == MaKH)
                    {
                        return item;
                    }


                }
                return String.Empty;
            }
            
        }
        public string GetNameDVVC(string MaDVVC)
        {
            using (DbConnection db = new DbConnection())
            {

                var obj = db.VehicleOwnerModels.Where(t=>t.VehicleOwner==MaDVVC).FirstOrDefault();


                if (obj != null)
                {
                    return obj.VehicleOwnerName;
                }else
                {
                    return string.Empty;
                }    



            }
        }
   
        /// <summary>
        /// Thêm đơn vị vận chuyển
        /// </summary>
        /// <param name="vehicleOwnerModel">Model đơn vị vận chuyển</param>
        /// <returns>true => thành công | false => thất bại</returns>
        public string Insert(VehicleOwnerModel vehicleOwnerModel)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    //Kiểm tra trùng mã đơn vị vận chuyển

                    var vehicleOwnerModelCheck = db.VehicleOwnerModels.Where(t => t.VehicleOwner == vehicleOwnerModel.VehicleOwner && t.Actived ==true);

                    if (vehicleOwnerModelCheck.Count() != 0)
                    {
                        return "Lưu thất bại. Do mã vận chuyển này đã có";
                    }

                    db.VehicleOwnerModels.Add(vehicleOwnerModel);

                    db.SaveChanges();

                    return "Lưu thành công";
                }


            }
            catch (Exception)
            {
                return "Lưu thất bại";
            }
        }
        /// <summary>
        /// Cập nhật đơn vị vận chuyển
        /// </summary>
        /// <param name="vehicleOwnerModel">Model đơn vị vận chuyển</param>
        /// <returns>true => thành công | false => thất bại</returns>
        public string Update(VehicleOwnerModel vehicleOwnerModel)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    //Lấy đơn vị vận chuyển

                    var vehicleOwnerModelCheck = db.VehicleOwnerModels.Where(t => t.VehicleOwner == vehicleOwnerModel.VehicleOwner).FirstOrDefault();
                    
                    vehicleOwnerModelCheck.VehicleOwnerName = vehicleOwnerModel.VehicleOwnerName;

                    vehicleOwnerModelCheck.Actived = vehicleOwnerModel.Actived;

                    db.SaveChanges();

                    return "Lưu thành công";
                }


            }
            catch (Exception)
            {
                return "Lưu thất bại";
            }
        }
    
    }
}

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

namespace BLL.DAO
{
    public class DriverRegisterDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;


        /// <summary>
        /// lấy danh sách tài xế theo công ty
        /// </summary>
        /// <param name="codekhachang">code công ty 100010020</param>
        /// <returns></returns>
        public IEnumerable<DriverRegister> GetAll(string codekhachang)
        {

            var lstCompany = new List<DriverRegister>();

            using (DbConnection db = new DbConnection())
            {
                var idowner = db.ProviderModels.Where(t => t.ProviderCode == codekhachang).FirstOrDefault();


                if (idowner != null)
                {

                    lstCompany = db.DriverRegisters.Where(t => t.OwnerId == idowner.ProviderId && t.Active == true).ToList();
                }

            }

            return lstCompany;
        }

        public string[] GetAllUsingDapper(object codekhachang, object text)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    //sai chọn
                    var idowner = connection.Query<CustomerModel>(@"select * from CustomerModel where CustomerCode = '" + codekhachang + "'");

                    if(idowner.Count() == 0)
                    {
                        return null;
                    }

                    var p = new DynamicParameters();

                    p.Add("OwnerID", (string)idowner.FirstOrDefault().CustomerId.ToString());

                    p.Add("text", (string)text);

                    string[] arr = connection.Query<string>(@"select top 10 @text + ' | ' + DriverCardNo+ ' | ' + DriverName + ' | ' + CONVERT(varchar, CreateDate,101) + ' | ' + Place from DriverRegister
                where OwnerId = @OwnerID and Active = 1 and (DriverCardNo + ' | ' + DriverName) like N'%' +  @text + '%'", p).ToArray();

                    return arr;
                }
            }
            catch (Exception)
            {

                return  null;
            }
           
        }

        /// <summary>
        /// Return tên tài xế theo CMND/GPXL
        /// </summary>
        /// <param name="DriverCardNo"></param>
        /// <returns></returns>
        public string GetNameVehicle(string DriverCardNo)
        {
            using (DbConnection db = new DbConnection())
            {
                var Drivers = db.DriverRegisters.Where(t => t.DriverCardNo == DriverCardNo).FirstOrDefault();

                if (Drivers != null)
                {
                    return Drivers.DriverName;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Lấy tất cả tài xế không phân biệt khác hàng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MDriverRegister> GetAll(string CustomerCode = "", string HoTen = "", string CMND = "")
        {
            using (DbConnection db = new DbConnection())
            {

                var lstTaiXe = db.DriverRegisters.Where(t => t.Active == true).AsQueryable();
                if (!string.IsNullOrEmpty(CustomerCode))
                {
                    var customerID = db.CustomerModels.Where(t => t.CustomerCode == CustomerCode);
                    if (customerID.Count() != 0)
                    {
                        lstTaiXe = lstTaiXe.Where(t => t.OwnerId == customerID.FirstOrDefault().CustomerId);
                    }

                }
                if (!string.IsNullOrEmpty(HoTen))
                {
                    lstTaiXe = lstTaiXe.Where(t => t.DriverName.Contains(HoTen));
                }
                if (!string.IsNullOrEmpty(CMND))
                {
                    lstTaiXe = lstTaiXe.Where(t => t.DriverCardNo.Contains(CMND));
                }






                var result = from a in lstTaiXe.ToList()
                             from b in db.CustomerModels.ToList()
                             where a.OwnerId == b.CustomerId
                             select new MDriverRegister()
                             {
                                 DriverId = a.DriverId.ToString(),
                                 DriverName = a.DriverName,
                                 DriverCardNo = a.DriverCardNo,
                                 CreateDate = a.CreateDate,
                                 Place = a.Place,
                                 CustomerName = b.CustomerName,
                                 CustomerCode = b.CustomerCode.ToString()
                             };

                return result.ToList();

            }


        }


        public string Insert(DriverRegister driverRegister, string CustomerCode)
        {
            using (DbConnection db = new DbConnection())
            {
                if(!KiemTraTrungTaiXe(CustomerCode, driverRegister.DriverName, driverRegister.DriverCardNo)){

                    return "Không thể thêm do tài xế đã có !";
                }
                else
                {

                    var customerID = db.CustomerModels.Where(t => t.CustomerCode == CustomerCode).FirstOrDefault().CustomerId;

                    driverRegister.OwnerId = customerID;

                    db.DriverRegisters.Add(driverRegister);

                    db.SaveChanges();

                    return "Thêm thành công";

                }
            }

        }

        public bool KiemTraTrungTaiXe(string CustomerCode, string HoTen, string CMND)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var p2 = new DynamicParameters();

                p2.Add("CustomerCode",CustomerCode);


                var CustomerID = connection.Query<CustomerModel>(@"select * from CustomerModel where CustomerCode = @CustomerCode",p2).FirstOrDefault().CustomerId;

                var p = new DynamicParameters();

                p.Add("OwnerID", CustomerID.ToString());

                p.Add("HoTen", HoTen);

                p.Add("CMND", CMND);



                var idowner = connection.Query<DriverRegister>(@"select * from DriverRegister where OwnerId = @OwnerID and DriverName = @HoTen and DriverCardNo = @CMND", p);


                if (idowner.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


        }

        public string Update(DriverRegister driverRegister)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    var driver = db.DriverRegisters.Find(driverRegister.DriverId);
                    driver.DriverName = driverRegister.DriverName;
                    driver.DriverCardNo = driverRegister.DriverCardNo;
                    driver.CreateDate = driverRegister.CreateDate;
                    driver.Place = driverRegister.Place;

                    db.SaveChanges();

                    return "Sửa thành công";
                }
            }
            catch (Exception)
            {

                return "Sửa thất bại";
            }
            

        }

        public string Delete(string DriverId)
        {
            try
            {
                using (DbConnection db = new DbConnection())
                {
                    var driver = db.DriverRegisters.Find(Guid.Parse(DriverId));

                    driver.Active = false;

                    db.SaveChanges();

                    return "Xóa thành công";
                }
            }
            catch (Exception)
            {

                return "Xóa thất bại";
            }


        }
    }
}

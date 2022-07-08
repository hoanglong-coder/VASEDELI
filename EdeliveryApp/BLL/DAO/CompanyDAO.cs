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
using Dapper;

namespace BLL.DAO
{
    public class CompanyDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        /// <summary>
        /// Lấy danh sách nơi nhận hàng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyModel> GetAll(string CompanyCode)
        {
            var lstCompany = new List<CompanyModel>();

            using (DbConnection db = new DbConnection())
            {
                lstCompany = db.CompanyModels.Where(t=>t.CompanyCode==CompanyCode).Select(t => t).ToList();
            }

            return lstCompany;
        }

        public string GetMaNoiGiaoNhan(int id)
        {
            using (DbConnection db = new DbConnection())
            {
                var lst = db.CompanyModels.Find(id);

                return lst.DbName;

            }
        }

        /// <summary>
        /// Lấy đơn vị company không được trùng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyModel> GetAllDonVi()
        {
            var lstCompany = new List<CompanyModel>();

            using (DbConnection db = new DbConnection())
            {
                lstCompany = db.CompanyModels.GroupBy(t => t.CompanyCode).Select(t => t.FirstOrDefault()).ToList();

                var result = new List<CompanyModel>();

                foreach (var item in lstCompany)
                {

                    CompanyModel company = new CompanyModel();
                    company.CompanyCode = item.CompanyCode;
                    string[] namecode = item.CompanyName.Split('_');
                    company.CompanyName = namecode[0] + "_"+ item.CompanyCode;
                    result.Add(company);

                }
                return result;
            }

            return lstCompany;
        }

        public string[] GetAllUsingDapper(object obj)
        {
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("text", (string)obj);

                string[] arr = connection.Query<string>(@"select  DISTINCT top 10 @text + ' | ' + SUBSTRING(CompanyName, 0, CHARINDEX('_', CompanyName))+ ' | ' + CompanyCode  from CompanyModel where (CompanyName + ' | ' + CompanyCode) like N'%' + @text + '%'", p).ToArray();

                return arr;
            }
        }

        public string[] GetAllDonViUsingDapper(object branchCode , object obj)
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

                string[] arr = connection.Query<string>(@"select @text + ' | ' + CompanyName + ' | ' + CompanyCode + ' | ' + CONVERT(varchar(10), ID)  from CompanyModel   where CompanyCode = @branchcode and (CompanyName + ' | ' + CompanyCode) like N'%' + @text + '%'", p).ToArray();

                return arr;
            }
        }
    }
}

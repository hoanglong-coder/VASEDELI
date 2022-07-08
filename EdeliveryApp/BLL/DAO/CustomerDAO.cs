using DAL.EntitiesFramwork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAO
{
    public class CustomerDAO
    {
        string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public CustomerModel Get(string CustomerCode)
        {
            //using (DbConnection db = new DbConnection())
            //{
            //    return db.CustomerModels.Where(t=>t.CustomerCode == CustomerCode).FirstOrDefault();
            //}

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("CustomerCode", CustomerCode);

                var arr = connection.Query<CustomerModel>(@"select * from CustomerModel where CustomerCode = @CustomerCode", p).FirstOrDefault();

                return arr;
            }
        }
     
    }
}

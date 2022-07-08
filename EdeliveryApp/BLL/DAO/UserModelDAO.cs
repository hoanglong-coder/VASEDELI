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
    public class UserModelDAO
    {
        static string Connection = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public static UserModel KiemTraDangNhap(string UserName, string PassWord, string CompanyCode)
        {

            string pwdMD5 = encryptMD5(PassWord);
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("username", (string)UserName);
                p.Add("password", pwdMD5);
                p.Add("companycode", CompanyCode);

                var arr = connection.Query<UserModel>(@"select * from UserModel where UserName = @username and PasswordEnscrypt = @password and CompanyCode = @companycode and Actived = 1", p).FirstOrDefault();

                if (arr != null)
                {
                    return arr;
                }else
                {
                    return null;
                }

               
            }
        }

        public static UserModel KiemTraDangNhapAdmin(string UserName, string PassWord)
        {

            string pwdMD5 = encryptMD5(PassWord);
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("username", (string)UserName);
                p.Add("password", pwdMD5);

                var arr = connection.Query<UserModel>(@"select * from UserModel where UserName = @username and PasswordEnscrypt = @password and Actived = 1", p).FirstOrDefault();

                if (arr != null)
                {
                    return arr;
                }
                else
                {
                    return null;
                }


            }
        }
        public static UserModel KiemTraNhanVienKho(string UserName, string PassWord)
        {

            string pwdMD5 = encryptMD5(PassWord);
            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("username", (string)UserName);
                p.Add("password", pwdMD5);

                var arr = connection.Query<UserModel>(@"select * from UserModel where UserName = @username and PasswordEnscrypt = @password and Actived = 1", p).FirstOrDefault();

                if (arr != null)
                {
                    return arr;
                }
                else
                {
                    return null;
                }


            }
        }
        public static string encryptMD5(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
        public IEnumerable<UserModel> GetAll(string search = "")
        {

            using (IDbConnection connection = new SqlConnection(Connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var p = new DynamicParameters();
                p.Add("search", search);

                var arr = connection.Query<UserModel>(@"select * from UserModel where Actived = 1", p);

                return arr;
            }

        }


        public bool CheckConnection()
        {         
            try
            {
                using (IDbConnection connection = new SqlConnection(Connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }


                    connection.Close();
                }
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }


        public bool IsDbConnectionOK()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    };
                    connection.Close();
                    return true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

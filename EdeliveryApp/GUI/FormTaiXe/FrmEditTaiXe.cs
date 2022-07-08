using BLL.DAO;
using DAL.EntitiesFramwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormTaiXe
{
    public partial class FrmEditTaiXe : Form
    {
        DriverRegisterDAO driverRegisterDAO = new DriverRegisterDAO();

        string driverId;

        string CustomerCode;

        string CustomerName;

        string DriverName;

        string DriverCardNo;

        string CreateDate;

        string Place;
        public FrmEditTaiXe(string _driverId, string _customercode, string _customerName, string _driverName, string _driverCardNo, string _creatDate, string _place)
        {
            InitializeComponent();
            driverId = _driverId;
            CustomerCode = _customercode;
            CustomerName = _customerName;
            DriverName = _driverName;
            DriverCardNo = _driverCardNo;
            CreateDate = _creatDate;
            Place = _place;
        }

        private void FrmEditTaiXe_Load(object sender, EventArgs e)
        {
            txtTenKH.Text = CustomerName;
            txtMaKH.Text = CustomerCode;
            txtTenTX.Text = DriverName;
            txtCMND.Text = DriverCardNo;
            txtNoiCap.Text = Place;

            try
            {
                dtNgayCap.Value = DateTime.Parse(CreateDate);

            }
            catch (Exception)
            {
                string[] ngay = CreateDate.Split('/');

                string ngaycap = ngay[1] + '/' + ngay[0] + '/' + ngay[2];

                dtNgayCap.Value = DateTime.Parse(ngaycap);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string patter_cmnd_gplx = @"\b([0-9]{12})\b|\b([0-9]{9})\b";

            string patter_ten = @"^([a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+)((\s{1}[a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+){1,})$";

            Match ten = Regex.Match(txtTenTX.Text.ToLower(), patter_ten);

            Match cmnd = Regex.Match(txtCMND.Text, patter_cmnd_gplx);


            if (txtTenTX.Text == String.Empty)
            {
                MessageBox.Show("Tên tài xế không được để trống ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else if (txtCMND.Text == String.Empty)
            {
                MessageBox.Show("CCCD/GPLX xế không được để trống ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else if (!cmnd.Success)
            {

                MessageBox.Show("Số CMND/GPLX phải đúng định dạng 9 chữ số hoặc 12 chữ số ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else if (!ten.Success)
            {
                MessageBox.Show("Họ tên không đúng định dạng: Ví dụ không được chứ ký tự đặt biệt và chữ số ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);

            }
            else
            {


                const string message = "Bạn có muốn thêm tài xế?";
                const string caption = "Xác nhận thêm tài xế";


                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.OK)
                {
                    var driverRegister = new DriverRegister();
                    driverRegister.DriverId = Guid.Parse(driverId);
                    driverRegister.DriverName = txtTenTX.Text;
                    driverRegister.DriverCardNo = txtCMND.Text;
                    driverRegister.CreatedTime = DateTime.Now;
                    driverRegister.CreateDate = dtNgayCap.Value;
                    driverRegister.Active = true;
                    driverRegister.Place = txtNoiCap.Text;

                    MessageBox.Show(driverRegisterDAO.Update(driverRegister));

                    this.Close();
                }

            }
        }
    }
}

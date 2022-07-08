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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormTaiXe
{
    public partial class FrmAddTaiXe : Form
    {
        Edelivery_GNHDAO edelivery_GNH_TestDAO = new Edelivery_GNHDAO();

        DriverRegisterDAO DriverRegisterDAO = new DriverRegisterDAO();
        public FrmAddTaiXe()
        {
            InitializeComponent();
        }

        private void FrmAddTaiXe_Load(object sender, EventArgs e)
        {
            AutoCompleteCustomer();
        }


        private void button2_Click(object sender, EventArgs e)
        {

            string patter_cmnd_gplx = @"\b([0-9]{12})\b|\b([0-9]{9})\b";

            string patter_ten = @"^([a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+)((\s{1}[a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+){1,})$";
            
            Match ten = Regex.Match(txtTenTX.Text.ToLower(), patter_ten);

            Match cmnd = Regex.Match(txtCMND.Text, patter_cmnd_gplx);


            if (txtMaKH.Text == String.Empty)
            {
                MessageBox.Show("Mã khách hàng không được để trống", "Cảnh báo",
                                     MessageBoxButtons.OKCancel,
                                     MessageBoxIcon.Warning);
            }
            else if (txtTenTX.Text == String.Empty)
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
                    driverRegister.DriverId = Guid.NewGuid();
                    driverRegister.DriverName = txtTenTX.Text;
                    driverRegister.DriverCardNo = txtCMND.Text;
                    driverRegister.CreatedTime = DateTime.Now;
                    driverRegister.CreateDate = dtNgayCap.Value;
                    driverRegister.Active = true;
                    driverRegister.Place = txtNoiCap.Text;

                    MessageBox.Show(DriverRegisterDAO.Insert(driverRegister, txtMaKH.Text.Trim()));

                    this.Close();
                }

            }
        }

        private void txtTenKH_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtTenKH.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtTenKH.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtMaKH.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txtTenKH.ReadOnly = true;
                    }
                }
            }
        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadCustomerAutocomplete);
                        myThread.Start(t.Text);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AutoCompleteCustomer()
        {
            this.txtTenKH.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtTenKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadCustomerAutocomplete(object obj)
        {

            string[] arr = edelivery_GNH_TestDAO.GetThongTinKhachHangUsingDapper("", obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteCustomer(collection);
            }
        }

        private delegate void SetAutoCompleteCustomerCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteCustomer(AutoCompleteStringCollection collection)
        {
            if (this.txtTenKH.InvokeRequired)
            {
                SetAutoCompleteCustomerCallback d = new SetAutoCompleteCustomerCallback(SetAutoCompleteCustomer);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtTenKH.AutoCompleteCustomSource = collection;
                if (txtTenKH.Text != null && txtTenKH.Text.Length > 1 &&
                    txtTenKH.Text.Substring(txtTenKH.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtTenKH.Text = "";
            txtMaKH.Text = "";
            txtTenKH.ReadOnly = false;
        }

        private void txtTenTX_TextChanged(object sender, EventArgs e)
        {
            txtTenTX.SelectionStart = txtTenTX.Text.Length;
            txtTenTX.Text = txtTenTX.Text.ToUpper();
        }
    }
}

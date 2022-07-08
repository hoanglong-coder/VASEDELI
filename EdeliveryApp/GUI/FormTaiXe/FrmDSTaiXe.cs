using BLL.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormTaiXe
{
    public partial class FrmDSTaiXe : Form
    {
        DriverRegisterDAO driverRegisterDAO = new DriverRegisterDAO();
        Edelivery_GNHDAO edelivery_GNH_TestDAO = new Edelivery_GNHDAO();
        public FrmDSTaiXe()
        {
            InitializeComponent();
        }

        private void FrmDSTaiXe_Load(object sender, EventArgs e)
        {
            LoadDatagirdView();
            AutoCompleteCustomer();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RemoveColumn()
        {

        }

        public void LoadDatagirdView()
        {
            dataGridView1.DataSource = driverRegisterDAO.GetAll();
            RemoveColumn();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            dataGridView1.DataSource = driverRegisterDAO.GetAll(txtMaKH.Text,txtHotenTX.Text,txtCCCD.Text);
            RemoveColumn();
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

        private void button4_Click(object sender, EventArgs e)
        {

            txtTenKH.Text = "";
            txtMaKH.Text = "";
            txtTenKH.ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmAddTaiXe frm = new FrmAddTaiXe();

            frm.ShowDialog();

            dataGridView1.DataSource = driverRegisterDAO.GetAll(txtMaKH.Text, txtHotenTX.Text, txtCCCD.Text);
            RemoveColumn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string driverId = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string CustomerId = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            string CustomerName = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            string DriverName = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string DriverCardNo = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            string CreateDate = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            string Place = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            FrmEditTaiXe frm = new FrmEditTaiXe(driverId,CustomerId,CustomerName,DriverName,DriverCardNo,CreateDate,Place);

            frm.ShowDialog();
        }

        private void btnXoaBo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string driverId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string taixe = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    string message = "Bạn có muốn xóa tài xế " + taixe + " ?";
                    string caption = "Xác nhận xóa thông tin tài xế";


                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Question);

                    // If the no button was pressed ...
                    if (result == DialogResult.OK)
                    {
                        var kq = driverRegisterDAO.Delete(driverId);

                        MessageBox.Show(kq, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn tài xế");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi" + ex.Message);
            }
        }
    }
}

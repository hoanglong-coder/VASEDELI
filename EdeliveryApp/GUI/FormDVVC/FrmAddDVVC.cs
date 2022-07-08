using BLL.DAO;
using DAL.EntitiesFramwork;
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

namespace GUI.FormDVVC
{
    public partial class FrmAddDVVC : Form
    {
        Edelivery_GNHDAO edelivery_GNHDAO = new Edelivery_GNHDAO();
        public FrmAddDVVC()
        {
            InitializeComponent();
           
        }
        private void FrmAddDVVC_Load(object sender, EventArgs e)
        {
            AutoCompleteCustomer();
        }
        #region AutoComplete Khách hàng

        private void AutoCompleteCustomer()
        {
            this.txtTenKH.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtTenKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadCustomerAutocomplete(object obj)
        {

            string[] arr = edelivery_GNHDAO.GetThongTinKhachHangUsingDapper("", obj);

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
        #endregion AutoComplete Khách hàng

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

        private void button2_Click(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(txtMaDVVC.Text))
            {
                MessageBox.Show("Mã đơn vị vận chuyển không được để trống!");
                return;
            }
            if (string.IsNullOrEmpty(txtTenDVVC.Text))
            {
                MessageBox.Show("Tên đơn vị vận chuyển không được để trống!");
                return;
            }
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Khách hàng hoặc Nội bộ không được để trống!");
                return;
            }
            const string message = "Bạn có muốn lưu thông tin đơn vị vận chuyển?";
            const string caption = "Xác nhận lưu thông tin đơn vị vận chuyển";


            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Question);
            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                VehicleOwnerModel DVVC = new VehicleOwnerModel();
                DVVC.VehicleOwner = txtMaDVVC.Text.Trim();
                DVVC.VehicleOwnerName = txtTenDVVC.Text.Trim();
                DVVC.ProviderCode = null;
                DVVC.CustomerCode = txtMaKH.Text.Trim();
                DVVC.Actived = true;

                VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();

                MessageBox.Show(vehicleOwnerModelDAO.Insert(DVVC));
            }

                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DAO;
using BLL.Export;
using Constant;
using DAL.Model;
using PagedList;
namespace GUI.FormEdelivery
{
    public partial class FrmKeHoachGiaoNhanTongHop : Form
    {
        int pageNumber = 1;

        DKEdeliveryDAO dKEdeliveryDAO = new DKEdeliveryDAO();

        Edelivery_GNHDAO edelivery_GNHDAO = new Edelivery_GNHDAO();

        CompanyDAO companyDAO = new CompanyDAO();

        IPagedList<ViewKeHoachGiaoNhan> list;
        public FrmKeHoachGiaoNhanTongHop()
        {
            InitializeComponent();
        }

        private void FrmKeHoachGiaoNhanTongHop_Load(object sender, EventArgs e)
        {
            AutoCompleteDVVC(txtSearchDonVi);
            AutoCompleteDVVC(txtSearchTenKH);
            AutoCompleteDVVC(txtSearchNoiGiaoNhan);
            AutoCompleteDVVC(txtSearchSONumber);
            LoadCombobox();
            GetAllDataGridView();
        }
        //load combobox
        public void LoadCombobox()
        {
            List<StatusDK_Edelivery> ListcbTT = new List<StatusDK_Edelivery>() {
                 new StatusDK_Edelivery(){Value = -1, Name ="Tất cả"},
                new StatusDK_Edelivery(){Value = Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua, Name ="Đang chờ phê duyệt"},
                new StatusDK_Edelivery(){Value = Constant.ConstantStatusDK_EDelivery.DangXacNhan, Name ="Đang xác nhận"},
                new StatusDK_Edelivery(){Value = Constant.ConstantStatusDK_EDelivery.DaXacNhan, Name ="Đã xác nhận"},
                new StatusDK_Edelivery(){Value =  Constant.ConstantStatusDK_EDelivery.DangPheDuyet, Name ="Đang phê duyệt"},
                new StatusDK_Edelivery(){Value =  Constant.ConstantStatusDK_EDelivery.PheDuyetThanhCong_HoanThanh, Name ="Phê duyệt thành công - Hoàn thành"},
            };

            cbTrangThai.DataSource = ListcbTT;
            cbTrangThai.DisplayMember = "Name";
            cbTrangThai.ValueMember = "Value";
        }
        //Tìm kiếm
        private void button3_Click(object sender, EventArgs e)
        {

            list = dKEdeliveryDAO.GetAllKeHoachTongHop(searchEdelivery());

            dataGridView1.Refresh();

            dataGridView1.DataSource = list.ToList();

            btnPrevious.Enabled = list.HasPreviousPage;

            btnNext.Enabled = list.HasNextPage;

            lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            VisibleColumn();
        
        }

        private SearchEdelivery searchEdelivery()
        {
            ////khởi tạo search
            SearchEdelivery search = new SearchEdelivery();

            search.BranchCode = txtSearchMaDonVi.Text;

            search.SONumber = txtSearchSONumber.Text;

            search.MaKhachHang = txtSearchMaKH.Text;   

            search.NgayBatDau = dtngaybatdau.Value.Date;

            search.NgayKetThuc = dtngayketthuc.Value.Date;

            search.IDCompanyCode = txtSeachMaNoiGiaoNhan.Text;

            search.TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString());

            return search;
        }

        public void GetAllDataGridView()
        {
            

            list = dKEdeliveryDAO.GetAllKeHoachTongHop(searchEdelivery());

            btnPrevious.Enabled = list.HasPreviousPage;

            btnNext.Enabled = list.HasNextPage;

            dataGridView1.DataSource = list.ToList();

            lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            VisibleColumn();
        }

        /// <summary>
        /// ẩn column không mong muốn
        /// </summary>
        private void VisibleColumn()
        {
            dataGridView1.Columns["IDEdelivery"].Visible = false;
            dataGridView1.Columns["MaNoiGiaoNhan"].Visible = false;
            dataGridView1.Columns["MaDonViVanChuyen"].Visible = false;
            dataGridView1.Columns["NgayCapCMND_CCCD"].Visible = false;
            dataGridView1.Columns["NoiCapCMND_CCCD"].Visible = false;
            dataGridView1.Columns["VehicleKey"].Visible = false;
            dataGridView1.Columns["IDCompanyCode"].Visible = false;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (list.HasPreviousPage)
            {

                list = dKEdeliveryDAO.GetAllKeHoachTongHop(searchEdelivery(), --pageNumber);

                btnPrevious.Enabled = list.HasPreviousPage;

                btnNext.Enabled = list.HasNextPage;

                dataGridView1.DataSource = list.ToList();

                lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (list.HasNextPage)
            {

                list = dKEdeliveryDAO.GetAllKeHoachTongHop(searchEdelivery(), ++pageNumber);

                btnPrevious.Enabled = list.HasPreviousPage;

                btnNext.Enabled = list.HasNextPage;

                dataGridView1.DataSource = list.ToList();

                lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            }
        }

        private void AutoCompleteDVVC(TextBox textBox)
        {
            textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }


        #region AutoComplete Đơn vị

        private void ThreadCompanyCodeEntry(object obj)
        {
            string[] arr = companyDAO.GetAllUsingDapper(obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteCompanyCode(collection);
            }
        }

        private delegate void SetAutoCompleteCompanyCodeCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteCompanyCode(AutoCompleteStringCollection collection)
        {
            if (this.txtSearchDonVi.InvokeRequired)
            {
                SetAutoCompleteCompanyCodeCallback d = new SetAutoCompleteCompanyCodeCallback(SetAutoCompleteCompanyCode);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearchDonVi.AutoCompleteCustomSource = collection;
                if (txtSearchDonVi.Text != null && txtSearchDonVi.Text.Length > 1 &&
                    txtSearchDonVi.Text.Substring(txtSearchDonVi.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }


        private void txtSearchDonVi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadCompanyCodeEntry);
                        myThread.Start(t.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchDonVi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtSearchDonVi.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtSearchDonVi.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtSearchMaDonVi.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txtSearchDonVi.ReadOnly = true;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtSearchDonVi.Text = "";
            txtSearchMaDonVi.Text = "";
            txtSearchDonVi.ReadOnly = false;
        }

        #endregion AutoComplete Đơn vị

        #region AutoComplete Khách hàng

        private void ThreadCustomerAutocomplete(object obj)
        {

            string[] arr = edelivery_GNHDAO.GetThongTinKhachHangUsingDapper(txtSearchMaDonVi.Text, obj);

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
            if (this.txtSearchTenKH.InvokeRequired)
            {
                SetAutoCompleteCustomerCallback d = new SetAutoCompleteCustomerCallback(SetAutoCompleteCustomer);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearchTenKH.AutoCompleteCustomSource = collection;
                if (txtSearchTenKH.Text != null && txtSearchTenKH.Text.Length > 1 &&
                    txtSearchTenKH.Text.Substring(txtSearchTenKH.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void txtSearchTenKH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDonVi.Text != string.Empty)
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
                else
                {
                    MessageBox.Show("Bạn chưa chọn đơn vị");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchTenKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtSearchTenKH.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtSearchTenKH.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtSearchMaKH.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txtSearchTenKH.ReadOnly = true;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtSearchTenKH.Text = "";
            txtSearchMaKH.Text = "";
            txtSearchTenKH.ReadOnly = false;
        }
        #endregion AutoComplete Khách hàng

        #region AutoComplete Nơi giao nhận
        private void ThreadNoiGiaoNhanAutocomplete(object obj)
        {

            string[] arr = companyDAO.GetAllDonViUsingDapper(txtSearchMaDonVi.Text, obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteNoiGiaoNhan(collection);
            }
        }

        private delegate void SetAutoNoiGiaoNhanCustomerCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteNoiGiaoNhan(AutoCompleteStringCollection collection)
        {
            if (this.txtSearchNoiGiaoNhan.InvokeRequired)
            {
                SetAutoNoiGiaoNhanCustomerCallback d = new SetAutoNoiGiaoNhanCustomerCallback(SetAutoCompleteNoiGiaoNhan);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearchNoiGiaoNhan.AutoCompleteCustomSource = collection;
                if (txtSearchNoiGiaoNhan.Text != null && txtSearchNoiGiaoNhan.Text.Length > 1 &&
                    txtSearchNoiGiaoNhan.Text.Substring(txtSearchNoiGiaoNhan.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void txtSearchNoiGiaoNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtSearchNoiGiaoNhan.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtSearchNoiGiaoNhan.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtSeachMaNoiGiaoNhan.Text = value.Substring(value.IndexOf(" | ") + 10);

                        txtSearchNoiGiaoNhan.ReadOnly = true;
                    }
                }
            }

        }

        private void txtSearchNoiGiaoNhan_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtSearchDonVi.Text != string.Empty)
                {
                    TextBox t = sender as TextBox;
                    if (t != null)
                    {
                        //say you want to do a search when user types 2 or more chars
                        if (t.Text.Length >= 2)
                        {
                            var myThread = new Thread(ThreadNoiGiaoNhanAutocomplete);
                            myThread.Start(t.Text);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn đơn vị");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            txtSearchNoiGiaoNhan.Text = "";
            txtSeachMaNoiGiaoNhan.Text = "";
            txtSearchNoiGiaoNhan.ReadOnly = false;
        }


        #endregion AutoComplete Nơi giao nhận

        #region AutoComplete Mã đơn hàng 

        private void ThreadSONumberEntry(object obj)
        {
            string[] arr = edelivery_GNHDAO.GetSONumberUsingDapper(txtSearchMaDonVi.Text, obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteSONumber(collection);
            }
        }

        private delegate void SetAutoCompleteSONumberCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteSONumber(AutoCompleteStringCollection collection)
        {
            if (this.txtSearchSONumber.InvokeRequired)
            {
                SetAutoCompleteSONumberCallback d = new SetAutoCompleteSONumberCallback(SetAutoCompleteSONumber);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearchSONumber.AutoCompleteCustomSource = collection;
                if (txtSearchSONumber.Text != null && txtSearchSONumber.Text.Length > 1 &&
                    txtSearchSONumber.Text.Substring(txtSearchSONumber.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }
        
        private void txtSearchSONumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDonVi.Text != string.Empty)
                {
                    TextBox t = sender as TextBox;
                    if (t != null)
                    {
                        //say you want to do a search when user types 2 or more chars
                        if (t.Text.Length >= 2)
                        {
                            var myThread = new Thread(ThreadSONumberEntry);
                            myThread.Start(t.Text);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn đơn vị");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchSONumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtSearchSONumber.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtSearchSONumber.Text = value;

                        txtSearchSONumber.ReadOnly = true;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtSearchSONumber.Text = "";
            txtSearchSONumber.ReadOnly = false;
        }


        #endregion  AutoComplete Mã đơn hàng 

    }
}

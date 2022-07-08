using BLL.DAO;
using Constant;
using DAL.EntitiesFramwork;
using DAL.Model;
using GUI.FormEdelivery;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace GUI.FromInstance
{
    public partial class FrmKho : Form
    {
        CompanyDAO companyDAO = new CompanyDAO();
        Edelivery_GNHDAO edelivery_GNHDAO = new Edelivery_GNHDAO();
        DKEdeliveryDAO dKEdeliveryDAO = new DKEdeliveryDAO();
        UserModel _user;
        public FrmKho(UserModel user)
        {
            _user = user;
            InitializeComponent();
        }

        private void FrmKho_Load(object sender, EventArgs e)
        {
            LoadCombobox();

            //AutoCompleteDVVC(txtSearchDonVi);

            #region Auto Đơn vị
            CompanyDAO companyDAO = new CompanyDAO();

            string[] arr = companyDAO.GetAllUsingDapper(_user.CompanyCode);

            String selItem = arr[0];

            if (selItem.Contains(" | "))
            {
                string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                if ((value.IndexOf(" | ") + 3) < value.Length)
                {
                    //Chọn lại kho nhập
                    txtSearchDonVi.Text = value.Substring(0, value.IndexOf(" | "));

                    //Chọn lại mã kho nhập
                    txtSearchMaDonVi.Text = value.Substring(value.IndexOf(" | ") + 3);

                }
            }
            #endregion Auto Đơn vị

            AutoCompleteDVVC(txtSearchTenKH);

            //AutoCompleteDVVC(txtSearchNoiGiaoNhan);

            LoadFirst();

            VisibleColumn();

        }

        //load combobox
        public void LoadCombobox()
        {
            List<StatusDK_Edelivery> ListcbVC = new List<StatusDK_Edelivery>() {
                new StatusDK_Edelivery(){Value = 1, Name ="Tất cả"},
                new StatusDK_Edelivery(){Value = 2, Name ="Khách hàng"},
                new StatusDK_Edelivery(){Value = 3, Name ="Nội bộ"},
            };

            List<StatusDK_Edelivery> ListcbTT = new List<StatusDK_Edelivery>() {
                 new StatusDK_Edelivery(){Value = -1, Name ="Tất cả"},
                new StatusDK_Edelivery(){Value = Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua, Name ="Tạo mới"},
                new StatusDK_Edelivery(){Value = Constant.ConstantStatusDK_EDelivery.DangXacNhan, Name ="Đang xử lý"},
                new StatusDK_Edelivery(){Value =  Constant.ConstantStatusDK_EDelivery.DaXacNhan, Name ="Đã chuyển xử lý"},
                new StatusDK_Edelivery(){Value =  Constant.ConstantStatusDK_EDelivery.DangPheDuyet, Name ="Đang phê duyệt"},
                new StatusDK_Edelivery(){Value =  Constant.ConstantStatusDK_EDelivery.PheDuyetThanhCong_HoanThanh, Name ="Phê duyệt thành công - Hoàn thành"},
            };

            cbLoaiVC.DataSource = ListcbVC;
            cbLoaiVC.DisplayMember = "Name";
            cbLoaiVC.ValueMember = "Value";

            cbTrangThai.DataSource = ListcbTT;
            cbTrangThai.DisplayMember = "Name";
            cbTrangThai.ValueMember = "Value";
        }

        private void VisibleColumn()
        {
            dataGridView1.Columns["MaDonViVanChuyen"].Visible = false;
            dataGridView1.Columns["MaNoiGiaoNhan"].Visible = false;
            dataGridView1.Columns["IDCompanyCode"].Visible = false;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            SearchMDSEdelivery search = new SearchMDSEdelivery()
            {
                BranchCode = txtSearchMaDonVi.Text,
                MaKhachHang = txtSearchMaKH.Text,
                //MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                TuNgay = dtngaybatdau.Value.Date,
                DenNgay = dtngayketthuc.Value.Date,
                LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
            };


            dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAOCHOKHO(search, _user.CompanyCode);
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



        //Load lần đầu Danh sách khai báo
        private void LoadFirst()
        {
            SearchMDSEdelivery search = new SearchMDSEdelivery()
            {
                BranchCode = txtSearchMaDonVi.Text,
                MaKhachHang = txtSearchMaKH.Text,
                //MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                TuNgay = dtngaybatdau.Value.Date,
                DenNgay = dtngayketthuc.Value.Date,
                LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
            };
          


            dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAOCHOKHO(search, _user.CompanyCode);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var lst = dKEdeliveryDAO.ShowInstance(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            FrmShowInstance frm = new FrmShowInstance(lst);
            frm.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SearchMDSEdelivery search = new SearchMDSEdelivery()
            {
                BranchCode = txtSearchMaDonVi.Text,
                MaKhachHang = txtSearchMaKH.Text,
                //MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                TuNgay = dtngaybatdau.Value.Date,
                DenNgay = dtngayketthuc.Value.Date,
                LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
            };


            dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAOCHOKHO(search, _user.CompanyCode);
        }
    }
}

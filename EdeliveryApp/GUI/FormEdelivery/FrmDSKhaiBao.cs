using BLL.DAO;
using Constant;
using DAL.EntitiesFramwork;
using DAL.Model;
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

namespace GUI.FormEdelivery
{
    public partial class FrmDSKhaiBao : Form
    {
        CompanyDAO companyDAO = new CompanyDAO();
        Edelivery_GNHDAO edelivery_GNHDAO = new Edelivery_GNHDAO();
        DKEdeliveryDAO dKEdeliveryDAO = new DKEdeliveryDAO();
        UserModel _user;
        public FrmDSKhaiBao(UserModel user)
        {
            _user = user;
            InitializeComponent();
        }

        private void FrmDSKhaiBao_Load(object sender, EventArgs e)
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

            AutoCompleteDVVC(txtSearchNoiGiaoNhan);

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


        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int checkselectrow = dataGridView1.SelectedRows.Count;


                    //checkselectrow < 0 => chưa chọn | checkselectrow > 1 => chỉ được phép chọn một dòng để sửa đổi
                    if (checkselectrow < 0)
                    {
                        MessageBox.Show("Chưa chọn dòng để sửa đổi", "Cảnh báo",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Warning);
                        return;
                    }
                    else if (checkselectrow > 1)
                    {
                        MessageBox.Show("Chỉ được phép chọn một dòng để sửa đổi", "Cảnh báo",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {


                        bool isOpne = false;

                        foreach (Form item in Application.OpenForms)
                        {
                            if (item.Name == "FrmSuaMatHang")
                            {
                                isOpne = true;
                                item.Focus();
                                MessageBox.Show("Chỉ được phép mở một form sửa thông tin", "Cảnh báo",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning);
                                break;
                            }
                        }


                        if (!isOpne)
                        {
                            //Kiểm tra dưới database | Nếu Status == 1 thì chặn không cho sửa | lấy mã đăng ký
                            //dKEdeliveryDAO.TrangThaiDkEdelivery(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())) == Constant.ConstantStatusDK_EDelivery.DangPheDuyet
                            if (dKEdeliveryDAO.TrangThaiDkEdelivery(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())) != Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua)
                            {
                                MessageBox.Show("Không được phép sửa vì đang xác nhận hoặc đang phê duyệt", "Cảnh báo",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning);

                                SearchMDSEdelivery search = new SearchMDSEdelivery()
                                {
                                    BranchCode = txtSearchMaDonVi.Text,
                                    MaKhachHang = txtSearchMaKH.Text,
                                    MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                                    TuNgay = dtngaybatdau.Value.Date,
                                    DenNgay = dtngayketthuc.Value.Date,
                                    LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                                    TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
                                };


                                dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAO(search,_user.UserId.ToString());
                                return;
                            }
                            ///Foreach Danh sách đăng ký coi xem SO nào có user đang đăng ký hay không
                            var usermodel = dKEdeliveryDAO.UserDangThaoTac(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                            if (usermodel.Count != 0)
                            {
                                string username = "";
                                foreach (var item in usermodel)
                                {
                                    username += item.FullName + ", ";
                                }



                                MessageBox.Show($"Không được phép sửa vì các user {username} đang giữ đơn hàng đang khai báo", "Cảnh báo",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning);
                                return;
                            }

                            string MaKH = dataGridView1.CurrentRow.Cells[1].Value != null ? dataGridView1.CurrentRow.Cells[1].Value.ToString() : "";
                            string TenKH = dataGridView1.CurrentRow.Cells[2].Value != null ? dataGridView1.CurrentRow.Cells[2].Value.ToString() : "";
                            string DVVC = dataGridView1.CurrentRow.Cells[8].Value != null ? dataGridView1.CurrentRow.Cells[8].Value.ToString() : "";
                            string TenNoiGiaoNhan = dataGridView1.CurrentRow.Cells[4].Value != null ? dataGridView1.CurrentRow.Cells[4].Value.ToString() : "";
                            string NoiNhanHang = dataGridView1.CurrentRow.Cells[9].Value != null ? dataGridView1.CurrentRow.Cells[9].Value.ToString() : "";
                            int IDCompanyCode = int.Parse(dataGridView1.CurrentRow.Cells[10].Value.ToString());
                            string EDeliveryID = dataGridView1.CurrentRow.Cells[0].Value != null ? dataGridView1.CurrentRow.Cells[0].Value.ToString() : "";
                            DateTime NgayGiaoNhan = DateTime.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                            FrmSuaMatHang cf = new FrmSuaMatHang(MaKH,TenKH, DVVC, NoiNhanHang, TenNoiGiaoNhan, EDeliveryID, NgayGiaoNhan, IDCompanyCode, _user);

                            cf.MdiParent = FrmMain.ActiveForm;

                            cf.Show();
                        }
                    }



                }


            }
            catch (Exception)
            {


            }
        }

        private void VisibleColumn()
        {
            dataGridView1.Columns["MaDonViVanChuyen"].Visible = false;
            dataGridView1.Columns["MaNoiGiaoNhan"].Visible = false;
            dataGridView1.Columns["IDCompanyCode"].Visible = false;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            const string message = "Bạn có muốn hủy?";
            const string caption = "Xác nhận hủy";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                if (dKEdeliveryDAO.TrangThaiDkEdelivery(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())) != Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua)
                {
                    MessageBox.Show("Một số đăng ký bạn chọn Không được phép hủy vì đang xác nhận", "Cảnh báo",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning);
                    return;
                }
            }

            var result = MessageBox.Show(message, caption,
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var instance = dKEdeliveryDAO.HuyDKEdelivery(Guid.Parse(row.Cells[0].Value.ToString()));

                    if (string.IsNullOrEmpty(instance))
                    {

                        MessageBox.Show("Hủy thất bại", "Kết quả",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {

                        MessageBox.Show("Hủy thành công", "Kết quả",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Information);
                    }
                }
                SearchMDSEdelivery search = new SearchMDSEdelivery()
                {
                    BranchCode = txtSearchMaDonVi.Text,
                    MaKhachHang = txtSearchMaKH.Text,
                    MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                    TuNgay = dtngaybatdau.Value.Date,
                    DenNgay = dtngayketthuc.Value.Date,
                    LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                    TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
                };


                dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAO(search, _user.UserId.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchMDSEdelivery search = new SearchMDSEdelivery()
            {
                BranchCode = txtSearchMaDonVi.Text,
                MaKhachHang = txtSearchMaKH.Text,
                MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                TuNgay = dtngaybatdau.Value.Date,
                DenNgay = dtngayketthuc.Value.Date,
                LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
            };


            dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAO(search,_user.UserId.ToString());
        }
        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

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

        //Load lần đầu Danh sách khai báo
        private void LoadFirst()
        {
            SearchMDSEdelivery search = new SearchMDSEdelivery()
            {
                BranchCode = txtSearchMaDonVi.Text,
                MaKhachHang = txtSearchMaKH.Text,
                MaNoiGiaoNhan = txtSeachMaNoiGiaoNhan.Text,
                TuNgay = dtngaybatdau.Value.Date,
                DenNgay = dtngayketthuc.Value.Date,
                LoaiVanChuyen = int.Parse(cbLoaiVC.SelectedValue.ToString()),
                TrangThai = int.Parse(cbTrangThai.SelectedValue.ToString())
            };


            dataGridView1.DataSource = dKEdeliveryDAO.GETDSKHAIBAO(search, _user.UserId.ToString());
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var lst = dKEdeliveryDAO.ShowInstance(Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            FrmShowInstance frm = new FrmShowInstance(lst);
            frm.ShowDialog();
        }
    }
}

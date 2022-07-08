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
using BLL.DAO;
using Constant;
using DAL.Model;
using PagedList;
namespace GUI.FormDangKyXe
{
    public partial class FrmDsXe : Form
    {
        string _ConpanyCode;
        int pageNumber = 1;
        IPagedList<MVehicleModel> list;
        VehicleModelDAO VehicleModelDAO = new VehicleModelDAO();
        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();
        Edelivery_GNHDAO edelivery_GNHDAO = new Edelivery_GNHDAO();
        public FrmDsXe(string branchCode)
        {
            InitializeComponent();
            this._ConpanyCode = branchCode;
        }

        private void FrmDsXe_Load(object sender, EventArgs e)
        {
            loadCombobox();

            AutoCompleteDVVC();

            LoadDatagridView(); 
        }

        public void loadCombobox()
        {
            #region Combobox loại xe
            cbLoaiXe.DataSource = new List<StatusDK_Edelivery>() {

                new StatusDK_Edelivery()
                { Value = -1, Name = "Tất cả"},

                new StatusDK_Edelivery()
                { Value = Constant.ConstantVehicle.NoiBo, Name = "Nội bộ" },

                new StatusDK_Edelivery()
                { Value = Constant.ConstantVehicle.KhachHang, Name = "Khách hàng"}

                
            };

            cbLoaiXe.DisplayMember = "Name";

            cbLoaiXe.ValueMember = "Value";
            #endregion Combobox loại xe

            #region kiểu xe
            cbKieuXe.DataSource = new List<StatusDK_Edelivery>() {
                new StatusDK_Edelivery()
                { Value = -1, Name = "Tất cả"},

                new StatusDK_Edelivery()
                { Value = Constant.ConstantVehicle.XeThuong, Name = "Xe thường"},

                new StatusDK_Edelivery()
                { Value = Constant.ConstantVehicle.DauKeo, Name = "Xe đầu kéo" },

                 new StatusDK_Edelivery()
                { Value = Constant.ConstantVehicle.Romoc, Name = "Rơ moóc" },
            };

            cbKieuXe.DisplayMember = "Name";

            cbKieuXe.ValueMember = "Value";

            #endregion kiểu xe
        }

        private void LoadDatagridView()
        {
            list = VehicleModelDAO.GetAllDsXe(GetObjSearch());

            dataGridView1.Columns["VehicleId"].Visible = false;

            btnPrevious.Enabled = list.HasPreviousPage;

            btnNext.Enabled = list.HasNextPage;

            dataGridView1.DataSource = list.ToList();

            lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (list.HasPreviousPage)
            {            
                list = VehicleModelDAO.GetAllDsXe(GetObjSearch(), --pageNumber);
                dataGridView1.Columns["VehicleId"].Visible = false;
                btnPrevious.Enabled = list.HasPreviousPage;

                btnNext.Enabled = list.HasNextPage;

                dataGridView1.Refresh();

                dataGridView1.DataSource = list.ToList();

                lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (list.HasNextPage)
            {
                list = VehicleModelDAO.GetAllDsXe(GetObjSearch(), ++pageNumber);
                dataGridView1.Columns["VehicleId"].Visible = false;
                btnPrevious.Enabled = list.HasPreviousPage;

                btnNext.Enabled = list.HasNextPage;

                dataGridView1.Refresh();

                dataGridView1.DataSource = list.ToList();

                lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");

            }
        }

        private MVehicleModelSearch GetObjSearch()
        {
            MVehicleModelSearch mVehicleModelSearch = new MVehicleModelSearch();
            mVehicleModelSearch.VehicleNumber = txtSearchSoXe.Text;
            mVehicleModelSearch.Type = int.Parse(cbLoaiXe.SelectedValue.ToString());
            mVehicleModelSearch.DonViVanChuyen = txtSearchMaDonViVanChuyen.Text;
            mVehicleModelSearch.KieuXe = int.Parse(cbKieuXe.SelectedValue.ToString());
            return mVehicleModelSearch;
        }
        private int? CheckType(string name)
        {
            if (name == "Nội bộ")
            {
                return 1;
            }else if (name == "Khách hàng")
            {
                return 2;
            }else
            {
                return null;
            }
        }

        public bool? CheckKhoa(string name)
        {
            if (name == "Khóa")
            {
                return true;
            }else if (name == "Không khóa")
            {
                return false;
            }else
            {
                return null;
            }
        }
        public int? CheckXe(string name)
        {

            if (name == "Xe thường")
            {
                return Constant.ConstantVehicle.XeThuong;
            }else if (name == "Rơ moóc")
            {
                return Constant.ConstantVehicle.Romoc;
            }else if (name == "Đầu kéo")
            {
                return Constant.ConstantVehicle.DauKeo;
            }else
            {
                return null;
            }

        }

        public bool? CheckKhoaChinSua(string name)
        {
            if (name == "Khóa")
            {
                return true;
            }
            else if (name == "Không khóa")
            {
                return false;
            }
            else
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddXe frmThemXe = new FrmAddXe();

            frmThemXe.ShowDialog();
        }

        private void btnXoaBo_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string vehicleId = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                    string biensoxe = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                    string message = "Bạn có muốn xóa xe " + biensoxe + " ?";
                    string caption = "Xác nhận xóa thông tin xe";


                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Question);

                    // If the no button was pressed ...
                    if (result == DialogResult.OK)
                    {
                        var kq = VehicleModelDAO.RemoveXe(vehicleId);
                        if (kq)
                        {
                            MessageBox.Show("Xóa thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        list = VehicleModelDAO.GetAllDsXe(GetObjSearch());

                        dataGridView1.Columns["VehicleId"].Visible = false;

                        btnPrevious.Enabled = list.HasPreviousPage;

                        btnNext.Enabled = list.HasNextPage;

                        dataGridView1.DataSource = list.ToList();

                        lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn xe");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi"+ ex.Message);
            }
              
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string VehicleNumber = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string TenDVVC = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            FrmEditXe frm = new FrmEditXe(VehicleNumber, TenDVVC);

            frm.ShowDialog();
        }

        //Tìm kiếm
        private void button5_Click_1(object sender, EventArgs e)
        {
            pageNumber = 1;

            list = VehicleModelDAO.GetAllDsXe(GetObjSearch());

            dataGridView1.Columns["VehicleId"].Visible = false;

            dataGridView1.Refresh();

            dataGridView1.DataSource = list.ToList();

            btnPrevious.Enabled = list.HasPreviousPage;

            btnNext.Enabled = list.HasNextPage;

            lblPage.Text = String.Format($"Trang {pageNumber}/{list.PageCount}");
        }

        private void FrmDsXe_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        //nếu chọn loại xe là khách hàng thì load danh sách khách hàng: ngược lại load đơn vị vận chuyển
        #region AutoComplete Đơn vị vận chuyển

        private void AutoCompleteDVVC()
        {
            this.txtSearhDonViVanChuyen.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtSearhDonViVanChuyen.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadDVVCOwnerEntry(object obj)
        {
            string[] arr = vehicleOwnerModelDAO.GetALLUsingDapper(obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoDVVC(collection);
            }
        }

        private delegate void SetAutoCompleteDVVCCallback(AutoCompleteStringCollection collection);

        private void SetAutoDVVC(AutoCompleteStringCollection collection)
        {
            if (this.txtSearhDonViVanChuyen.InvokeRequired)
            {
                SetAutoCompleteDVVCCallback d = new SetAutoCompleteDVVCCallback(SetAutoDVVC);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearhDonViVanChuyen.AutoCompleteCustomSource = collection;
                if (txtSearhDonViVanChuyen.Text != null && txtSearhDonViVanChuyen.Text.Length > 1 &&
                    txtSearhDonViVanChuyen.Text.Substring(txtSearhDonViVanChuyen.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }


        #region Khách hàng

        private void ThreadDVVCKHOwnerEntry(object obj)
        {
            string[] arr = vehicleOwnerModelDAO.GetALLUsingDapperKhachHang(obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoDVVCKH(collection);
            }
        }

        private delegate void SetAutoCompleteDVVCKHCallback(AutoCompleteStringCollection collection);

        private void SetAutoDVVCKH(AutoCompleteStringCollection collection)
        {
            if (this.txtSearhDonViVanChuyen.InvokeRequired)
            {
                SetAutoCompleteDVVCKHCallback d = new SetAutoCompleteDVVCKHCallback(SetAutoDVVCKH);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtSearhDonViVanChuyen.AutoCompleteCustomSource = collection;
                if (txtSearhDonViVanChuyen.Text != null && txtSearhDonViVanChuyen.Text.Length > 1 &&
                    txtSearhDonViVanChuyen.Text.Substring(txtSearhDonViVanChuyen.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }
        #endregion Khách hàng


        private void txtSearhDonViVanChuyen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(cbLoaiXe.SelectedValue.ToString()) == -1)
                {
                    MessageBox.Show("Bạn chưa chọn Loại xe");
                    return;
                }
                else if (int.Parse(cbLoaiXe.SelectedValue.ToString()) == Constant.ConstantVehicle.NoiBo)
                {
                    TextBox t = sender as TextBox;
                    if (t != null)
                    {
                        //say you want to do a search when user types 2 or more chars
                        if (t.Text.Length >= 2)
                        {
                            var myThread = new Thread(ThreadDVVCOwnerEntry);
                            myThread.Start(t.Text);
                        }
                    }
                }
                else
                {
                    //load danh sách khách hàng
                    TextBox t = sender as TextBox;
                    if (t != null)
                    {
                        //say you want to do a search when user types 2 or more chars
                        if (t.Text.Length >= 2)
                        {
                            var myThread = new Thread(ThreadDVVCKHOwnerEntry);
                            myThread.Start(t.Text);
                        }
                    }
                    
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearhDonViVanChuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtSearhDonViVanChuyen.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtSearhDonViVanChuyen.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtSearchMaDonViVanChuyen.Text = value.Substring(value.IndexOf(" | ") + 3);
                   
                        txtSearhDonViVanChuyen.ReadOnly = true;
                    }
                }
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            txtSearhDonViVanChuyen.Text = "";
            txtSearchMaDonViVanChuyen.Text = "";
            txtSearhDonViVanChuyen.ReadOnly = false;
        }


        #endregion AutoComplete Đơn vị vận chuyển

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

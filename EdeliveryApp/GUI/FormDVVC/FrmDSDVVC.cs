using BLL.DAO;
using DAL.EntitiesFramwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormDVVC
{
    public partial class FrmDSDVVC : Form
    {
        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();
        public FrmDSDVVC()
        {
            InitializeComponent();
        }

        private void FrmDSDVVC_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        //Load danh sách đơn vị vận chuyển
        public void LoadDataGridView()
        {
            dataGridView1.DataSource = vehicleOwnerModelDAO.GetALL();
            RemoveColunm();
        }

        public void RemoveColunm()
        {

            dataGridView1.Columns["ProviderCode"].Visible = false;
            dataGridView1.Columns["CustomerCode"].Visible = false;
            dataGridView1.Columns["Actived"].Visible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string MaDonViVanChuyen = txtMaDVVC.Text.Trim();
            string TenDonViVanChuyen = txtTenDVVC.Text.Trim();

            dataGridView1.DataSource = vehicleOwnerModelDAO.GetALL(MaDonViVanChuyen, TenDonViVanChuyen);
            RemoveColunm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddDVVC frmAddDVVC = new FrmAddDVVC();
            frmAddDVVC.ShowDialog();
            LoadDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
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
                            if (item.Name == "FrmEditDVVC")
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
                            string MaDVVC = dataGridView1.CurrentRow.Cells["MaDonViVanChuyen"].Value.ToString();
                            string TenDVVC = dataGridView1.CurrentRow.Cells["TenDonViVanChuyen"].Value.ToString();

                            VehicleOwnerModel vehicleOwnerModel = vehicleOwnerModelDAO.GetALL(MaDVVC, TenDVVC).FirstOrDefault();

                            var Custoemr = new CustomerDAO().Get(vehicleOwnerModel.CustomerCode).CustomerName;

                            FrmEditDVVC frm = new FrmEditDVVC(MaDVVC, TenDVVC, Custoemr, vehicleOwnerModel.CustomerCode, vehicleOwnerModel.ProviderCode, "");

                            frm.ShowDialog();
                            LoadDataGridView();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi" + ex.Message.ToString());
            }
        }

        private void btnXoaBo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    const string message = "Bạn có muốn xóa thông tin đơn vị vận chuyển?";
                    const string caption = "Xác nhận xóa thông tin đơn vị vận chuyển";


                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Question);
                    // If the no button was pressed ...
                    if (result == DialogResult.OK)
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            VehicleOwnerModel DVVC = new VehicleOwnerModel();
                            DVVC.VehicleOwner = dataGridView1.CurrentRow.Cells["MaDonViVanChuyen"].Value.ToString();
                            DVVC.VehicleOwnerName = dataGridView1.CurrentRow.Cells["TenDonViVanChuyen"].Value.ToString();
                            DVVC.Actived = false;

                            VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();

                            MessageBox.Show(vehicleOwnerModelDAO.Update(DVVC));
                            LoadDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi" + ex.Message.ToString());
            }


        }
    }
}

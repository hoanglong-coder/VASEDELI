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
    public partial class FrmEditDVVC : Form
    {
        string _MaDonViVanChuyen;
        string _TenDonViVanChuyen;
        string _TenKH;
        string _MaKH;
        string _TenNB;
        string _MaNB;
        public FrmEditDVVC(string MaDonViVanChuyen, string TenDonViVanChuyen, string TenKH, string MaKH, string TenNB, string MaNB)
        {
            InitializeComponent();
            _MaDonViVanChuyen = MaDonViVanChuyen;
            _TenDonViVanChuyen = TenDonViVanChuyen;
            _TenKH = TenKH;
            _MaKH = MaKH;
            _TenNB = TenNB;
            _MaNB = MaNB;
        }

        private void FrmEditDVVC_Load(object sender, EventArgs e)
        {
            txtMaDVVC.Text = _MaDonViVanChuyen;
            txtTenDVVC.Text = _TenDonViVanChuyen;
            txtTenKH.Text = _TenKH;
            txtMaKH.Text = _MaKH;
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

                MessageBox.Show(vehicleOwnerModelDAO.Update(DVVC));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Model;
using BLL.DAO;
using DAL.EntitiesFramwork;
using System.Text.RegularExpressions;

namespace GUI.FormDangKyXe
{
    public partial class FrmEditXe : Form
    {
        VehicleModel _mVehicleModel;
        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();
        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();
        string TenDVVC;

        public FrmEditXe(string vehicleId, string tendvvc)
        {
            InitializeComponent();

            this._mVehicleModel = vehicleModelDAO.FindXe(Guid.Parse(vehicleId));

            this.TenDVVC = tendvvc;
        }

        private void FrmEditXe_Load(object sender, EventArgs e)
        {
            LoadCbDVVC();

            LoadThongTinXe();


        }
        /// <summary>
        /// Load thông tin xe khi chỉnh sửa
        /// </summary>
        private void LoadThongTinXe()
        {
            txtBienSoXe.Text = _mVehicleModel.VehicleNumber;

            txtDVVC.Text = TenDVVC;

            txtMaDVVC.Text = _mVehicleModel.VehicleOwner;

            txtBiXe.Text = Math.Round(_mVehicleModel.VehicleWeight.Value,0).ToString();

            txtTrongluongdangkiem.Text = Math.Round(_mVehicleModel.TrongLuongDangKiem.Value,0).ToString();

            txtTiLeVuot.Text = Math.Round(_mVehicleModel.TyLeVuot.Value,0).ToString();

            //CheckLoaiXe(_mVehicleModel.Type.Value);

            //CheckKieuXe(_mVehicleModel.isRoMooc, _mVehicleModel.isDauKeo);

            //CheckTrangThai(_mVehicleModel.isLock);

            //CheckKhoaChinhSua(_mVehicleModel.isLockEdit);
        }

        /// <summary>
        /// Load thông tin đơn vị vẩn chuyển
        /// </summary>
        private void LoadCbDVVC()
        {

            //cbDVVC.DataSource = vehicleOwnerModelDAO.GetALL();

            //cbDVVC.ValueMember = "MADONVIVANCHUYEN";

            //cbDVVC.DisplayMember = "TENDONVIVANCHUYEN";

        }

        ///// <summary>
        ///// Set radio button cho loại xe
        ///// </summary>
        ///// <param name="Type"></param>
        //private void CheckLoaiXe(int Type)
        //{
        //    if (Type == Constant.ConstantVehicle.NoiBo)
        //    {
        //        rdoXeNB.Checked = true;
        //        return;
        //    }
        //    if (Type == Constant.ConstantVehicle.KhachHang)
        //    {
        //        rdoXeKH.Checked = true;
        //        return;
        //    }
        //}
    
        //private void CheckKieuXe(int? isRomooc, bool? DauKeo)
        //{
        //    const int LaXeRomooc = 1;
        //    if (isRomooc.HasValue)
        //    {
        //        if (isRomooc.Value == LaXeRomooc)
        //        {
        //            rdoXeRomoc.Checked = true;
        //            return;
        //        }
        //    }
        //    if (DauKeo.HasValue)
        //    {
        //        if (DauKeo == true)
        //        {
        //            rdoXeDauKeo.Checked = true;
        //            return;
        //        }
        //    }

        //    rdoXeThuong.Checked = true;
        //    return;

        //}
   
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                //if (!CheckRaiobutonLoaiXe())
                //{

                //    MessageBox.Show("Bạn chưa chọn loại xe");

                //    return;
                //}
                //if (!CheckRaiobutonKieuXe())
                //{
                //    MessageBox.Show("Bạn chưa chọn kiểu xe");

                //    return;
                //}

                if (!CheckBienSoXe())
                {
                    return;
                }
                if (txtBiXe.Text == "")
                {
                    MessageBox.Show("Trọng lượng bì xe không được để trống");
                    return;
                }
                if (txtTrongluongdangkiem.Text == "")
                {
                    MessageBox.Show("Trọng lượng đăng kiểm không được để trống");
                    return;
                }
                if (txtTiLeVuot.Text == "")
                {
                    MessageBox.Show("Tỉ lệ vượt không được để trống");
                    return;
                }
                if (int.Parse(txtTiLeVuot.Text) > 100)
                {
                    MessageBox.Show("Tỉ lệ vượt không được lớn hơn 100%");
                    return;
                }



                const string message = "Bạn có muốn lưu thông tin xe?";
                const string caption = "Xác nhận lưu thông tin xe";


                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.OK)
                {

                    VehicleModel vehicleModel = new VehicleModel();
                    //vehicleModel.Type = TypeLoaiXe();
                    vehicleModel.VehicleNumber = txtBienSoXe.Text;
                    vehicleModel.VehicleOwner = txtMaDVVC.Text;
                    vehicleModel.VehicleWeight = decimal.Parse(txtBiXe.Text);
                    //vehicleModel.isRoMooc = rdoXeRomoc.Checked ? 1 : 0;
                    vehicleModel.TrongLuongDangKiem = decimal.Parse(txtTrongluongdangkiem.Text);
                    vehicleModel.TyLeVuot = decimal.Parse(txtTiLeVuot.Text);
                    vehicleModel.isLock = false;
                    vehicleModel.isLockEdit = false;
                    vehicleModel.LastEditTime = DateTime.Now;
                    //vehicleModel.isDauKeo = rdoXeDauKeo.Checked ? true : false;

                    var ketqua = vehicleModelDAO.UpdateXe(vehicleModel);

                    if (ketqua)
                    {
                        MessageBox.Show("Sửa thông tin xe thành công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sửa thông tin xe bị lỗi");
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi " + ex.Message);
            }
        }
        ////private bool CheckRaiobutonLoaiXe()
        ////{
        ////    if (rdoXeNB.Checked != false || rdoXeKH.Checked != false)
        ////    {
        ////        return true;
        ////    }
        ////    return false;
        ////}
        //private bool CheckRaiobutonKieuXe()
        //{
        //    if (rdoXeDauKeo.Checked != false || rdoXeRomoc.Checked != false || rdoXeThuong.Checked != false)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private int TypeLoaiXe()
        //{
        //    if (rdoXeNB.Checked)
        //    {
        //        return Constant.ConstantVehicle.NoiBo;
        //    }
        //    else if (rdoXeKH.Checked)
        //    {
        //        return Constant.ConstantVehicle.KhachHang;
        //    }
        //    else
        //    {
        //        return Constant.ConstantVehicle.DonViVanChuyen;
        //    }


        //}
        private bool CheckBienSoXe()
        {
            string pattern = @"^([0-9]*([A-Z]{1}|[A-Z]{2}))[-](([0-9]{3}[.][0-9]{2})|([0-9]{4}))\b";

            Match m = Regex.Match(txtBienSoXe.Text, pattern);

            if (txtBienSoXe.Text == String.Empty)
            {
                MessageBox.Show("Biển số xe không được để trống", "Cảnh báo",
                                     MessageBoxButtons.OKCancel,
                                     MessageBoxIcon.Warning);
                return false;
            }
            else if (!m.Success)
            {
                MessageBox.Show("Biển số xe phải đúng định dạng. Ví dụ 51C-999.99 hoặc 51C-9999 KHÔNG được viết liền nhau 51C99999 ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);

                return false;
            }
            return true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

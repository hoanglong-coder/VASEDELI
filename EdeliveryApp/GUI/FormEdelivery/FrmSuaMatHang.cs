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
using BLL.DAO;
using DAL.EntitiesFramwork;
using DAL.Model;
namespace GUI.FormEdelivery
{
    public partial class FrmSuaMatHang : Form
    {
        public static FrmSuaMatHang Instance;

        string BienSoXeEdit;
        string RomoocEdit;
        string CMNDEdit;
        string HotenEdit;
        string NoiCapEdit;
        DateTime NgayCapEdit = DateTime.Now;

        string _EdeliveryID;
        string _MaKH;
        string _TenKH;
        string _DVVC;
        string _NoiNhanHang;
        string _TenNoiGiaoNhan;
        int _IDCompanyCode;
        DateTime _NgayGiaoNhan;
        UserModel _userModel;
        public List<MDangKyXe> DKOld = new List<MDangKyXe>();
        public List<MDangKyXe> DKBANDAU = new List<MDangKyXe>();
        public List<MDangKyXe> DK = new List<MDangKyXe>();
        public List<MDangKyXe> DKMH = new List<MDangKyXe>(); //LƯU DANH SÁCH ĐƠN HÀNG CÓ THEO BIỂN SỐ
        public List<MDangKyXe> DKMHTEMP = new List<MDangKyXe>(); // lƯU TẠM

        public bool isInsertNew = false;
        public bool isInsertMH = false;

        Edelivery_GNHDAO edelivery_GNH_TestDAO = new Edelivery_GNHDAO();
        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();
        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();
        CompanyDAO CompanyDAO = new CompanyDAO();
        DKEdeliveryDAO dKEdeliveryDAO = new DKEdeliveryDAO();

        BaremDAO baremDAO = new BaremDAO();
        public FrmSuaMatHang(string MaKH, string TenKH, string DVVC, string NoiNhanHang, string TenNoiGiaoNhan, string EdeliveryID, DateTime NgayGiaoNhan, int IDCompanyCode, UserModel user)
        {
            InitializeComponent();

            _MaKH = MaKH;
            _DVVC = DVVC;
            _NoiNhanHang = NoiNhanHang;
            _EdeliveryID = EdeliveryID;
            _NgayGiaoNhan = NgayGiaoNhan.Date;
            _IDCompanyCode = IDCompanyCode;
            _TenNoiGiaoNhan = TenNoiGiaoNhan;
            _TenKH = TenKH;
            _userModel = user;
            Instance = this;
        }

        private void FrmSuaMatHang_Load(object sender, EventArgs e)
        {

            DKMHTEMP = dKEdeliveryDAO.GetDKEdeliverySua(_EdeliveryID, _userModel.UserId.ToString()).ToList();

            AutoComplete(txtTenNoiGiaoNhan);

            SetDataEdit();

        }



        /// <summary>
        /// Load Thông tin khách hàng/mã khách hàng
        /// </summary>
        private void LoadCbMaKH()
        {
            //cbMaKH.DataSource = edelivery_GNH_TestDAO.GetThongTinKhachHang("1000");

            //cbMaKH.ValueMember = "MAKHACHANG";

            //cbMaKH.DisplayMember = "TENKHACHHANG";

            //cbMaKH.SelectedIndex = -1;
        }

        /// <summary>
        /// Load thông tin đơn vị vẩn chuyển
        /// </summary>
        private void LoadCbDVVC()
        {

            //cbDVVC.DataSource = vehicleOwnerModelDAO.GetALL();

            //cbDVVC.ValueMember = "MADONVIVANCHUYEN";

            //cbDVVC.DisplayMember = "TENDONVIVANCHUYEN";

            //cbDVVC.SelectedIndex = -1;
        }

        /// <summary>
        /// Load combobox nơi nhận hàng
        /// </summary>
        private void LoadCbNhanHang()
        {
            //cbNoiNhanHang.DataSource = CompanyDAO.GetAll("1000");


            //cbNoiNhanHang.DisplayMember = "CompanyName";

            //cbNoiNhanHang.ValueMember = "ID";

            //cbNoiNhanHang.SelectedIndex = -1;
        }


        //Gán thông tin cần sửa vào form sửa kế hoạch
        private void SetDataEdit()
        {
            txtTenDonVi.Text = _TenNoiGiaoNhan.Split('_')[0];
            txtMaDonVi.Text = _NoiNhanHang;

            txTenKH.Text = _TenKH;
            txtMaKH.Text = _MaKH;

            txtTenNoiGiaoNhan.Text = _TenNoiGiaoNhan;
            txtMaNoiGiaoNhan.Text = _IDCompanyCode.ToString();

            txtDVVC.Text = "";
            txtMaDVVC.Text = "";

            datengaygiaonhan.Value = _NgayGiaoNhan;

            //Kiểm tra loại vận chuyển của đăng ký
            int KiemTraLoaiVanChuyen = dKEdeliveryDAO.LoaiVanChuyen(Guid.Parse(_EdeliveryID));

            if (KiemTraLoaiVanChuyen == Constant.LoaiVanChuyen.KhachHang)
            {
                rdoTypeCustomer.Checked = true;
            }
            else
            {
                rdoTypeLocal.Checked = true;
            }


            DKOld = dKEdeliveryDAO.GetDKEdelivery(_EdeliveryID).GroupBy(t => t.VehicelID).Select(t => t.FirstOrDefault()).ToList();



            dataGridView1.DataSource = DKOld;

            RemoveColumnDsXe();

        }
        /// <summary>
        /// Xóa các cột không cần hiển thị
        /// </summary>
        private void RemoveColumnDsXe()
        {
            dataGridView1.Columns["SONumber"].Visible = false;
            dataGridView1.Columns["SOItems"].Visible = false;
            dataGridView1.Columns["MaHangHoa"].Visible = false;
            dataGridView1.Columns["TenHangHoa"].Visible = false;
            dataGridView1.Columns["SOBC"].Visible = false;
            dataGridView1.Columns["SOCAY"].Visible = false;
            dataGridView1.Columns["TRONGLUONG"].Visible = false;
            dataGridView1.Columns["SOLUONGBEBO"].Visible = false;
            dataGridView1.Columns["SOCAYLE"].Visible = false;

            dataGridView1.Columns["IsDelete"].Visible = false;
            dataGridView1.Columns["VehicelID"].Visible = false;

            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["rowguid"].Visible = false;
            dataGridView1.Columns["SONumberPBM"].Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {
                        string BienSoxe = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                        Guid VehicleID = Guid.Parse(dataGridView1.CurrentRow.Cells[15].Value.ToString());

                        dataGridView2.Refresh();

                        dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == BienSoxe && t.VehicelID == VehicleID).ToList();

                        RemoveColumnDsSO();

                        //fix lỗi click vào header datagridview1
                        if (dataGridView1.CurrentRow.Cells[0].Value != null)
                        {
                            ShowTongSoluong();
                        }
                    }

                }

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    btnThemMatHang.Enabled = true;
                }
                else
                {
                    btnThemMatHang.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RemoveColumnDsSO()
        {
            dataGridView2.Columns["BienSo_SoHieu"].Visible = false;
            dataGridView2.Columns["TaiXe_ThuyenTruong"].Visible = false;
            dataGridView2.Columns["CMND_CCCD"].Visible = false;
            dataGridView2.Columns["NgayCapCMND_CCCD"].Visible = false;
            dataGridView2.Columns["NoiCapCMND_CCCD"].Visible = false;
            dataGridView2.Columns["Romooc"].Visible = false;
            //dataGridView2.Columns["SOCAYLE"].Visible = false;

            dataGridView2.Columns["VehicelID"].Visible = false;
            dataGridView2.Columns["IsDelete"].Visible = false;

            dataGridView2.Columns["Status"].Visible = false;
            dataGridView2.Columns["rowguid"].Visible = false;
            dataGridView2.Columns["SONumberPBM"].Visible = false;

            //chỉ đọc
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                //trọng lượng
                row.Cells[13].ReadOnly = true;
                row.Cells[6].ReadOnly = true;
                row.Cells[7].ReadOnly = true;
                row.Cells[8].ReadOnly = true;
                row.Cells[9].ReadOnly = true;
                row.Cells[11].ReadOnly = true;
                row.Cells[13].ReadOnly = true;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rdoTypeCustomer.Checked)
            {
                FrmThemXeSuaDangKy frmThemxe = new FrmThemXeSuaDangKy("C" + txtMaKH.Text);

                frmThemxe.ShowDialog();


                if (isInsertNew)
                {
                    DKOld = DKOld.Concat(DK).ToList();

                    dataGridView1.DataSource = DKOld;

                    RemoveColumnDsXe();

                    isInsertNew = false;
                }
            }
            else
            {
                FrmThemXeSuaDangKy frmThemxe = new FrmThemXeSuaDangKy(txtMaDVVC.Text);

                frmThemxe.ShowDialog();

                if (isInsertNew)
                {
                    DKOld = DKOld.Concat(DK).ToList();

                    dataGridView1.DataSource = DKOld;

                    RemoveColumnDsXe();

                    isInsertNew = false;
                }

            }
        }

        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            string BrandCode = txtMaDonVi.Text;

            string MaNoiGiaoNhan = new CompanyDAO().GetMaNoiGiaoNhan(int.Parse(txtMaNoiGiaoNhan.Text));


            FormEdelivery.FrmDSDonHangSuaDangKy frmDSDonHang = new FrmDSDonHangSuaDangKy(txtMaKH.Text, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString(), dataGridView1.CurrentRow.Cells["Romooc"].Value.ToString(), DKMHTEMP, dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString()), dataGridView1.CurrentRow.Cells[4].Value.ToString(), Guid.Parse(dataGridView1.CurrentRow.Cells[15].Value.ToString()), Guid.Parse(_EdeliveryID), BrandCode, MaNoiGiaoNhan, _userModel);

            frmDSDonHang.ShowDialog();

            if (isInsertMH)
            {
                int a = DKMHTEMP.Count();
                int b = DKMH.Count();

                DKMHTEMP = DKMHTEMP.Concat(DKMH).ToList();

                dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString() && dataGridView1.CurrentRow.Cells[15].Value.ToString() == t.VehicelID.ToString()).ToList();

                RemoveColumnDsSO();

                ShowTongSoluong();

                isInsertMH = false;
            }
        }

        /// <summary>
        /// tính tổng các số bc/tổng lượng...
        /// </summary>
        private bool ShowTongSoluong()
        {
            double tongtrongluong = 0;
            double tongsobc = 0;
            double tongsocay = 0;
            double tongsocayle = 0;


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {
                    tongtrongluong += double.Parse(row.Cells[13].Value.ToString());
                    tongsobc += double.Parse(row.Cells[10].Value.ToString());
                    tongsocay += double.Parse(row.Cells[11].Value.ToString());
                    tongsocayle += double.Parse(row.Cells[12].Value.ToString());
                }
                catch (Exception)
                {

                }


            }


            if (KiemTrTrongLuong((float)tongtrongluong))
            {
                lblTongTrongluong.Text = tongtrongluong.ToString();
                lblSoBC.Text = tongsobc.ToString();
                lblSoCayLe.Text = tongsocayle.ToString();
                lblSoCay.Text = tongsocay.ToString();

                return true;
            }
            else
            {
                lblTongTrongluong.Text = tongtrongluong.ToString();
                lblSoBC.Text = tongsobc.ToString();
                lblSoCayLe.Text = tongsocayle.ToString();
                lblSoCay.Text = tongsocay.ToString();


                //MessageBox.Show("Trọng lượng hàng vượt định mức đăng kiểm", "Cảnh báo",
                //                         MessageBoxButtons.OKCancel,
                //                         MessageBoxIcon.Warning);

                //số bó cuộn bằng 1
                //dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

                ConvertSOBC();

                return true;
            }
        }

        private bool KiemTrTrongLuong(float trongluong)
        {

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                trongluong += float.Parse(row.Cells[13].Value.ToString());
            }

            if (KiemTraTrongLuongDangKiem(dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString(), trongluong))
            {
                return true;
            }
            else
            {

                return false;

            }
        }
        /// <summary>
        /// Kiểm tra trộng lượng đăng kiểm
        /// </summary>
        /// <param name="BienSo">Biển số xe</param>
        /// <param name="trongluong">Trọng lượng kiểm tra</param>
        /// <returns></returns>
        private bool KiemTraTrongLuongDangKiem(string BienSo, float trongluong)
        {
            return vehicleModelDAO.KiemTraTrongLuongDangKiem(BienSo, trongluong);
        }

        /// <summary>
        /// nhập số bó cuộn quy đổi ra trộng lượng, số cây
        /// </summary>
        public void ConvertSOBC()
        {
            //string MaMH = dataGridView2.CurrentRow.Cells[8].Value.ToString();

            //Barem barem = baremDAO.FindBarem(MaMH);


            //int socayle = 0;



            ////Nếu số cây # null thì số cây lẽ bằng socayle
            //if (dataGridView2.CurrentRow.Cells[11].Value != null)
            //{
            //    socayle = int.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString());
            //}





            ////Kiêm tra số bó cuộn
            //if (!KiemTraSOBCDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //   dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //   decimal.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString()),
            //   edelivery_GNH_TestDAO.GetSOBC(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //   dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //{
            //    MessageBox.Show("Số lượng bó/cuộn lớn hơn đơn hàng", "Lỗi",
            //                             MessageBoxButtons.OKCancel,
            //                             MessageBoxIcon.Error);

            //    //số bó cuộn bằng 0
            //    dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

            //    dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

            //    dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
            //}
            //else
            //{

            //    if (!KiemTraSOCayLeDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    decimal.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString()), edelivery_GNH_TestDAO.GetSOCAYLE(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //    {
            //        MessageBox.Show("Số lượng cây lẻ lớn hơn đơn hàng", "Lỗi",
            //                               MessageBoxButtons.OKCancel,
            //                               MessageBoxIcon.Error);

            //        //số bó cuộn bằng 0

            //        dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();
            //    }
            //    else
            //    {
            //        // số cây
            //        dataGridView2.CurrentRow.Cells[11].Value = baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

            //        //tổng trọng lượng
            //        dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();
            //    }

            //}

            ////Kiểm tra cây lẻ
            //if (!KiemTraSOCayLeDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    decimal.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString()), edelivery_GNH_TestDAO.GetSOCAYLE(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //{
            //    MessageBox.Show("Số lượng cây lẻ lớn hơn đơn hàng", "Lỗi",
            //                           MessageBoxButtons.OKCancel,
            //                           MessageBoxIcon.Error);

            //    //số bó cuộn bằng 0

            //    dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();
            //}
            //else
            //{
            //    ////Kiêm tra số bó cuộn
            //    //if (!KiemTraSOBCDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    //   dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    //   decimal.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString()),
            //    //   edelivery_GNH_TestDAO.GetSOBC(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    //   dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //    //{
            //    //    MessageBox.Show("Số lượng bó/cuộn lớn hơn đơn hàng", "Lỗi",
            //    //                             MessageBoxButtons.OKCancel,
            //    //                             MessageBoxIcon.Error);

            //    //    //số bó cuộn bằng 0
            //    //    dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

            //    //    dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

            //    //    dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
            //    //}
            //    //else
            //    //{
            //    //    dataGridView2.CurrentRow.Cells[11].Value = BaremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

            //    //    dataGridView2.CurrentRow.Cells[13].Value = BaremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();


            //    //}
            //}


            string MaMH = dataGridView2.CurrentRow.Cells[8].Value.ToString();

            Barem barem = baremDAO.FindBaremWithAPI(txtMaDonVi.Text,MaMH);

            decimal trongluongdadangky = dKEdeliveryDAO.SoTRONGLUONGDaDKSuaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString(), Guid.Parse(dataGridView2.CurrentRow.Cells[15].Value.ToString()), Guid.Parse(_EdeliveryID));

            if (barem == null)
            {
                if (decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) == 0)
                {
                    MessageBox.Show("Mặt hàng này không có barem hãy nhập trọng lượng", "Lỗi",
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Error);
                    dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

                    dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

                    dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();

                    dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();

                    dataGridView2.CurrentRow.Cells[14].Value = 0.ToString();

                    dataGridView2.CurrentRow.Cells[13].ReadOnly = false;
                    return;
                }
                else
                {
                    dataGridView2.CurrentRow.Cells[13].ReadOnly = false;

                    if (!KiemTraTrongLuongDaDangKy(TrongLuongDaDangKy(DKMHTEMP, dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()), edelivery_GNH_TestDAO.GetTrongLuong(dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()) + trongluongdadangky))
                    {
                        MessageBox.Show("Trọng lượng đăng ký vượt quá trọng lượng SO", "Lỗi",
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Error);
                        //trọng lượng = 0
                        dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
                    }

                    return;
                }
            }
            int socayle = 0;



            //Nếu số cây # null thì số cây lẽ bằng socayle
            if (dataGridView2.CurrentRow.Cells[11].Value != null)
            {
                socayle = int.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString());
            }


            ////Kiêm tra số bó cuộn
            //if (!KiemTraSOBCDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //   dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //   decimal.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString()),
            //   edelivery_GNH_TestDAO.GetSOBC(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //   dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //{
            //    MessageBox.Show("Số lượng bó/cuộn lớn hơn đơn hàng", "Lỗi",
            //                             MessageBoxButtons.OKCancel,
            //                             MessageBoxIcon.Error);

            //    //số bó cuộn bằng 0
            //    dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

            //    dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

            //    dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
            //}
            //else
            //{

            //    if (!KiemTraSOCayLeDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    decimal.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString()), edelivery_GNH_TestDAO.GetSOCAYLE(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //    {
            //        MessageBox.Show("Số lượng cây lẻ lớn hơn đơn hàng", "Lỗi",
            //                               MessageBoxButtons.OKCancel,
            //                               MessageBoxIcon.Error);

            //        //số bó cuộn bằng 0

            //        dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();
            //    }
            //    else
            //    {

            //        //Tỏng số cây
            //        dataGridView2.CurrentRow.Cells[11].Value = (baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())) + socayle).ToString();

            //        //Tổng trộng lượng
            //        dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString())).ToString();
            //    }

            //}

            ////Kiểm tra cây lẻ
            //if (!KiemTraSOCayLeDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    decimal.Parse(dataGridView2.CurrentRow.Cells[12].Value.ToString()), edelivery_GNH_TestDAO.GetSOCAYLE(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //{
            //    MessageBox.Show("Số lượng cây lẻ lớn hơn đơn hàng", "Lỗi",
            //                           MessageBoxButtons.OKCancel,
            //                           MessageBoxIcon.Error);

            //    //số bó cuộn bằng 0

            //    dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();
            //}
            //else
            //{
            //    ////Kiêm tra số bó cuộn
            //    //if (!KiemTraSOBCDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    //   dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //    //   decimal.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString()),
            //    //   edelivery_GNH_TestDAO.GetSOBC(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //    //   dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //    //{
            //    //    MessageBox.Show("Số lượng bó/cuộn lớn hơn đơn hàng", "Lỗi",
            //    //                             MessageBoxButtons.OKCancel,
            //    //                             MessageBoxIcon.Error);

            //    //    //số bó cuộn bằng 0
            //    //    dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

            //    //    dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

            //    //    dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
            //    //}
            //    //else
            //    //{
            //    //    dataGridView2.CurrentRow.Cells[11].Value = BaremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

            //    //    dataGridView2.CurrentRow.Cells[13].Value = BaremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();


            //    //}
            //}

            ////Kiểm tra số lượng bẻ bó
            //if (!KiemTraSoLuongBeBoDaDangKy(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //  dataGridView2.CurrentRow.Cells[8].Value.ToString(),
            //  decimal.Parse(dataGridView2.CurrentRow.Cells[14].Value.ToString()),
            //  edelivery_GNH_TestDAO.GetSOLUONGBEBO(dataGridView2.CurrentRow.Cells[6].Value.ToString(),
            //  dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            //{
            //    MessageBox.Show("Số lượng bẻ bó lớn hơn đơn hàng", "Lỗi",
            //                             MessageBoxButtons.OKCancel,
            //                             MessageBoxIcon.Error);

            //    //số bó cuộn bằng 0
            //    dataGridView2.CurrentRow.Cells[14].Value = 0.ToString();
            //}

            if (decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) != TrongLuong)
            {

                decimal tongcay = Math.Round(decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) / ((decimal)barem.BAREM1 / 1000), 0);
                //SOBC
                dataGridView2.CurrentRow.Cells[10].Value = Math.Floor(tongcay / (decimal)barem.CAY_BO);

                //SOCAYLE
                dataGridView2.CurrentRow.Cells[12].Value = tongcay - (barem.CAY_BO * Math.Floor(tongcay / (decimal)barem.CAY_BO));

                //SOCAY
                dataGridView2.CurrentRow.Cells[11].Value = tongcay - (decimal)dataGridView2.CurrentRow.Cells[12].Value;
            }
            else
            {
                // số cây
                dataGridView2.CurrentRow.Cells[11].Value = baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

                //trộng lượng
                dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();

            }

            ////Tỏng số cây
            //dataGridView2.CurrentRow.Cells[11].Value = baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

            ////Tổng trộng lượng
            //dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();

            

            if (!KiemTraTrongLuongDaDangKy(TrongLuongDaDangKy(DKMHTEMP, dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()), edelivery_GNH_TestDAO.GetTrongLuong(dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()) + trongluongdadangky))
            {
                MessageBox.Show("Trọng lượng đăng ký vượt quá trọng lượng SO", "Lỗi",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Error);
                //số bó cuộn bằng 0
                dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

                dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

                dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();
            }



        }

        /// <summary>
        /// Tính trọng lượng đã đăng ký
        /// </summary>
        /// <param name="SOnumber">Mã đơn hàng</param>
        /// <param name="MaHH"> mã hàng hóa</param>
        /// <param name="editSOBC">User gõ số bó cuộn vào </param>
        /// <returns></returns>
        private decimal TrongLuongDaDangKy(List<MDangKyXe> mDangKyXes, string SONumber, string MAHANGHOA)
        {
            //decimal sobc = dKEdeliveryDAO.SoBCDaDK(DKMHTEMP, SOnumber, MaHH, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString());


            decimal TrongLuongDaDangKy = 0;
            foreach (var item in mDangKyXes)
            {
                if (item.SONumber == SONumber && item.MaHangHoa == MAHANGHOA)
                {
                    TrongLuongDaDangKy += item.TRONGLUONG.Value;
                }
            }
            return TrongLuongDaDangKy;

        }

        private bool KiemTraTrongLuongDaDangKy(decimal trongluongdangky, decimal trongluongbpm)
        {
            if (trongluongdangky <= trongluongbpm)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Kiểm tra số bc nhập vào đã còn nhỏ hơn tổng số bó cuộn đã đăng ký hay không
        /// </summary>
        /// <param name="SOnumber">Mã đơn hàng</param>
        /// <param name="MaHH"> mã hàng hóa</param>
        /// <param name="editSOBC">User gõ số bó cuộn vào </param>
        /// <returns></returns>
        private bool KiemTraSOBCDaDangKy(string SOnumber, string MaHH, decimal editSOBC, decimal SOBCPBM)
        {
            //decimal sobc = dKEdeliveryDAO.SoBCDaDKSuaDangKy(DKMHTEMP, SOnumber, MaHH, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString(), Guid.Parse(_EdeliveryID));



            if (editSOBC <= SOBCPBM)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// Kiểm tra số cây lẻ nhập vào đã còn nhỏ hơn tổng số bó cuộn đã đăng ký hay không
        /// </summary>
        /// <param name="SOnumber">Mã đơn hàng</param>
        /// <param name="MaHH"> mã hàng hóa</param>
        /// <param name="editSOBC">User gõ số bó cuộn vào </param>
        /// <returns></returns>
        private bool KiemTraSOCayLeDaDangKy(string SOnumber, string MaHH, decimal editSOCAYLE, decimal SOBCPBM)
        {
            decimal socayle = dKEdeliveryDAO.SoCayLeDaDKSuaDangKy(DKMHTEMP, SOnumber, MaHH, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString(), Guid.Parse(_EdeliveryID));



            if (editSOCAYLE <= SOBCPBM)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                btnXoaMatHang.Enabled = true;
            }
            else
            {
                btnXoaMatHang.Enabled = false;
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ConvertSOBC();

            ShowTongSoluong();
        }

        private void btnXoaMatHang_Click(object sender, EventArgs e)
        {
            const string message = "Bạn có muốn xóa măt hàng: ";
            const string caption = "Xác nhận Xóa";


            var result = MessageBox.Show(message, caption,
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                    {
                        edelivery_GNH_TestDAO.DeleteThinTinSo(ref DKMHTEMP, row.Cells["BienSo_SoHieu"].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[8].Value.ToString(), Guid.Parse(dataGridView1.CurrentRow.Cells[15].Value.ToString()));




                    }

                    MessageBox.Show("Xóa thành công");

                    dataGridView2.Refresh();

                    dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString() && dataGridView1.CurrentRow.Cells[15].Value.ToString() == t.VehicelID.ToString()).ToList();
                }
                catch (Exception)
                {

                    MessageBox.Show("Xóa thất bại");

                }
            }
        }

        private void btnXoaXe_Click(object sender, EventArgs e)
        {
            const string message = "Bạn có muốn xóa xe: ";
            const string caption = "Xác nhận Xóa";


            var result = MessageBox.Show(message, caption,
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        edelivery_GNH_TestDAO.DeleteXe(ref DKOld, ref DKMHTEMP, row.Cells["BienSo_SoHieu"].Value.ToString(), row.Cells["VehicelID"].Value.ToString());

                    }

                    MessageBox.Show("Xóa thành công");

                    dataGridView1.DataSource = null;

                    dataGridView1.DataSource = DKOld;

                    dataGridView1.Columns["BienSo_SoHieu"].HeaderText = "Biển số xe";
                    dataGridView1.Columns["Romooc"].HeaderText = "Rơ mooc";
                    dataGridView1.Columns["TaiXe_ThuyenTruong"].HeaderText = "Họ tên tài xế";
                    dataGridView1.Columns["CMND_CCCD"].HeaderText = "CMND/GPLX";
                    dataGridView1.Columns["NgayCapCMND_CCCD"].HeaderText = "Ngày cấp";
                    dataGridView1.Columns["NoiCapCMND_CCCD"].HeaderText = "Nơi cấp";


                    RemoveColumnDsXe();

                    if (dataGridView1.CurrentRow != null)
                    {
                        dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString()).ToList();


                        RemoveColumnDsSO();
                    }
                    else
                    {
                        dataGridView2.Refresh();

                        dataGridView2.DataSource = DKMHTEMP;
                    }


                }
                catch (Exception)
                {

                    MessageBox.Show("Xóa thất bại");


                }



            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Bạn chưa chọn Mã/Tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (string.IsNullOrEmpty(txtMaNoiGiaoNhan.Text))
            {
                MessageBox.Show("Bạn chưa chọn nơi giao nhận hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rdoTypeLocal.Checked && !string.IsNullOrEmpty(txtMaDVVC.Text))
            {
                MessageBox.Show("Bạn chưa chọn đơn vị vẩn chuyển!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (dKEdeliveryDAO.TrangThaiDkEdelivery(Guid.Parse(_EdeliveryID)) != Constant.ConstantStatusDK_EDelivery.DangChoDuyetChoPhepSua)
            {
                MessageBox.Show("Đăng ký này đã được phê duyệt không thể lưu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Xác nhận lưu
                MessageBoxSave();

            }
        }

        /// <summary>
        /// Hiển thị xác nhận lưu thông tin đăng ký Edelivery
        /// </summary>
        private void MessageBoxSave()
        {



            if (DKMHTEMP.Count == 0)
            {
                //Kiểm tra trạng thái một lần nữa | nếu trạng thái là 1(đã gửi phê duyệt thì chặn)|| nếu là 2 thì version 2 tạo cái ID mới ròi call api thông báo

                var result = MessageBox.Show("Khai báo giao nhận không có hàng hóa. Bạn có muốn hủy đơn hàng!", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    DK_Edelivery dK_Edelivery = new DK_Edelivery();

                    dK_Edelivery.MaKhachHang = txtMaKH.Text;

                    dK_Edelivery.TenKhachHang = txTenKH.Text;

                    dK_Edelivery.NgayGiaoNhan = datengaygiaonhan.Value;

                    if (rdoTypeCustomer.Checked)
                    {
                        dK_Edelivery.MaDonViVanChuyen = "C" + dK_Edelivery.MaKhachHang;

                        dK_Edelivery.DonViVanChuyen = dK_Edelivery.TenKhachHang;

                    }
                    else
                    {
                        dK_Edelivery.MaDonViVanChuyen = txtMaDVVC.Text;

                        dK_Edelivery.DonViVanChuyen = txtDVVC.Text;
                    }

                    dK_Edelivery.MaNoiGiaoNhan = txtMaDonVi.Text;

                    dK_Edelivery.TenNoiGiaoNhan = txtTenNoiGiaoNhan.Text;

                    string checkInsert = dKEdeliveryDAO.HuyDKEdelivery(Guid.Parse(_EdeliveryID));

                    ///Hàm khác để hủy  VÀ GỬI INSTANCE ĐỂ HỦY

                    if (!string.IsNullOrEmpty(checkInsert))
                    {

                        edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());


                        MessageBox.Show("Hủy thành công", "Kết quả",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Information);


                    }
                    else
                    {
                        MessageBox.Show("Hủy thất bại", "Kết quả",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                const string message = "Bạn có muốn lưu thông tin giao/nhận đơn hàng (SO)?";
                const string caption = "Xác nhận lưu thông tin Edelivery";


                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.OK)
                {

                    DK_Edelivery dK_Edelivery = new DK_Edelivery();

                    dK_Edelivery.MaKhachHang = txtMaKH.Text;

                    dK_Edelivery.TenKhachHang = txTenKH.Text;

                    dK_Edelivery.NgayGiaoNhan = datengaygiaonhan.Value;

                    if (rdoTypeCustomer.Checked)
                    {
                        dK_Edelivery.MaDonViVanChuyen = "C" + dK_Edelivery.MaKhachHang;

                        dK_Edelivery.DonViVanChuyen = dK_Edelivery.TenKhachHang;

                    }
                    else
                    {
                        dK_Edelivery.MaDonViVanChuyen = txtMaDVVC.Text;

                        dK_Edelivery.DonViVanChuyen = txtDVVC.Text;
                    }

                    dK_Edelivery.MaNoiGiaoNhan = txtMaNoiGiaoNhan.Text;

                    dK_Edelivery.TenNoiGiaoNhan = txtTenNoiGiaoNhan.Text;

                    dK_Edelivery.NhaMaySanXuat = new CompanyDAO().GetMaNoiGiaoNhan(int.Parse(txtMaNoiGiaoNhan.Text));

                    string checkupdate = dKEdeliveryDAO.UpdateDKEdelivery(dK_Edelivery, DKMHTEMP, Guid.Parse(_EdeliveryID), _userModel);

                    if (checkupdate != string.Empty)
                    {
                        edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());

                        //CallAPIBPM callAPIBPM = new CallAPIBPM();

                        //callAPIBPM.CallComplate("U", checkupdate,"");

                        MessageBox.Show("Lưu thành công", "Kết quả",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại", "Kết quả",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Error);
                    }
                }

            }


        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    string bienso = dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value != null ? dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString() : "";
                    string romooc = dataGridView1.CurrentRow.Cells["Romooc"].Value != null ? dataGridView1.CurrentRow.Cells["Romooc"].Value.ToString() : "";
                    string cmnd_gplx = dataGridView1.CurrentRow.Cells["CMND_CCCD"].Value != null ? dataGridView1.CurrentRow.Cells["CMND_CCCD"].Value.ToString() : "";
                    string tentx = dataGridView1.CurrentRow.Cells["TaiXe_ThuyenTruong"].Value != null ? dataGridView1.CurrentRow.Cells["TaiXe_ThuyenTruong"].Value.ToString() : "";
                    string noicap = dataGridView1.CurrentRow.Cells["NoiCapCMND_CCCD"].Value != null ? dataGridView1.CurrentRow.Cells["NoiCapCMND_CCCD"].Value.ToString() : "";


                    string pattern = @"^([0-9]*([A-Z]{1}|[A-Z]{2}))[-](([0-9]{3}[.][0-9]{2})|([0-9]{4}))\b";

                    string patternromoc = @"^([0-9]*[R]{1})[-](([0-9]{3}[.][0-9]{2})|([0-9]{4}))\b";

                    string patter_cmnd_gplx = @"\b^([0-9]{12})\b|\b([0-9]{9})\b";

                    string patter_ten = @"^([a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+)((\s{1}[a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+){1,})$";

                    Match m = Regex.Match(bienso, pattern);

                    Match romoc = Regex.Match(romooc, patternromoc);

                    Match cmnd = Regex.Match(cmnd_gplx, patter_cmnd_gplx);

                    Match ten = Regex.Match(tentx.Trim().ToLower(), patter_ten);


                    if (!m.Success || string.IsNullOrEmpty(bienso))
                    {
                        MessageBox.Show("Biển số xe phải đúng định dạng và không được để trống. Ví dụ 51C-999.99 hoặc 51C-9999 KHÔNG được viết liền nhau 51C99999 ", "Cảnh báo",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Warning);
                        dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value = BienSoXeEdit;

                        return;
                    }

                    if (rdoTypeCustomer.Checked)
                    {
                        if (!vehicleModelDAO.KiemTraXeCoTrongData(bienso, "C" + txtMaKH.Text))
                        {
                            MessageBox.Show("Không có biển số xe này ", "Cảnh báo",
                                                  MessageBoxButtons.OKCancel,
                                                  MessageBoxIcon.Warning);
                            dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value = BienSoXeEdit;

                            return;
                        }
                    }
                    //Lấy xe có mã S
                    else
                    {

                        if (!vehicleModelDAO.KiemTraXeCoTrongData(bienso, txtMaDVVC.Text))
                        {
                            MessageBox.Show("Không có biển số xe này ", "Cảnh báo",
                                                  MessageBoxButtons.OKCancel,
                                                  MessageBoxIcon.Warning);
                            dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value = BienSoXeEdit;

                            return;
                        }
                    }

                    if (!romoc.Success && romooc != string.Empty)
                    {
                        MessageBox.Show("Biển số rơ móc phải đúng định dạng. Ví dụ 51R-999.99 hoặc 51R-9999 KHÔNG được viết liền nhau 51R99999 ", "Cảnh báo",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Warning);
                        dataGridView1.CurrentRow.Cells["Romooc"].Value = RomoocEdit;

                        return;
                    }
                    if (!cmnd.Success)
                    {

                        MessageBox.Show("Số CMND/GPLX phải đúng định dạng 9 chữ số hoặc 12 chữ số ", "Cảnh báo",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Warning);
                        dataGridView1.CurrentRow.Cells["CMND_CCCD"].Value = CMNDEdit;

                        return;
                    }
                    if (tentx == string.Empty || tentx == "" || !ten.Success)
                    {

                        MessageBox.Show("Họ tên không được để trống, chứa ký tự đặc biệt, chữ số ", "Cảnh báo",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Warning);
                        dataGridView1.CurrentRow.Cells["TaiXe_ThuyenTruong"].Value = HotenEdit;
                        return;
                    }

                    string VehicleID = dataGridView1.CurrentRow.Cells["VehicelID"].Value.ToString();


                    foreach (var item in DKMHTEMP)
                    {

                        if (item.VehicelID == Guid.Parse(VehicleID))
                        {
                            //đổi biển số xe, rơ móc, họ tên, cmnd, ngày cấp, nơi cấp

                            item.BienSo_SoHieu = bienso;
                            item.TaiXe_ThuyenTruong = tentx;
                            item.Romooc = romooc;
                            item.CMND_CCCD = cmnd_gplx;
                            item.NgayCapCMND_CCCD = NgayCapEdit;
                            item.NoiCapCMND_CCCD = noicap;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Biển số xe phải đúng định dạng và không được để trống. Ví dụ 51C-999.99 hoặc 51C-9999 KHÔNG được viết liền nhau 51C99999 ", "Cảnh báo",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Warning);
                    dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value = BienSoXeEdit;

                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    this.BienSoXeEdit = dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString();

                    RomoocEdit = dataGridView1.CurrentRow.Cells["Romooc"].Value != null ? dataGridView1.CurrentRow.Cells["Romooc"].Value.ToString() : "";

                    CMNDEdit = dataGridView1.CurrentRow.Cells["CMND_CCCD"].Value.ToString();

                    HotenEdit = dataGridView1.CurrentRow.Cells["TaiXe_ThuyenTruong"].Value.ToString();

                    NoiCapEdit = dataGridView1.CurrentRow.Cells["NoiCapCMND_CCCD"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void AutoComplete(TextBox text)
        {
            text.AutoCompleteMode = AutoCompleteMode.Suggest;
            text.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }



        #region Nơi giao nhận hàng
        private void ThreadNoiGiaoNhanAutocomplete(object obj)
        {

            string[] arr = CompanyDAO.GetAllDonViUsingDapper(txtMaDonVi.Text, obj);

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
            if (this.txtTenNoiGiaoNhan.InvokeRequired)
            {
                SetAutoNoiGiaoNhanCustomerCallback d = new SetAutoNoiGiaoNhanCustomerCallback(SetAutoCompleteNoiGiaoNhan);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtTenNoiGiaoNhan.AutoCompleteCustomSource = collection;
                if (txtTenNoiGiaoNhan.Text != null && txtTenNoiGiaoNhan.Text.Length > 1 &&
                    txtTenNoiGiaoNhan.Text.Substring(txtTenNoiGiaoNhan.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void txtTenNoiGiaoNhan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaDonVi.Text != string.Empty)
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

        private void txtTenNoiGiaoNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtTenNoiGiaoNhan.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtTenNoiGiaoNhan.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtMaNoiGiaoNhan.Text = value.Substring(value.IndexOf(" | ") + 10);

                        txtTenNoiGiaoNhan.ReadOnly = true;
                    }
                }
            }
        }

        private void btnRemoveNoiGiaoNhan_Click(object sender, EventArgs e)
        {
            txtTenNoiGiaoNhan.Text = "";
            txtMaNoiGiaoNhan.Text = "";
            txtTenNoiGiaoNhan.ReadOnly = false;
        }

        #endregion Nơi giao nhận hàng

        #region AutoComplete đơn vị vận chuyển
        private void AutoCompleteDVVC()
        {
            this.txtDVVC.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtDVVC.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
            if (this.txtDVVC.InvokeRequired)
            {
                SetAutoCompleteDVVCCallback d = new SetAutoCompleteDVVCCallback(SetAutoDVVC);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtDVVC.AutoCompleteCustomSource = collection;
                if (txtDVVC.Text != null && txtDVVC.Text.Length > 1 &&
                    txtDVVC.Text.Substring(txtDVVC.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void txtDVVC_TextChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDVVC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtDVVC.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtDVVC.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtMaDVVC.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txtDVVC.ReadOnly = true;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtDVVC.Text = "";
            txtMaDVVC.Text = "";
            txtDVVC.ReadOnly = false;
        }




        #endregion AutoComplete đơn vị vận chuyển

        private void rdoTypeCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTypeCustomer.Checked)
            {
                txtDVVC.Enabled = false;
            }
        }

        private void rdoTypeLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTypeLocal.Checked)
            {
                txtDVVC.Enabled = true;
            }
        }

        private void FrmSuaMatHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());
        }
        decimal TrongLuong;
        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            TrongLuong = (decimal)dataGridView2.CurrentRow.Cells[13].Value;
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var a = dataGridView1.CurrentCell.DataGridView.Name;
            if (e.Control != null && e.Control.Name.ToString() == "")
            {
                e.Control.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }
        //Handling presses for minus dot and numbers
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.')
            {
                if (((DataGridViewTextBoxEditingControl)sender).Text.Length == 0)
                    e.Handled = true;
                if (((DataGridViewTextBoxEditingControl)sender).Text.Contains('.'))
                    e.Handled = true;
            }
            if (e.KeyChar == '-')
            {
                if (((DataGridViewTextBoxEditingControl)sender).Text.Length != 0)
                    e.Handled = true;
                if (((DataGridViewTextBoxEditingControl)sender).Text.Contains('-'))
                    e.Handled = true;
            }
        }
    }

}

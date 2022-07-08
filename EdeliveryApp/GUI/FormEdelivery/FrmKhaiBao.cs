using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DAO;
using DAL.Model;
using DAL.EntitiesFramwork;
using System.Text.RegularExpressions;
using System.Threading;

namespace GUI.FormEdelivery
{
    public partial class FrmKhaiBao : Form
    {
        public static FrmKhaiBao Instance;
        UserModel _userModel;
        string BienSoXeEdit;
        string RomoocEdit;
        string CMNDEdit;
        string HotenEdit;
        string NoiCapEdit;
        DateTime NgayCapEdit = DateTime.Now;


        public bool isInsertNew = false;
        public bool isInsertMH = false;

        public List<MDangKyXe> DK = new List<MDangKyXe>();// LƯU DANH SÁCH XE CHO SO
        public List<MDangKyXe> DKOld = new List<MDangKyXe>();

        public List<MDangKyXe> DKMH = new List<MDangKyXe>(); //LƯU DANH SÁCH ĐƠN HÀNG CÓ THEO BIỂN SỐ
        public List<MDangKyXe> DKMHTEMP = new List<MDangKyXe>(); // lƯU TẠM




        Edelivery_GNHDAO edelivery_GNH_TestDAO = new Edelivery_GNHDAO();
        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO();
        CompanyDAO CompanyDAO = new CompanyDAO();
        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();
        BaremDAO baremDAO = new BaremDAO();
        DKEdeliveryDAO dKEdeliveryDAO = new DKEdeliveryDAO();

        public FrmKhaiBao(UserModel userModel)
        {
            InitializeComponent();

            Instance = this;
            _userModel = userModel;
        }



        private void FrmKhaiBao_Load(object sender, EventArgs e)
        {

            //AutoCompleteVehicleEntry();

            CompanyDAO companyDAO = new CompanyDAO();

            string[] arr = companyDAO.GetAllUsingDapper(_userModel.CompanyCode);

            String selItem = arr[0];

            if (selItem.Contains(" | "))
            {
                string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                if ((value.IndexOf(" | ") + 3) < value.Length)
                {
                    //Chọn lại kho nhập
                    textBox1.Text = value.Substring(0, value.IndexOf(" | "));

                    //Chọn lại mã kho nhập
                    textBox2.Text = value.Substring(value.IndexOf(" | ") + 3);

                    textBox1.ReadOnly = true;
                }
            }



            AutoCompleteCustomer();

            AutoComplete();

            AutoCompleteDVVC();

            LoadCbDVVC();

            //Ẩn nút thêm đơn hàng khi chưa chọn xe
            DisableBtnThemDonHang();

            rdoTypeCustomer.Checked = true;
        }
        public void LoadDonVi()
        {
            //    cbDonViCompany.DataSource = CompanyDAO.GetAllDonVi();
            //    cbDonViCompany.DisplayMember = "CompanyName";
            //    cbDonViCompany.ValueMember = "CompanyCode";
            //    cbDonViCompany.SelectedIndex = -1;
        }

        /// <summary>
        /// Load Thông tin khách hàng/mã khách hàng
        /// </summary>
        private void LoadCbMaKH(string branchCode)
        {
            //cbMaKH.DataSource = edelivery_GNH_TestDAO.GetThongTinKhachHang(branchCode);

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
        private void LoadCbNhanHang(string CompanyCode)
        {
            //cbNoiNhanHang.DataSource = CompanyDAO.GetAll(CompanyCode);


            //cbNoiNhanHang.DisplayMember = "CompanyName";

            //cbNoiNhanHang.ValueMember = "ID";

            //cbNoiNhanHang.SelectedIndex = -1;
        }

        private void DisableBtnThemDonHang()
        {
            // ẩn nút thêm hóa đơn nếu chưa chọn xe
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnThemMatHang.Enabled = true;


            }
            else
            {
                btnThemMatHang.Enabled = false;
            }

            // ẩn nút xóa hóa đơn nếu chưa chọn hóa đơn
            if (dataGridView2.SelectedRows.Count > 0)
            {
                btnXoaMatHang.Enabled = true;


            }
            else
            {
                btnXoaMatHang.Enabled = false;
            }
        }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtMaKH.Text != string.Empty)
            //    {
            //        //var t = (MEdelivery)cbMaKH.SelectedItem;
            //        cbDVVC.SelectedValue = vehicleOwnerModelDAO.GetMaDVVC(txtMaKH.Text);


            //    }
            //}
            //catch (Exception)
            //{


            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Nếu loại đơn vị là khách hàng => lấy xe có mã C + mã khách hàng
            if (rdoTypeCustomer.Checked)
            {
                FormEdelivery.FrmThemXe frmThemxe = new FormEdelivery.FrmThemXe("C" + txtMaKH.Text);

                frmThemxe.ShowDialog();


                if (isInsertNew)
                {
                    DKOld = DKOld.Concat(DK).ToList();

                    dataGridView1.DataSource = DKOld;

                    RemoveColumnDsXe();

                    isInsertNew = false;
                }

            }
            //Lấy xe có mã S
            else
            {
                FormEdelivery.FrmThemXe frmThemxe = new FormEdelivery.FrmThemXe(txtMaDVVC.Text);

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

            dataGridView1.Columns["Note"].Visible = false;

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Selected == true)
                {
                    btnThemMatHang.Enabled = true;

                    dataGridView2.Refresh();

                    dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString() && dataGridView1.CurrentRow.Cells[15].Value.ToString() == t.VehicelID.ToString()).ToList();

                    RemoveColumnDsSO();

                    //fix lỗi click vào header datagridview1
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {
                        ShowTongSoluong();
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


            }
        }

        /// <summary>
        /// Xóa các cột không cần hiển thị
        /// </summary>
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
                //row.Cells[13].ReadOnly = true;//bỏ luôn mới đúng
                row.Cells[6].ReadOnly = true;
                row.Cells[7].ReadOnly = true;
                row.Cells[8].ReadOnly = true;
                row.Cells[9].ReadOnly = true;
                row.Cells[11].ReadOnly = true;

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

                MessageBox.Show("Trọng lượng hàng vượt định mức đăng kiểm", "Cảnh báo",
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Warning);

                ////số bó cuộn bằng 1
                //dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

               // ConvertSOBC();

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
            string MaMH = dataGridView2.CurrentRow.Cells[8].Value.ToString();

            Barem barem = baremDAO.FindBaremWithAPI(textBox2.Text,MaMH);

            if (barem == null)
            {
                if(decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) == 0)
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

                    if (!KiemTraTrongLuongDaDangKy(TrongLuongDaDangKy(DKMHTEMP, dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()), edelivery_GNH_TestDAO.GetTrongLuong(dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString())))
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

            if(decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) != TrongLuong)
            {

                decimal tongcay = Math.Round(decimal.Parse(dataGridView2.CurrentRow.Cells[13].Value.ToString()) /( (decimal)barem.BAREM1/1000),0);
                //SOBC
                dataGridView2.CurrentRow.Cells[10].Value = Math.Floor(tongcay / (decimal)barem.CAY_BO);

                //SOCAYLE
                dataGridView2.CurrentRow.Cells[12].Value = tongcay - (barem.CAY_BO * Math.Floor(tongcay / (decimal)barem.CAY_BO));

                //SOCAY
                dataGridView2.CurrentRow.Cells[11].Value = tongcay - (decimal)dataGridView2.CurrentRow.Cells[12].Value;
            }
            else {
                // số cây
                dataGridView2.CurrentRow.Cells[11].Value = baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

                //trộng lượng
                dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();

                //MessageBox.Show(baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString());
            }

            ////Tỏng số cây
            //dataGridView2.CurrentRow.Cells[11].Value = baremDAO.ChangeSOBCtoSOCAY(barem, int.Parse(dataGridView2.CurrentRow.Cells[10].Value.ToString())).ToString();

            ////Tổng trộng lượng
            //dataGridView2.CurrentRow.Cells[13].Value = baremDAO.ChangeSOBCtoTrongLuong(barem, int.Parse(dataGridView2.CurrentRow.Cells[11].Value.ToString()) + socayle).ToString();

            if (!KiemTraTrongLuongDaDangKy(TrongLuongDaDangKy(DKMHTEMP, dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString()), edelivery_GNH_TestDAO.GetTrongLuong(dataGridView2.CurrentRow.Cells[6].Value.ToString(), dataGridView2.CurrentRow.Cells[8].Value.ToString())))
            {
                MessageBox.Show("Trọng lượng đăng ký vượt quá trọng lượng SO", "Lỗi",
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Error);
                //số bó cuộn bằng 0
                dataGridView2.CurrentRow.Cells[10].Value = 0.ToString();

                dataGridView2.CurrentRow.Cells[11].Value = 0.ToString();

                dataGridView2.CurrentRow.Cells[13].Value = 0.ToString();

                //SOCAYLE
                dataGridView2.CurrentRow.Cells[12].Value = 0.ToString();
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
                if(item.SONumber==SONumber && item.MaHangHoa==MAHANGHOA)
                {
                    TrongLuongDaDangKy += item.TRONGLUONG.Value;
                }    
            }
            return TrongLuongDaDangKy;

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
            decimal socayle = dKEdeliveryDAO.SoCayLeDaDK(DKMHTEMP, SOnumber, MaHH, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString());



            if (editSOCAYLE <= SOBCPBM)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// Kiểm tra số lượng bẻ bó nhập vào đã còn nhỏ hơn tổng số bẻ bó đã đăng ký hay không
        /// </summary>
        /// <param name="SOnumber">Mã đơn hàng</param>
        /// <param name="MaHH"> mã hàng hóa</param>
        /// <param name="editSOBC">User gõ số bó cuộn vào </param>
        /// <returns></returns>
        private bool KiemTraSoLuongBeBoDaDangKy(string SOnumber, string MaHH, decimal editSOBC, decimal SOLUONGBEBO)
        {
            decimal soluongbebo = dKEdeliveryDAO.SoLuongBeBoDaDK(DKMHTEMP, SOnumber, MaHH, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString());

            if (soluongbebo <= SOLUONGBEBO)
            {
                return true;
            }
            else
            {
                return false;
            }


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
        /// Thêm sản phẩm vào xe => lấy danh sách SO của khách hàng hiển thị | Truyền vào mã khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            //FrmDSDonHang frmDSDonHang = new FrmDSDonHang(dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString());
            //kiểm tra nơi giao nhận
            if (string.IsNullOrEmpty(txtMaNoiGiaoNhan.Text))
            {
                MessageBox.Show("Bạn chưa chọn nơi giao nhận", "Cảnh báo",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Error);
                return;
            }


            string BrandCode = textBox2.Text;

            string _MaNoiGiaoNhan = new CompanyDAO().GetMaNoiGiaoNhan(int.Parse(txtMaNoiGiaoNhan.Text));

            FormEdelivery.FrmDSDonHang frmDSDonHang = new FormEdelivery.FrmDSDonHang(txtMaKH.Text, dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString(), dataGridView1.CurrentRow.Cells["Romooc"].Value.ToString(), DKMHTEMP, dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString()), dataGridView1.CurrentRow.Cells[4].Value.ToString(), Guid.Parse(dataGridView1.CurrentRow.Cells[15].Value.ToString()), BrandCode,_MaNoiGiaoNhan,_userModel);

            frmDSDonHang.ShowDialog();

            if (isInsertMH)
            {

                DKMHTEMP = DKMHTEMP.Concat(DKMH).ToList();

                dataGridView2.DataSource = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString() && dataGridView1.CurrentRow.Cells[15].Value.ToString() == t.VehicelID.ToString()).OrderBy(t => int.Parse(t.SOItems)).ToList();

                RemoveColumnDsSO();

                ShowTongSoluong();

                isInsertMH = false;
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ConvertSOBC();

            ShowTongSoluong();
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

        //Xóa xe
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
            //Kiểm tra chưa chọn Mã/Tên khách hàng
            if (txtMaKH.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn Mã/Tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (int.Parse(txtMaNoiGiaoNhan.Text) == -1)
            {
                MessageBox.Show("Bạn chưa chọn nơi giao nhận hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!KiemTraKhongNhapTrongLuong())
            {
                MessageBox.Show("Bạn không được bỏ trống xe hoặc mặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Xác nhận lưu
                MessageBoxSave();
            }
        }
        /// <summary>
        /// Kiểm tra  không kê khai xe, không nhập mặt hàng, không nhập trọng lượng
        /// </summary>
        public bool KiemTraKhongNhapTrongLuong()
        {
            if (DKMHTEMP == null)
            {
                return false;
            }
            if (DKMHTEMP.Count == 0)
            {
                return false;
            }
            if (DKMHTEMP.Sum(t => t.TRONGLUONG) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Hiển thị xác nhận lưu thông tin đăng ký Edelivery
        /// </summary>
        private void MessageBoxSave()
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

                //dK_Edelivery.TenHangHoa = cbMaKH.SelectedText.ToString();

                dK_Edelivery.NgayGiaoNhan = datengaygiaonhan.Value;


                //Nếu loại đơn vị vận chuyển là khách hàng
                //  => mã và tên đơn vị vận chuyển là tên khách hàng và mả là C + mã khách hàng
                //Nếu loại đơn vị vận chuyển là nội bộ
                //  => mã và tên đơn vị vận chuyển là do combobox đơn vị vận chuyển
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

                List<string> ListSO = new List<string>();

                string checkInsert = dKEdeliveryDAO.InsertDKEdelivery(dK_Edelivery, DKMHTEMP, ref ListSO,_userModel);

                if (checkInsert != string.Empty)
                {

                    edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());

                    MessageBox.Show("Lưu thành công", "Kết quả",
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Information);

                   // MessageBox.Show(checkInsert);

                   this.Close();

                }
                else
                {
                    MessageBox.Show("Lưu thất bại", "Kết quả",
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Error);
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
            catch (Exception )
            {

               
            }

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {


            //NgayCap = DateTime.Parse(dataGridView1.CurrentRow.Cells["NgayCapCMND_CCCD"].Value.ToString());

            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                BienSoXeEdit = dataGridView1.CurrentRow.Cells["BienSo_SoHieu"].Value.ToString();

                RomoocEdit = dataGridView1.CurrentRow.Cells["Romooc"].Value.ToString();

                CMNDEdit = dataGridView1.CurrentRow.Cells["CMND_CCCD"].Value.ToString();

                HotenEdit = dataGridView1.CurrentRow.Cells["TaiXe_ThuyenTruong"].Value.ToString();

                NoiCapEdit = dataGridView1.CurrentRow.Cells["NoiCapCMND_CCCD"].Value.ToString();
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var lst = DKOld.AsQueryable();


            //if (txtSearchBienSo.Text != "")
            //{
            //    lst = lst.Where(t => t.BienSo_SoHieu.Contains(txtSearchBienSo.Text));
            //}
            //if (txtSearchHoten.Text != "")
            //{
            //    lst = lst.Where(t => t.TaiXe_ThuyenTruong.Contains(txtSearchHoten.Text));
            //}

            //dataGridView1.DataSource = lst.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var lst = DKMHTEMP.Where(t => t.BienSo_SoHieu == dataGridView1.CurrentRow.Cells[0].Value.ToString() && dataGridView1.CurrentRow.Cells[15].Value.ToString() == t.VehicelID.ToString()).AsQueryable();

            //if (txtSearchMaSO.Text != null)
            //{
            //    lst = lst.Where(t => t.SONumber.Contains(txtSearchMaSO.Text));
            //}
            //if (txtSearchMaMH.Text != null)
            //{
            //    lst = lst.Where(t => t.MaHangHoa.Contains(txtSearchMaMH.Text));
            //}

            //dataGridView2.DataSource = lst.ToList();
        }

        private void rdoTypeCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTypeCustomer.Checked)
            {
                //bDVVC.Enabled = false;
                txtDVVC.Enabled = false;
            }
        }

        private void rdoTypeLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTypeLocal.Checked)
            {
                //cbDVVC.Enabled = true;
                txtDVVC.Enabled = true;
            }
        }

        private void cbDonViCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cbDonViCompany.SelectedValue != null)
            //{
            //    cbMaKH.DataSource = null;

            //    string BrandCode = cbDonViCompany.SelectedValue.ToString();

            //    LoadCbNhanHang(BrandCode);

            //    LoadCbMaKH(BrandCode);
            //}

        }

        #region AutoComplete Đơn vị

        private void AutoCompleteVehicleEntry()
        {
            this.textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadVehicleOwnerEntry(object obj)
        {
            CompanyDAO companyDAO = new CompanyDAO();

            string[] arr = companyDAO.GetAllUsingDapper(obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteVehicleEntry(collection);
            }
        }

        private delegate void SetAutoCompleteVehicleEntryCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteVehicleEntry(AutoCompleteStringCollection collection)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetAutoCompleteVehicleEntryCallback d = new SetAutoCompleteVehicleEntryCallback(SetAutoCompleteVehicleEntry);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                textBox1.AutoCompleteCustomSource = collection;
                if (textBox1.Text != null && textBox1.Text.Length > 1 &&
                    textBox1.Text.Substring(textBox1.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadVehicleOwnerEntry);
                        myThread.Start(t.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.textBox1.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        textBox1.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        textBox2.Text = value.Substring(value.IndexOf(" | ") + 3);

                        textBox1.ReadOnly = true;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.ReadOnly = false;
        }

        #endregion AutoComplete Đơn vị

        #region AutoComplete Khách hàng

        private void AutoCompleteCustomer()
        {
            this.txTenKH.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txTenKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadCustomerAutocomplete(object obj)
        {

            string[] arr = edelivery_GNH_TestDAO.GetThongTinKhachHangUsingDapper(textBox2.Text, obj);

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
            if (this.txTenKH.InvokeRequired)
            {
                SetAutoCompleteCustomerCallback d = new SetAutoCompleteCustomerCallback(SetAutoCompleteCustomer);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txTenKH.AutoCompleteCustomSource = collection;
                if (txTenKH.Text != null && txTenKH.Text.Length > 1 &&
                    txTenKH.Text.Substring(txTenKH.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }

        private void txTenKH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != string.Empty)
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

        private void txTenKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txTenKH.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txTenKH.Text = value.Substring(0, value.IndexOf(" | "));

                        //Chọn lại mã kho nhập
                        txtMaKH.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txTenKH.ReadOnly = true;
                    }
                }
            }
        }

        private void btnRemoveKH_Click(object sender, EventArgs e)
        {
            txTenKH.Text = "";
            txtMaKH.Text = "";
            txTenKH.ReadOnly = false;

        }

        #endregion AutoComplete Khách hàng

        #region Nơi giao nhận hàng

        private void AutoComplete()
        {
            this.txtTenNoiGiaoNhan.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtTenNoiGiaoNhan.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ThreadNoiGiaoNhanAutocomplete(object obj)
        {

            string[] arr = CompanyDAO.GetAllDonViUsingDapper(textBox2.Text, obj);

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
                if (textBox2.Text != string.Empty)
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

        private void button5_Click(object sender, EventArgs e)
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

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        decimal TrongLuong;
        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            TrongLuong = (decimal)dataGridView2.CurrentRow.Cells[13].Value;
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            var a = dataGridView1.CurrentCell.DataGridView.Name;
            if (e.Control != null &&e.Control.Name.ToString() =="")
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

        private void FrmKhaiBao_FormClosing(object sender, FormClosingEventArgs e)
        {
            edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

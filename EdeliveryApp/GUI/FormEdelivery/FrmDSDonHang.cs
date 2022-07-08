using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.EntitiesFramwork;
using DAL.Model;
using BLL.DAO;
namespace GUI.FormEdelivery
{
    public partial class FrmDSDonHang : Form
    {
        Edelivery_GNHDAO Edelivery_GNH_Test = new Edelivery_GNHDAO();

        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();

        string MaKhachHang;
        string _BrandCode;
        string _NoiGiaoNhanHang;
        string BienSoXe;
        string Romooc;
        string _tentx;
        string _cmnd;
        string _noicap;
        UserModel _user;
        Guid _vehicalID;
        DateTime _ngaycap;

        List<MDangKyXe> _input;
        public FrmDSDonHang(string makhachhang, string biensoxe,string romooc, List<MDangKyXe> input, string tentx, string cmnd, DateTime ngaycap, string noicap, Guid vehicelID, string BrandCode,string NoiGiaoNhanHang, UserModel user)
        {
            InitializeComponent();

            this.MaKhachHang = makhachhang;
            this.BienSoXe = biensoxe;
            this.Romooc = romooc;
            this._input = input;
            this._cmnd = cmnd;
            this._noicap = noicap;
            this._ngaycap = ngaycap;
            this._tentx = tentx;
            this._vehicalID = vehicelID;
            this._BrandCode = BrandCode;
            this._NoiGiaoNhanHang = NoiGiaoNhanHang;
            this._user = user;
        }

        private void FrmDSDonHang_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Edelivery_GNH_Test.LoadDsMatHang(MaKhachHang, _BrandCode,_NoiGiaoNhanHang, _input);

            RemoveColumnDsMatHang();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[13].ReadOnly = false;
                row.Cells[6].ReadOnly = false;

                row.Cells[9].ReadOnly = false;
                row.Cells[14].ReadOnly = false;
                row.Cells[5].ReadOnly = false;
            }
        }

        /// <summary>
        /// Xóa các cột không cần hiển thị
        /// </summary>
        private void RemoveColumnDsMatHang()
        {
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["MADONVICUNGCAP"].Visible = false;
            dataGridView1.Columns["TENDONVICUNGCAP"].Visible = false;
            dataGridView1.Columns["MAKHACHANG"].Visible = false;
            dataGridView1.Columns["TENKHACHHANG"].Visible = false;
            dataGridView1.Columns["SOCAY"].Visible = false;
            dataGridView1.Columns["SOBC"].Visible = false;
            dataGridView1.Columns["SOLUONGBEBO"].Visible = false;
            dataGridView1.Columns["SOCAYLE"].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            //dataGridView1.Columns["SONumberBPM"].Visible = false;
        }


        /// <summary>
        /// Tìm kiếm danh sách mặt hàng theo đơn hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            dataGridView1.DataSource = Edelivery_GNH_Test.LoadDsMatHang(MaKhachHang, _BrandCode, _NoiGiaoNhanHang, _input,txtSearch.Text);

        }

        /// <summary>
        /// reset danh sách mặt hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            txtSearch.Text = String.Empty;
            //dataGridView1.DataSource = Edelivery_GNH_Test.LoadDsMatHang(MaKhachHang, _BrandCode, _input);

        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            MessageBoxSave();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Hiển thị xác nhận thêm đơn hàng/sản phẩm vào xe
        /// </summary>
        private void MessageBoxSave()
        {
            const string message = "Bạn có muốn thêm đơn hàng/sản phầm vào xe?";
            const string caption = "Xác nhận thêm SO";


            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                List<MDangKyXe> rs = new List<MDangKyXe>();

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    //MessageBox.Show($"Mã đơn hàng {row.Cells[5].Value.ToString()} : Biển số xe {BienSoXe} : Mã sản phẩm {row.Cells[11].Value.ToString()} : TRỌNG LƯỢNG KHAI BÁO {row.Cells[17].Value.ToString()}");


                    if (KiemTraMatHangDaCo(this._input, row.Cells[2].Value.ToString(), row.Cells[8].Value.ToString(), BienSoXe, _vehicalID))
                    {
                        UserModel user = Edelivery_GNH_Test.KiemTraSONumberLockByUserId(row.Cells[2].Value.ToString(), row.Cells[8].Value.ToString());



                        if (user != null && _user.UserId != user.UserId)
                        {

                            MessageBox.Show($"Mặt hàng mã: {row.Cells[8].Value.ToString()}  đã có User {user.FullName} đang thực hiện", "Cảnh báo",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning);
                        }
                        else
                        {
                            List<MDangKyXe> bv = Edelivery_GNH_Test.InsertThongTinSO(row.Cells[2].Value.ToString(), row.Cells[17].Value.ToString(), row.Cells[8].Value.ToString(), BienSoXe, Romooc, row.Cells[9].Value.ToString(), row.Cells[3].Value.ToString(), _tentx, _cmnd, _ngaycap, _noicap, _vehicalID, _user.UserId);

                            rs = rs.Concat(bv).ToList();
                        }


                      
                    }
                    else
                    {
                        MessageBox.Show($"Mặt hàng mã: {row.Cells[8].Value.ToString()}  đã có không thể thêm", "Cảnh báo",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning);
                    }



                }
                FrmKhaiBao.Instance.DKMH = rs;

                FrmKhaiBao.Instance.isInsertMH = true;

                this.Close();
            }
        }

        /// <summary>
        /// Kiểm tra mặt hàng đã có trong xe hiện tại
        /// </summary>
        /// <param name="input">Danh sách xe đăng ký với mặt hàng</param>
        /// <param name="MaMH">mã mặt hàng</param>
        /// <param name="BienSo">biển số</param>
        /// <param name="VehicelID">mã biển số từng thời điểm</param>
        /// <returns></returns>
        private bool KiemTraMatHangDaCo(List<MDangKyXe> input, string SONumber , string MaMH, string BienSo, Guid VehicelID)
        {

            if (input.Where(t => t.SONumber == SONumber && t.MaHangHoa == MaMH && t.BienSo_SoHieu == BienSo && t.VehicelID == VehicelID).Count() == 0)
            {
                return true;
            }
            return false;

        }

    }
}

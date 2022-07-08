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

namespace GUI.FormEdelivery
{
    public partial class FrmDSDonHangSuaDangKy : Form
    {
        Edelivery_GNHDAO Edelivery_GNH_Test = new Edelivery_GNHDAO();

        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();

        string MaKhachHang;
        string _MaNoiGiaoNhan;
        string _Brancode;
        string BienSoXe;
        string Romooc;
        string _tentx;
        string _cmnd;
        string _noicap;
        Guid _vehicalID;
        Guid _EdeliveryID;
        DateTime _ngaycap;
        UserModel _user;

        List<MDangKyXe> _input;
        public FrmDSDonHangSuaDangKy(string makhachhang, string biensoxe,string romooc, List<MDangKyXe> input, string tentx, string cmnd, DateTime ngaycap, string noicap, Guid vehicelID, Guid Edelivery, string branchcode ,string MaNoiGiaoNhan, UserModel user)
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
            this._EdeliveryID = Edelivery;
            this._MaNoiGiaoNhan = MaNoiGiaoNhan;
            this._Brancode = branchcode;
            this._user = user;
        }

        private void FrmDSDonHangSuaDangKy_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Edelivery_GNH_Test.LoadDsMatHangSua(MaKhachHang, _Brancode, _MaNoiGiaoNhan, _input);

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
            //dataGridView1.Columns["SONumberBPM"].Visible = false;

            //đã khai báo
            dataGridView1.Columns["Column7"].Visible = false;
            
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            MessageBoxSave();
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
                        List<MDangKyXe> bv = Edelivery_GNH_Test.InsertThongTinSO(row.Cells[2].Value.ToString(), row.Cells[17].Value.ToString(), row.Cells[8].Value.ToString(), BienSoXe, Romooc, row.Cells[9].Value.ToString(), row.Cells[3].Value.ToString(), _tentx, _cmnd, _ngaycap, _noicap, _vehicalID, _user.UserId);

                        rs = rs.Concat(bv).ToList();
                    }
                    else
                    {
                        MessageBox.Show($"Mặt hàng mã: {row.Cells[8].Value.ToString()}  đã có không thể thêm", "Cảnh báo",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning);
                    }



                }
                FrmSuaMatHang.Instance.DKMH = rs;

                FrmSuaMatHang.Instance.isInsertMH = true;

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
        private bool KiemTraMatHangDaCo(List<MDangKyXe> input, string SONumber ,string MaMH, string BienSo, Guid VehicelID)
        {

            if (input.Where(t => t.SONumber == SONumber && t.MaHangHoa == MaMH && t.BienSo_SoHieu == BienSo && t.VehicelID == VehicelID).Count() == 0)
            {
                return true;
            }
            return false;

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            dataGridView1.Refresh();
            //Edelivery_GNH_Test.LoadDsMatHangSua(MaKhachHang, _Brancode, _MaNoiGiaoNhan, _input, _EdeliveryID);
            dataGridView1.DataSource = Edelivery_GNH_Test.LoadDsMatHangSua(MaKhachHang, _Brancode, _MaNoiGiaoNhan, _input, txtSearch.Text);
        }
    }
}

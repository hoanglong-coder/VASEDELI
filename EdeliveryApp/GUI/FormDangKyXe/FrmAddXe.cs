using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DAO;
using DAL.EntitiesFramwork;
namespace GUI.FormDangKyXe
{
    public partial class FrmAddXe : Form
    {

        VehicleOwnerModelDAO vehicleOwnerModelDAO = new VehicleOwnerModelDAO(); 

        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();
        public FrmAddXe()
        {
            InitializeComponent();
        }

        private void FrmAddXe_Load(object sender, EventArgs e)
        {
            rdoXeKH.Checked = true;
            txtRomoc.ReadOnly = true;
            AutoCompleteDVVC();
            AutoComplete(this.txtRomoc);
            txtTiLeVuot.Text = 10.ToString();
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
        private void AutoComplete(TextBox textBox)
        {
            textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckRaiobutonKieuXe())
                {
                    MessageBox.Show("Bạn chưa chọn kiểu xe");

                    return;
                }

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
                    if (!vehicleModelDAO.KiemTraTrungBienSoXwe(txtBienSoXe.Text,txtMaDVVC.Text))
                    {
                        MessageBox.Show("Biển số xe đã có không thể thêm!", "Lỗi",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                        return;
                    }

                    VehicleModel vehicleModel = new VehicleModel();
                    vehicleModel.VehicleId = Guid.NewGuid();
                    vehicleModel.Type = rdoXeKH.Checked ? 2 : 1;
                    vehicleModel.VehicleNumber = txtBienSoXe.Text;
                    vehicleModel.VehicleOwner = txtMaDVVC.Text;
                    vehicleModel.VehicleWeight =decimal.Parse(txtBiXe.Text);
                    vehicleModel.isRoMooc = rdoXeRomoc.Checked ? 1 : 0;
                    vehicleModel.TrongLuongDangKiem = decimal.Parse(txtTrongluongdangkiem.Text);
                    vehicleModel.TyLeVuot = decimal.Parse(txtTiLeVuot.Text);
                    vehicleModel.isLockEdit = false;
                    vehicleModel.isLock = true;
                    vehicleModel.CreatedTime = DateTime.Now;
                    vehicleModel.isDauKeo = rdoXeDauKeo.Checked ? true : false;

                    var ketqua = vehicleModelDAO.AddXe(vehicleModel,txtRomoc.Text);

                    if (ketqua)
                    {
                        MessageBox.Show("Thêm xe thành công");
                        this.Close();
                    }else
                    {
                        MessageBox.Show("Thêm xe bị lỗi");
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi "+ ex.Message);
            }
        }
        private bool CheckRaiobutonKieuXe()
        {
            if (rdoXeDauKeo.Checked != false || rdoXeRomoc.Checked != false || rdoXeThuong.Checked != false)
            {
                return true;
            }
            return false;
        }

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

        private void txtTiLeVuot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
                
        }

        private void txtBiXe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTrongluongdangkiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //private void txtBiXe_Leave(object sender, EventArgs e)
        //{
        //    Double value;
        //    if (Double.TryParse(txtBiXe.Text, out value))
        //        txtBiXe.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N0}", value);
        //    else
        //        txtBiXe.Text = String.Empty;
        //}

        //private void txtTrongluongdangkiem_Leave(object sender, EventArgs e)
        //{
        //    Double value;
        //    if (Double.TryParse(txtTrongluongdangkiem.Text, out value))
        //        txtTrongluongdangkiem.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N0}", value);
        //    else
        //        txtTrongluongdangkiem.Text = String.Empty;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


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
        private void txtSearhDonViVanChuyen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoXeNB.Checked)
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
                        txtMaDVVC.Text = value.Substring(value.IndexOf(" | ") + 3);

                        txtSearhDonViVanChuyen.ReadOnly = true;
                    }
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

        #endregion AutoComplete Đơn vị vận chuyển

        

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearhDonViVanChuyen.Text = "";
            txtMaDVVC.Text = "";
            txtSearhDonViVanChuyen.ReadOnly = false;
        }

        private void rdoXeDauKeo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoXeDauKeo.Checked)
            {
                txtRomoc.ReadOnly = false;
            }
            else
            {
                txtRomoc.ReadOnly = true;
            }
        }
        #region AutoComplete Rơ mooc

        private void ThreadVehicleRomocModel(object obj)
        {

            string[] arr = vehicleModelDAO.GetALLRomocUsingDapper(txtMaDVVC.Text, obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteVehicleRomocModel(collection);
            }
        }

        private delegate void SetAutoCompleteVehicleRomoocModelCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteVehicleRomocModel(AutoCompleteStringCollection collection)
        {
            if (this.txtRomoc.InvokeRequired)
            {
                SetAutoCompleteVehicleRomoocModelCallback d = new SetAutoCompleteVehicleRomoocModelCallback(SetAutoCompleteVehicleRomocModel);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtRomoc.AutoCompleteCustomSource = collection;
                if (txtRomoc.Text != null && txtRomoc.Text.Length > 1 &&
                    txtRomoc.Text.Substring(txtRomoc.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }
        private void txtRomoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtRomoc.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại kho nhập
                        txtRomoc.Text = value;
                    }
                }
            }
        }

        private void txtRomoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadVehicleRomocModel);
                        myThread.Start(t.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            txtRomoc.Text = "";
            txtRomoc.ReadOnly = false;
        }
        #endregion AutoComplete Rơ mooc

        private void txtRomoc_TextChanged_1(object sender, EventArgs e)
        {

        }


    }
}

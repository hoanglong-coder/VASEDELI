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
namespace GUI.FormEdelivery
{
    public partial class FrmThemXe : Form
    {
        // NHẬN MÃ ĐƠN VỊ VẬN CHUYỂN ĐỂ SEARCH XE NHANH HƠN
        string _MaDVVC;

        VehicleModelDAO vehicleModelDAO = new VehicleModelDAO();
        DriverRegisterDAO RegisterDAO = new DriverRegisterDAO();
        Edelivery_GNHDAO Edelivery_GNHDAO = new Edelivery_GNHDAO();    

        public FrmThemXe(string MaDVVC)
        {
            _MaDVVC = MaDVVC;

            InitializeComponent();


        }

        private void FrmThemXe_Load(object sender, EventArgs e)
        {
            AutoComplete(this.txtBienSoXe);

            AutoComplete(this.txtRomoc);

            AutoComplete(this.txtCMND_GPLX);
        }

        private void txtHoten_TextChanged(object sender, EventArgs e)
        {
            txtHoten.SelectionStart = txtHoten.Text.Length;
            txtHoten.Text = txtHoten.Text.ToUpper();
        }

        /// <summary>
        /// Đăng ký xe => kết quả trả về danh sách xe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            string pattern = @"^([0-9]*([A-Z]{1}|[A-Z]{2}))[-](([0-9]{3}[.][0-9]{2})|([0-9]{4}))\b";

            string patternromoc = @"^([0-9]*[R]{1})[-](([0-9]{3}[.][0-9]{2})|([0-9]{4}))\b";

            string patter_cmnd_gplx = @"\b([0-9]{12})\b|\b([0-9]{9})\b";

            string patter_ten = @"^([a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+)((\s{1}[a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+){1,})$";

            Match m = Regex.Match(txtBienSoXe.Text, pattern);

            Match romoc = Regex.Match(txtRomoc.Text, patternromoc);

            Match cmnd = Regex.Match(txtCMND_GPLX.Text, patter_cmnd_gplx);

            Match ten = Regex.Match(txtHoten.Text.Trim().ToLower(), patter_ten);


            if (txtBienSoXe.Text == String.Empty)
            {
                MessageBox.Show("Biển số xe không được để trống", "Cảnh báo",
                                     MessageBoxButtons.OKCancel,
                                     MessageBoxIcon.Warning);
            }
            else if (!m.Success)
            {
                MessageBox.Show("Biển số xe phải đúng định dạng. Ví dụ 51C-999.99 hoặc 51C-9999 KHÔNG được viết liền nhau 51C99999 ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else if (!romoc.Success && txtRomoc.Text != string.Empty)
            {
                MessageBox.Show("Biển số rơ móc phải đúng định dạng. Ví dụ 51R-999.99 hoặc 51R-9999 KHÔNG được viết liền nhau 51R99999 ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);

            }
            else if (!cmnd.Success)
            {

                MessageBox.Show("Số CMND/GPLX phải đúng định dạng 9 chữ số hoặc 12 chữ số ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else if (txtHoten.Text == string.Empty || txtHoten.Text == "")
            {

                MessageBox.Show("Họ tên không được để trống ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);

            }
            else if (!ten.Success)
            {
                MessageBox.Show("Họ tên không đúng định dạng: Ví dụ không được chứ ký tự đặt biệt và chữ số ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);

            }else if(!vehicleModelDAO.KiemTraXeCoTrongData(txtBienSoXe.Text, _MaDVVC))
                    {
                MessageBox.Show("Không có biển số xe này ", "Cảnh báo",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Warning);
            }
            else
            {

                const string message = "Bạn có muốn thêm xe?";
                const string caption = "Xác nhận đăng ký xe";


                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OKCancel,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.OK)
                {
                    //key801
                    FrmKhaiBao.Instance.DK = Edelivery_GNHDAO.InsertThongTinTaiXe(txtBienSoXe.Text, txtRomoc.Text, txtCMND_GPLX.Text, txtHoten.Text, txtNoiCap.Text, dtNgaycap.Value);

                    FrmKhaiBao.Instance.isInsertNew = true;

                    this.Close();
                }

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoComplete(TextBox textBox)
        {
            textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }


        #region AutoComplete Biển số xe

        private void ThreadVehicleModel(object obj)
        {

            string[] arr = vehicleModelDAO.GetALLUsingDapper(_MaDVVC, obj);

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteModel(collection);
            }
        }

        private delegate void SetAutoCompleteVehicleModelCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteModel(AutoCompleteStringCollection collection)
        {
            if (this.txtBienSoXe.InvokeRequired)
            {
                SetAutoCompleteVehicleModelCallback d = new SetAutoCompleteVehicleModelCallback(SetAutoCompleteModel);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtBienSoXe.AutoCompleteCustomSource = collection;
                if (txtBienSoXe.Text != null && txtBienSoXe.Text.Length > 1 &&
                    txtBienSoXe.Text.Substring(txtBienSoXe.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }



        private void txtBienSoXe_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadVehicleModel);
                        myThread.Start(t.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBienSoXe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtBienSoXe.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại biển số xe
                        txtBienSoXe.Text = value;

                        if (vehicleModelDAO.IsDauKeo(_MaDVVC, txtBienSoXe.Text))
                        {
                            txtRomoc.Text = vehicleModelDAO.ListVehicleNumberRomooc(txtBienSoXe.Text);
                        }

                        txtBienSoXe.ReadOnly = true;

                        txtRomoc.ReadOnly = true;
                    }
                }
            }
        }

        private void btnRemoveVehicle_Click(object sender, EventArgs e)
        {
            txtBienSoXe.Text = "";
            txtRomoc.Text = "";
            txtBienSoXe.ReadOnly = false;
            txtRomoc.ReadOnly = false;
        }
        #endregion AutoComplete Biển số xe

        #region AutoComplete Chứng minh - giấy phép lái xe

        private void ThreadCMND_GPLX(object obj)
        {
            
            string[] arr = RegisterDAO.GetAllUsingDapper(_MaDVVC.Substring(1), obj);

            if (arr == null)
            {
                return;
            }

            if (arr.Length > 0)
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                collection.AddRange(arr);
                SetAutoCompleteCMND_GPLX(collection);
            }
        }

        private delegate void SetAutoCompleteCMND_GPLXCallback(AutoCompleteStringCollection collection);

        private void SetAutoCompleteCMND_GPLX(AutoCompleteStringCollection collection)
        {
            if (this.txtCMND_GPLX.InvokeRequired)
            {
                SetAutoCompleteCMND_GPLXCallback d = new SetAutoCompleteCMND_GPLXCallback(SetAutoCompleteCMND_GPLX);
                this.Invoke(d, new object[] { collection });
            }
            else
            {
                txtCMND_GPLX.AutoCompleteCustomSource = collection;
                if (txtCMND_GPLX.Text != null && txtCMND_GPLX.Text.Length > 1 &&
                    txtCMND_GPLX.Text.Substring(txtCMND_GPLX.Text.Length - 1) == " "
                    )
                {
                    SendKeys.Send("{DELETE}");
                }
            }
        }


        private void txtCMND_GPLX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String selItem = this.txtCMND_GPLX.Text;
                if (selItem.Contains(" | "))
                {
                    string value = selItem.Substring(selItem.IndexOf(" | ") + 3);
                    if ((value.IndexOf(" | ") + 3) < value.Length)
                    {
                        //Chọn lại
                        txtCMND_GPLX.Text = value.Substring(0, value.IndexOf(" | "));
                        
                        string[] vs = value.Split('|');

                        txtHoten.Text = vs[1];


                        try
                        {
                            dtNgaycap.Value = DateTime.Parse(vs[2]);

                        }
                        catch (Exception)
                        {
                            string[] ngay = vs[2].Split('/');

                            string ngaycap = ngay[1] + '/' + ngay[0] + '/' + ngay[2];

                            dtNgaycap.Value = DateTime.Parse(ngaycap);
                        }
                      

                        txtNoiCap.Text = vs[3];

                        txtCMND_GPLX.ReadOnly = true;

                        txtHoten.ReadOnly = true;

                        
                    }
                }
            }
        }

        private void txtCMND_GPLX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 2 or more chars
                    if (t.Text.Length >= 2)
                    {
                        var myThread = new Thread(ThreadCMND_GPLX);
                        myThread.Start(t.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveCMCC_Click(object sender, EventArgs e)
        {
            txtCMND_GPLX.Text = "";
            txtCMND_GPLX.ReadOnly = false;
            txtHoten.Text = "";
            txtHoten.ReadOnly = false;
        }
        #endregion AutoComplete Chứng minh - giấy phép lái xe

        #region AutoComplete Rơ mooc

        private void ThreadVehicleRomocModel(object obj)
        {

            string[] arr = vehicleModelDAO.GetALLRomocUsingDapper(_MaDVVC, obj);

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

        private void btnRemoveIsRomoc_Click(object sender, EventArgs e)
        {
            txtRomoc.Text = "";
            txtRomoc.ReadOnly = false;
        }


        #endregion AutoComplete Rơ mooc


    }
}

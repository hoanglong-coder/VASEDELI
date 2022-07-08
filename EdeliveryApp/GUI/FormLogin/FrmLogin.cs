using BLL.DAO;
using Constant;
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

namespace GUI.FormLogin
{
    public partial class FrmLogin : Form
    {
        UserModelDAO userModelDAO = new UserModelDAO();
        public FrmLogin()
        {
            InitializeComponent();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            var listCompanyCode = new List<StatusDK_Edelivery>() {
                                new StatusDK_Edelivery(){Value = 1000, Name = "VASG_1000"},
                                new StatusDK_Edelivery(){Value = 3000, Name = "NSS_3000"},
                                new StatusDK_Edelivery(){Value = 4000, Name = "AHT_4000"},
                                new StatusDK_Edelivery(){Value = 5000, Name = "VMS_5000"},
                                new StatusDK_Edelivery(){Value = 6000, Name = "TMS_6000"},
                                new StatusDK_Edelivery(){Value = 7000, Name = "DNS_7000"},
            };

            comboBox1.DataSource = listCompanyCode;

            comboBox1.DisplayMember = "Name";

            comboBox1.ValueMember = "Value";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            KiemTra();
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                KiemTra();
            }

        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                KiemTra();
            }
        }

        #region Login

        public void KiemTra()
        {
            if (userModelDAO.IsDbConnectionOK())
            {

                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Chưa điền đủ thông tin đăng nhập");
                    return;
                }

                UserModel kiemtra = UserModelDAO.KiemTraDangNhap(txtUserName.Text.Trim().ToLower(), txtPassword.Text, comboBox1.SelectedValue.ToString());

                
                
                UserModel kiemtraadmin = UserModelDAO.KiemTraDangNhapAdmin(txtUserName.Text.Trim().ToLower(), txtPassword.Text);

                if (kiemtraadmin != null)
                {
                    if (kiemtraadmin.GroupUser == Constant.User.Admin)
                    {
                        MessageBox.Show("Đăng nhập thành công");

                        Program.SetMainForm(new FormAdmin.FrmAdmin());

                        Program.ShowMainForm();

                        this.Close();
                    }else if (kiemtraadmin.GroupUser == Constant.User.Kho)
                    {
                        MessageBox.Show("Đăng nhập thành công");

                        Program.SetMainForm(new FromInstance.FrmKho(kiemtraadmin));

                        Program.ShowMainForm();

                        this.Close();
                    }
                    else if (kiemtra != null)
                    {
                        MessageBox.Show("Đăng nhập thành công");



                        Program.SetMainForm(new FrmMain(kiemtra));

                        Program.ShowMainForm();

                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Thông tin đăng nhập không chính xác");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Không kết nói được server. Kết nói VPN!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }            
        }

        #endregion Login

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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

namespace GUI
{
    public partial class FrmMain : Form
    {      
        UserModel _userModel;
        Edelivery_GNHDAO edelivery_GNH_TestDAO = new Edelivery_GNHDAO();
        public FrmMain(UserModel userModel)
        {
            InitializeComponent();

            _userModel = userModel;
           
        }

        private void kHAIBÁOGIAONHẬNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmKhaiBao")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormEdelivery.FrmKhaiBao form1 = new FormEdelivery.FrmKhaiBao(_userModel);

                form1.MdiParent = this;

                form1.Show();
            }
        }

        private void kẾHOẠCHGIAONHẬNToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void kẾHOẠCHGIAONHẬNTỔNGHỢPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmKeHoachGiaoNhanTongHop")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormEdelivery.FrmKeHoachGiaoNhanTongHop cf = new FormEdelivery.FrmKeHoachGiaoNhanTongHop();


                cf.MdiParent = this;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }

        private void dANHSÁCHKHAIBÁOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmDSKhaiBao")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormEdelivery.FrmDSKhaiBao cf = new FormEdelivery.FrmDSKhaiBao(_userModel);


                cf.MdiParent = this;

                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }

        private void dANHMỤCTÀIXẾToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            MessageBox.Show("Lưu thành công");
        }

        private void dANHMỤCXEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmDsXe")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormDangKyXe.FrmDsXe cf = new FormDangKyXe.FrmDsXe(_userModel.CompanyCode);


                cf.MdiParent = this;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            edelivery_GNH_TestDAO.DeleteUserLockId(_userModel.UserId.ToString());
        }

        private void tHÔNGTINUSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmInfo")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormEdelivery.FrmInfo cf = new FormEdelivery.FrmInfo();
                cf.MdiParent = this;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }

        private void dANHMỤCĐƠNVỊVẬNCHUYỂNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmDSDVVC")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormDVVC.FrmDSDVVC cf = new FormDVVC.FrmDSDVVC();
                cf.MdiParent = this;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }

        private void dANHMỤCTÀIXẾToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool isOpne = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FrmDSTaiXe")
                {
                    isOpne = true;
                    item.Focus();
                    break;
                }
            }

            if (!isOpne)
            {
                FormTaiXe.FrmDSTaiXe cf = new FormTaiXe.FrmDSTaiXe();
                cf.MdiParent = this;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                cf.Show();
            }
        }
    }
}

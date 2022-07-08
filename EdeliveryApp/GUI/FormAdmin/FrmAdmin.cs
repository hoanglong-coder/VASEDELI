using BLL.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormAdmin
{
    public partial class FrmAdmin : Form
    {
        UserModelDAO  userdao = new UserModelDAO();
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            LoadDatagridView();
        }
        //Load danh sách user
        public void LoadDatagridView()
        {
            dataGridView1.DataSource = userdao.GetAll();
        }
    }
}

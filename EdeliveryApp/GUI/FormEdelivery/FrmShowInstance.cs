using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormEdelivery
{
    public partial class FrmShowInstance : Form
    {
        List<MInstance> _frmShowInstances;
        public FrmShowInstance(List<MInstance> frmShowInstances)
        {
            InitializeComponent();
            _frmShowInstances = frmShowInstances;
        }

        private void FrmShowInstance_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _frmShowInstances;
        }
    }
}

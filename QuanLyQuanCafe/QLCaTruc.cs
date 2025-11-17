using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class QLCaTruc : Form
    {
        public QLCaTruc()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string tenTaiKhoan = row.Cells["TenTaiKhoan"].Value.ToString();
            string buoi = row.Cells["Buoi"].Value.ToString();
            bool thu2 = (bool)row.Cells["Thu2"].Value;
            bool thu3 = (bool)row.Cells["Thu3"].Value;
            bool thu4 = (bool)row.Cells["Thu4"].Value;
            bool thu5 = (bool)row.Cells["Thu5"].Value;
            bool thu6 = (bool)row.Cells["Thu6"].Value;
            bool thu7 = (bool)row.Cells["Thu7"].Value;
            bool chuNhat = (bool)row.Cells["ChuNhat"].Value;

            QL_DAO.Instance.UpdateQuanLy(tenTaiKhoan, buoi, thu2, thu3, thu4, thu5, thu6, thu7, chuNhat);

        }

        private void QLCaTruc_Load(object sender, EventArgs e)
        {
            DataTable data = QL_DAO.Instance.GetQuanLy();
            dataGridView1.DataSource = data;

            dataGridView1.Columns["TenTaiKhoan"].Width = 150;

        }
    }
}

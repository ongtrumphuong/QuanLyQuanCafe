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
    public partial class Revenue : Form
    {
        public Revenue()
        {
            InitializeComponent();
            loadDateTimeBill();
            loadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        void loadDateTimeBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void loadListBillByDate (DateTime checkIn, DateTime checkOut)
        {
            dtThongKe.DataSource = Bill_DAO.Instance.getBillListByDate(checkIn, checkOut);
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
    }
}

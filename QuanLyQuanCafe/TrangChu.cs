using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyQuanCafe
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
            loadTotalByNameFood();
        }
        
        void loadTotalByNameFood()
        {
            dtTotal.DataSource = Bill_DAO.Instance.getListNameFood();
            DataTable total = Bill_DAO.Instance.getListNameFood();
            dtTotal.Columns["Tên sản phẩm"].Width = 180;


            Pie.Series.Clear();
            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Pie;
            foreach (DataRow row in total.Rows)
            {
                string productName = row["Tên sản phẩm"].ToString();
                int totalSales = Convert.ToInt32(row["Tổng tiền"]);
                series.Points.AddXY(productName, totalSales);
            }

            Pie.Series.Add(series);
            Pie.ChartAreas[0].RecalculateAxesScale();

            DataRow spBestSeller = Bill_DAO.Instance.getBestSellingProduct();
            if (spBestSeller != null)
            {
                string name = spBestSeller["Tên sản phẩm"].ToString();
                int sale = Convert.ToInt32(spBestSeller["Tổng tiền"]);
                lbBestSeller.Text = $"{name}";
                lbSale.Text = $"{sale.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"))}";
            }

            DataRow totalRevenue = Bill_DAO.Instance.getTotalRevenueForMonth(DateTime.Now);
            if (totalRevenue != null)
            {
                int tongDoanhThu = Convert.ToInt32(totalRevenue["Tổng doanh thu"]);
                lbDoanhThu.Text = $"{tongDoanhThu.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"))}";
            }
            DataRow slQuanLyAndNhanVien = Account_DAO.Instance.getEmployeeAndManagerCount();
            if (slQuanLyAndNhanVien != null)
            {
                int employeeCount = Convert.ToInt32(slQuanLyAndNhanVien["EmployeeCount"]);
                int managerCount = Convert.ToInt32(slQuanLyAndNhanVien["ManagerCount"]);
                lbNhanVien.Text = $"{employeeCount}";
                lbQuanLy.Text = $"{managerCount}";
            }
        }
    }
}

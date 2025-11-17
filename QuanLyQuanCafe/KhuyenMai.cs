using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLyQuanCafe
{
    public partial class KhuyenMai : Form
    {
        BindingSource discountList = new BindingSource();
        public KhuyenMai()
        {
            InitializeComponent();
            dtgvDiscount.DataSource = discountList;
            loadActiveCombobox(cbTrangThai);
            loadDiscountList();
            addDisCountBinding();
        }
        void loadActiveCombobox(ComboBox cb)
        {
            // Kết nối CSDL và thực thi truy vấn SQL để lấy danh sách các giá trị Trạng thái duy nhất từ cột Trạng thái
            string query = "SELECT DISTINCT TrangThai FROM KhuyenMai";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Xóa tất cả các mục trong ComboBox trước khi thêm các giá trị mới
            cbTrangThai.Items.Clear();

            // Duyệt qua từng dòng trong DataTable và thêm giá trị vào ComboBox
            foreach (DataRow row in data.Rows)
            {
                string trangThai = row["TrangThai"].ToString();
                cbTrangThai.Items.Add(trangThai);
            }
        }
        void loadDiscountList()
        {
            discountList.DataSource = Discount_DAO.Instance.getListDiscount();
        }
        void addDisCountBinding()
        {
            txMaKM.DataBindings.Add(new Binding("Text", dtgvDiscount.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txTenKM.DataBindings.Add(new Binding("Text", dtgvDiscount.DataSource, "NameCode", true, DataSourceUpdateMode.Never));
            txGiaTri.DataBindings.Add(new Binding("Text", dtgvDiscount.DataSource, "GiaTri", true, DataSourceUpdateMode.Never));
            txDieuKien.DataBindings.Add(new Binding("Text", dtgvDiscount.DataSource, "Condition", true, DataSourceUpdateMode.Never));
            dtNgayBatDau.DataBindings.Add(new Binding("Value", dtgvDiscount.DataSource, "StartDate", true, DataSourceUpdateMode.Never));
            dtNgayKetThuc.DataBindings.Add(new Binding("Value", dtgvDiscount.DataSource, "EndDate", true, DataSourceUpdateMode.Never));
            cbTrangThai.DataBindings.Add(new Binding("Text", dtgvDiscount.DataSource, "Active", true, DataSourceUpdateMode.Never));
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            int maKM = int.Parse(txMaKM.Text);
            string tenKM = txTenKM.Text;
            float giaTri = float.Parse(txGiaTri.Text);
            string dieuKien = txDieuKien.Text;
            DateTime ngayBatDau = dtNgayBatDau.Value;
            DateTime ngayKetThuc = dtNgayKetThuc.Value;
            string trangThai = cbTrangThai.Text;

            if (Discount_DAO.Instance.InsertDiscount(maKM, tenKM, giaTri, dieuKien, ngayBatDau, ngayKetThuc, trangThai))
            {
                MessageBox.Show("Thêm Khuyến mãi thành công !");
                loadDiscountList();

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm khuyến mãi !");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int maKM = int.Parse(txMaKM.Text);
            string tenKM = txTenKM.Text;
            float giaTri = float.Parse(txGiaTri.Text);
            string dieuKien = txDieuKien.Text;
            DateTime ngayBatDau = dtNgayBatDau.Value;
            DateTime ngayKetThuc = dtNgayKetThuc.Value;
            string trangThai = cbTrangThai.Text;

            if (Discount_DAO.Instance.updateDiscount(maKM, tenKM, giaTri, dieuKien, ngayBatDau, ngayKetThuc, trangThai))
            {
                MessageBox.Show("Cập nhật Khuyến mãi thành công !");
                loadDiscountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật khuyến mãi !");
            }
        }
        List<Discount> searchDiscountByName(string name)
        {
            List<Discount> dsFood = Discount_DAO.Instance.SearchDiscount(name);

            return dsFood;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            discountList.DataSource = searchDiscountByName(txSearch.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            loadDiscountList();
        }
    }
}

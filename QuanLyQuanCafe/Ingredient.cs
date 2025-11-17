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
    public partial class Ingredient : Form
    {
        BindingSource NguyenLieuList = new BindingSource();
        public Ingredient()
        {
            InitializeComponent();
            dtgvNguyenLieu.DataSource = NguyenLieuList;
            LoadListNguyenLieu();
            AddNguyenLieuBinding();
        }
        void AddNguyenLieuBinding()
        {
            //ktbd
            txId.DataBindings.Add(new Binding("Text", dtgvNguyenLieu.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txTen.DataBindings.Add(new Binding("Text", dtgvNguyenLieu.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmSoLuong.DataBindings.Add(new Binding("Value", dtgvNguyenLieu.DataSource, "SoLuong", true, DataSourceUpdateMode.Never));
        }
        void LoadListNguyenLieu()
        {
            NguyenLieuList.DataSource = NguyenLieu_DAO.Instance.getListNguyenLieu();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txTen.Text;
            float soLuong = (float)nmSoLuong.Value;
            if (NguyenLieu_DAO.Instance.InsertNguyenLieu(name, soLuong))
            {
                MessageBox.Show("Thêm nguyên liệu thành công!");
                LoadListNguyenLieu();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm nguyên liệu!");
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadListNguyenLieu();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txId.Text);
            string name = txTen.Text;
            float soLuong = (float)nmSoLuong.Value;
            if (NguyenLieu_DAO.Instance.UpdateNguyenLieu(id, name, soLuong))
            {
                MessageBox.Show("Sửa nguyên liệu thành công!");
                LoadListNguyenLieu();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa nguyên liệu!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txId.Text);
            if (NguyenLieu_DAO.Instance.DeleteNguyenLieu(id))
            {
                MessageBox.Show("Xóa nguyên liệu thành công!");
                LoadListNguyenLieu();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa nguyên liệu!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            NguyenLieuList.DataSource = NguyenLieu_DAO.Instance.SearchNguyenLieuByName(txTimKiem.Text);
        }
    }
}

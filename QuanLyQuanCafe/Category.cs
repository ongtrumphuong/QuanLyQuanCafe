using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
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
    public partial class Category : Form
    {
        BindingSource CategoryList = new BindingSource();
        public Category()
        {            
            InitializeComponent();
            dtgvCategory.DataSource = CategoryList;
            loadListCategory();
            addCategoryBinding();
        }
        void loadListCategory()
        {
            CategoryList.DataSource = DanhMuc_DAO.Instance.getDsDanhMuc();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            loadListCategory();
        }
        void addCategoryBinding()
        {
            txId.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txTen.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txTen.Text;
           
            if (DanhMuc_DAO.Instance.insertCategory(name))
            {
                MessageBox.Show("Thêm danh mục món thành công !");
                loadListCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục món !");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txId.Text);
            if (DanhMuc_DAO.Instance.isCategoryInUse(id))
            {
                MessageBox.Show("Danh mục này đang được sử dụng bởi một số món ăn. Vui lòng xóa hoặc cập nhật các món ăn trước khi xóa danh mục.");
                return;
            }
            if (DanhMuc_DAO.Instance.deleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục món thành công !");
                loadListCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục món !");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string name = txTen.Text;
            int id = Convert.ToInt32(txId.Text);
            if (DanhMuc_DAO.Instance.updateCategory(name, id))
            {
                MessageBox.Show("Sửa danh mục món thành công !");
                loadListCategory();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục món !");
            }
        }
    }
}

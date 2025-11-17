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
    public partial class ThucAn : Form
    {
        BindingSource FoodList = new BindingSource();
        public ThucAn()
        {
            InitializeComponent();
            dtgvFood.DataSource = FoodList;
            loadListFood();
            loadCategoryIntoCombobox(cbDanhMuc);
            addFoodBinding();
        }
        List<Food> searchFoodByName (string name)
        {
            List<Food> dsFood = Food_DAO.Instance.searchFoodByName(name);
            return dsFood;
        }
        
        void loadListFood()
        {
            FoodList.DataSource = Food_DAO.Instance.getListFood();
        }
        void loadCategoryIntoCombobox (ComboBox cb)
        {
            cb.DataSource = DanhMuc_DAO.Instance.getDsDanhMuc();
            cb.DisplayMember = "Name";
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            loadListFood();
        }
        void addFoodBinding()
        {
            txId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txTen.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmGia.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Gia", true, DataSourceUpdateMode.Never));
            
        }

        private void txId_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["DanhMucId"].Value;
                DanhMuc dm = DanhMuc_DAO.Instance.getDanhMucById(id);
                cbDanhMuc.SelectedItem = dm;
                int index = -1; int i = 0;
                foreach (DanhMuc item in cbDanhMuc.Items)
                {
                    if (item.Id == dm.Id)
                    {
                        index = i; break;
                    }
                    i++;
                }
                cbDanhMuc.SelectedIndex = index;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txTen.Text;
            int idCategory = (cbDanhMuc.SelectedItem as DanhMuc).Id;
            float price = (float)nmGia.Value;
            if (Food_DAO.Instance.insertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm món thành công !");
                loadListFood();                
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món !");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string name = txTen.Text;
            int idCategory = (cbDanhMuc.SelectedItem as DanhMuc).Id;
            float price = (float)nmGia.Value;
            int id = Convert.ToInt32(txId.Text);
            if (Food_DAO.Instance.updateFood(id, name, idCategory, price))
            {
                MessageBox.Show("Sửa món thành công !");
                loadListFood();                
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món !");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txId.Text);
            if (Food_DAO.Instance.deleteFood(id))
            {
                MessageBox.Show("Xóa món thành công !");
                loadListFood();                
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa món !");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            FoodList.DataSource =  searchFoodByName(txTimKiem.Text);
        }

        
    }
}

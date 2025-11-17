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
    public partial class TaiKhoan : Form
    {
        BindingSource dsAcc = new BindingSource();
        private Account loginAcc;
        public TaiKhoan(Account loginAcc) // Sửa đổi constructor để nhận tài khoản đang đăng nhập
        {
            InitializeComponent();
            this.loginAcc = loginAcc; // Lưu trữ tài khoản đang đăng nhập            
            LoadAcc();
            dtgvAccount.DataSource = dsAcc;
            AddAccountBinding();            
        }
        void AddAccountBinding()
        {
            txUsername.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Tên tài khoản", true, DataSourceUpdateMode.Never));
            txDisplayname.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            txVaitro.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Vai trò", true, DataSourceUpdateMode.Never));
            nmType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Loại TK", true, DataSourceUpdateMode.Never));
        }
        void LoadAcc()
        {            
            dsAcc.DataSource = Account_DAO.Instance.getDsAcc();           
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadAcc();
        }
        void addAccount(string tenTaiKhoan, string tenHienThi, string vaiTro, int loai)
        {
            if (Account_DAO.Instance.insertAccount(tenTaiKhoan, tenHienThi,vaiTro, loai))
            {
                MessageBox.Show("Thêm tài khoản thành công !");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại !");
            }
            LoadAcc();
        }
        void editAccount(string tenTaiKhoan, string tenHienThi,string vaiTro, int loai)
        {
            if (Account_DAO.Instance.updateAccount(tenTaiKhoan, tenHienThi, vaiTro, loai))
            {
                MessageBox.Show("Cập nhật tài khoản thành công !");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại !");
            }
            LoadAcc();
        }
        void deleteccount(string tenTaiKhoan)
        {
            if (Account_DAO.Instance.deleteAccount(tenTaiKhoan))
            {
                MessageBox.Show("Xóa tài khoản thành công !");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại !");
            }
            LoadAcc();
        }
        void resetPassword(string name)
        {
            if (Account_DAO.Instance.resetPass(name))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công !");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại !");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string userName = txUsername.Text;
            string displayName = txDisplayname.Text;
            string vaiTro = txVaitro.Text;
            int type = (int)nmType.Value;
            addAccount(userName, displayName, vaiTro, type);
            string query = "EXEC AddNewAccount " + displayName;
            object[] parameters = new object[] { };

            DataProvider.Instance.ExecuteNonQuery(query, parameters);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string userName = txUsername.Text;
            string displayName = txDisplayname.Text;
            string vaiTro = txVaitro.Text;
            int type = (int)nmType.Value;
            editAccount(userName, displayName, vaiTro, type);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string userName = txUsername.Text;
            // Kiểm tra xem tài khoản đang xóa có phải là tài khoản đăng nhập hay không
            if (userName == loginAcc.TenTaiKhoan)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản đang đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            deleteccount(userName);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string userName = txUsername.Text;
            resetPassword(userName);
        }
    }
}

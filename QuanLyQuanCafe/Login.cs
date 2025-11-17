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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string userName = txUserName.Text;
            string password = txPassword.Text;
            if (login(userName, password))
            {
                DTO.Account loginAcc = Account_DAO.Instance.GetAccountByUserName(userName);
                Form1 f = new Form1(loginAcc);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Nhập sai tên tài khoản hoặc mật khẩu !", "Thông báo");
            }
        }
        bool login(string userName, string passWord)
        {
            return Account_DAO.Instance.login(userName, passWord);
        }
    }
}

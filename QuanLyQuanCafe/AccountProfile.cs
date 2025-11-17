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
    public partial class AccountProfile : Form
    {
        private Account loginAcc;
        public Account LoginAcc
        {
            get { return loginAcc; }
            set { loginAcc = value; changeAcc(loginAcc); }
        }
        public AccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAcc = acc;
        }
        void changeAcc(Account acc)
        {
            txUserName.Text = LoginAcc.TenTaiKhoan;
            txFullName.Text = LoginAcc.TenHienThi;
            txVaiTro.Text = LoginAcc.VaiTro;
            txEmail.Text = LoginAcc.Email;
            txSdt.Text = LoginAcc.DienThoai.ToString();
            
        }
        void updateAcc()
        {
            string userName = txUserName.Text;      string fullName = txFullName.Text;
            string pass = txPass.Text;              string newPass = txNewPass.Text;
            string reEnterPass = txReEnterNew.Text; 
            string email = txEmail.Text;            string phone = txSdt.Text;

            if (!newPass.Equals(reEnterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới !");
            }
            else
            {
                if (Account_DAO.Instance.UpdateAcc(userName, fullName, pass, newPass, email, phone))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateAcc();
        }
    }
}

using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class Form1 : Form
    {        
        bool sidebarExpand;
        bool acc;
        private Form currentFormChild;
        private Account loginAcc;
        public Account LoginAcc
        {
            get { return loginAcc; }
            set { loginAcc = value; changeAcc(loginAcc.LoaiTK); }
        }

        public Form1(Account acc)
        {
            InitializeComponent();
            this.LoginAcc = acc;
            
        }

        void changeAcc (int type)
        {
            btnAdmin.Enabled = type == 1;
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {

            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void AccTimer_Tick(object sender, EventArgs e)
        {
            if (acc)
            {
                accContainer.Height += 10;
                if (accContainer.Height == accContainer.MaximumSize.Height)
                {
                    acc = false;
                    AccTimer.Stop();
                }
            }
            else
            {
                accContainer.Height -= 10;
                if (accContainer.Height == accContainer.MinimumSize.Height)
                {
                    acc = true;
                    AccTimer.Stop();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AccTimer.Start();
        }
        private void openChildForm (Form f)
        {
            if (currentFormChild != null) 
                currentFormChild.Close();
            currentFormChild = f;
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            pn_body.Controls.Add(f);
            pn_body.Tag = f;
            f.BringToFront();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new TableManager());
            txTitle.Text = btnBill.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new AccountProfile(LoginAcc));
            txTitle.Text = btnInfo.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Revenue());
            txTitle.Text = btnThongKe.Text;
        }

        private void admimTmer_Tick(object sender, EventArgs e)
        {
            if (acc)
            {
                adminContainer.Height += 10;
                if (adminContainer.Height == adminContainer.MaximumSize.Height)
                {
                    acc = false;
                    admimTmer.Stop();
                }
            }
            else
            {
                adminContainer.Height -= 10;
                if (adminContainer.Height == adminContainer.MinimumSize.Height)
                {
                    acc = true;
                    admimTmer.Stop();
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            admimTmer.Start();
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            openChildForm(new ThucAn());
            txTitle.Text = btnFood.Text;
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            TaiKhoan acc = new TaiKhoan(LoginAcc); // Truyền loginAcc vào form TaiKhoan
            openChildForm(acc);
            txTitle.Text = btnAcc.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new TrangChu());
            txTitle.Text = button1.Text;
        }

        private void btnCagatory_Click(object sender, EventArgs e)
        {
            openChildForm(new Category());
            txTitle.Text = btnCagatory.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new KhuyenMai()); 
            txTitle.Text = button3.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Ingredient());
            txTitle.Text = button2.Text;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            openChildForm(new TPFood());
            txTitle.Text = button4.Text;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            openChildForm(new QLCaTruc());
            txTitle.Text = button6.Text;
        }

        
    }
}

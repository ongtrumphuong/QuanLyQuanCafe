using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        private string tenTaiKhoan;
        private string tenHienThi;
        private string vaiTro;
        private string email;
        private int dienThoai;        
        private string matKhau;
        private int loaiTK;

        public string TenTaiKhoan
        {
            get { return tenTaiKhoan; }
            set { tenTaiKhoan = value; }
        }
        public string TenHienThi
        {
            get { return tenHienThi; }
            set { tenHienThi = value; }
        }
        public string VaiTro
        {
            get { return vaiTro; }
            set { vaiTro = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int DienThoai
        {
            get { return dienThoai; }
            set { dienThoai = value; }
        }
        
        public string MatKhau
        {
            get { return matKhau; }
            set { matKhau = value; }
        }
        public int LoaiTK
        {
            get { return loaiTK; }
            set { loaiTK = value; }
        }
        public Account(string tenTaiKhoan, string tenHienThi, string vaiTro, string email, int dienThoai, int loaiTK, string matKhau = null)
        {
            this.TenTaiKhoan = tenTaiKhoan;
            this.TenHienThi = tenHienThi;
            this.VaiTro = vaiTro;
            this.Email = email;
            this.DienThoai = dienThoai;            
            this.LoaiTK = loaiTK;
            this.MatKhau = matKhau;
        }
        public Account(DataRow row)
        {
            this.TenTaiKhoan = row["tenTaiKhoan"].ToString();
            this.TenHienThi = row["tenHienThi"].ToString();
            this.VaiTro = row["vaiTro"].ToString();
            this.Email = row["email"].ToString();
            this.DienThoai = (int)row["dienThoai"];            
            this.LoaiTK = (int)row["loaiTK"];
            this.MatKhau = row["matKhau"].ToString();
        }

    }
}

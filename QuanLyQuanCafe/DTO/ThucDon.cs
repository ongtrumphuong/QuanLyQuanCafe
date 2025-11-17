using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class ThucDon
    {
        private string tenMon;
        private int soLuong;
        private float donGia;
        private float thanhTien;
        public string TenMon { get { return tenMon; } set { tenMon = value; } }
        public int SoLuong { get { return soLuong; } set { soLuong = value; } }
        public float DonGia { get { return donGia; } set { donGia = value; } }
        public float ThanhTien { get { return thanhTien; } set { thanhTien = value; } }
        public ThucDon(string tenMon, int soLuong, float donGia, float thanhTien = 0)
        {
            this.TenMon = tenMon;
            this.SoLuong = soLuong;
            this.DonGia = donGia;
            this.ThanhTien = thanhTien;
        }
        public ThucDon(DataRow row)
        {
            this.TenMon = row["Ten"].ToString();
            this.SoLuong = (int)row["soLuong"];
            this.DonGia = (float)Convert.ToDouble(row["Gia"].ToString());
            this.ThanhTien = (float)Convert.ToDouble(row["ThanhTien"].ToString());
        }
    }
}

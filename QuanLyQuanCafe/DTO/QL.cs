using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class QL
    {
        private string tenTaiKhoan;
        private string buoi;
        private bool thu2;
        private bool thu3;
        private bool thu4;
        private bool thu5;
        private bool thu6;
        private bool thu7;
        private bool chuNhat;

        public string TenTaiKhoan { get { return tenTaiKhoan; } set { tenTaiKhoan = value; } }
        public string Buoi { get { return buoi; } set { buoi = value; } }
        public bool Thu2 { get { return thu2; } set { thu2 = value; } }
        public bool Thu3 { get { return thu3; } set { thu3 = value; } }
        public bool Thu4 { get { return thu4; } set { thu4 = value; } }
        public bool Thu5 { get { return thu5; } set { thu5 = value; } }
        public bool Thu6 { get { return thu6; } set { thu6 = value; } }
        public bool Thu7 { get { return thu7; } set { thu7 = value; } }
        public bool ChuNhat { get { return chuNhat; } set { chuNhat = value; } }

        public QL
    (string tenTaiKhoan, string buoi, bool thu2, bool thu3, bool thu4, bool thu5, bool thu6, bool thu7, bool chuNhat)
        {
            this.TenTaiKhoan = tenTaiKhoan;
            this.Buoi = buoi;
            this.Thu2 = thu2;
            this.Thu3 = thu3;
            this.Thu4 = thu4;
            this.Thu5 = thu5;
            this.Thu6 = thu6;
            this.Thu7 = thu7;
            this.ChuNhat = chuNhat;
        }

        public QL(DataRow row)
        {
            this.TenTaiKhoan = row["TenTaiKhoan"].ToString();
            this.Buoi = row["Buoi"].ToString();
            this.Thu2 = (bool)row["Thu2"];
            this.Thu3 = (bool)row["Thu3"];
            this.Thu4 = (bool)row["Thu4"];
            this.Thu5 = (bool)row["Thu5"];
            this.Thu6 = (bool)row["Thu6"];
            this.Thu7 = (bool)row["Thu7"];
            this.ChuNhat = (bool)row["ChuNhat"];
        }

    }
}

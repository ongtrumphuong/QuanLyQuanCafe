using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class NguyenLieu
    {
        private int id;
        private string name;
        private float soLuong;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public float SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public NguyenLieu(int id, string name, float soLuong)
        {
            this.Id = id;
            this.Name = name;
            this.SoLuong = soLuong;
        }
        public NguyenLieu(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.Name = row["Name"].ToString();
            this.SoLuong = (float)Convert.ToDouble(row["SoLuong"].ToString());
        }
    }
}

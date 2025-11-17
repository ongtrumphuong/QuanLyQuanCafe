using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private int id;
        private string name;
        private int danhMucId;
        private float gia;
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int DanhMucId { get { return danhMucId; } set { danhMucId = value; } }
        public float Gia { get { return gia; } set { gia = value; } }
        public Food(int id, string name, int danhMucId, float gia)
        {
            this.Id = id;
            this.Name = name;
            this.DanhMucId = danhMucId;
            this.Gia = gia;
        }
        public Food(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["Ten"].ToString();
            this.DanhMucId = (int)row["idCategory"];
            this.Gia = (float)Convert.ToDouble(row["Gia"].ToString());
        }
    }
}

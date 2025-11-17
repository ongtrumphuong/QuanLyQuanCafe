using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class DanhMuc
    {
        private int id;
        private string name;
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public DanhMuc(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public DanhMuc(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["Ten"].ToString();
        }
    }
}

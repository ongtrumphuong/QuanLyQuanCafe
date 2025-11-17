using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BanAn
    {
        private string ten;
        private string trangThai;
        private int id;
        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }
        public string TrangThai { get { return trangThai; } set { trangThai = value; } }
        public int Id { get { return id; } set { id = value; } }
        public BanAn(int id, string ten, string trangThai)
        {
            this.Id = id;
            this.Ten = ten;
            this.TrangThai = trangThai;
        }
        public BanAn(DataRow hang)
        {
            this.Id = (int)hang["id"];
            this.Ten = hang["ten"].ToString();
            this.TrangThai = hang["trangThai"].ToString();
        }

    }
}

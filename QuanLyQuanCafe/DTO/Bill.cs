using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int trangThai;
        private int giamGia;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public DateTime? DateCheckIn { get { return dateCheckIn; } set { dateCheckIn = value; } }
        public DateTime? DateCheckOut { get { return dateCheckOut; } set { dateCheckOut = value; } }
        public int TrangThai { get { return trangThai; } set { trangThai = value; } }
        public int GiamGia { get { return giamGia; } set { giamGia = value; } }
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int trangThai, int giamGia = 0)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.TrangThai = trangThai;
            this.GiamGia = giamGia;
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];
            var temp = row["dateCheckOut"];
            if (temp.ToString() != "")
                this.DateCheckOut = (DateTime?)temp;
            this.TrangThai = (int)row["trangThai"];
            if(row["GiamGia"].ToString() != "")
                this.GiamGia = (int)row["GiamGia"];
        }
    }
}

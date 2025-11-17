using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class Discount_DAO
    {
        private static Discount_DAO instance;
        public static Discount_DAO Instance
        {
            get { if (instance == null) instance = new Discount_DAO(); return instance; }
            private set { instance = value; }
        }
        public Discount_DAO() { }
        public List<Discount> getActiveDiscount()
        {
            List<Discount> list = new List<Discount>();
            string query = "SELECT * FROM KhuyenMai WHERE TrangThai = N'Còn hiệu lực' AND NgayBatDau <= GETDATE() AND NgayKetThuc >= GETDATE()";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Discount promo = new Discount
                {
                    Id = (int)item["MaKhuyenMai"],
                    NameCode = item["TenKhuyenMai"].ToString(),
                    StartDate = (DateTime)item["NgayBatDau"],
                    EndDate = (DateTime)item["NgayKetThuc"],
                    Active = item["TrangThai"].ToString(),
                    Condition = item["DieuKien"].ToString(),
                    GiaTri = (float)Convert.ToDouble(item["GiaTri"])
                };
                list.Add(promo);
            }
            return list;
        }
        public Discount getDiscountByNameCode(string nameCode)
        {
            string query = $"SELECT * FROM KhuyenMai WHERE TenKhuyenMai = N'{nameCode}' AND TrangThai = N'Còn hiệu lực' AND NgayBatDau <= GETDATE() AND NgayKetThuc >= GETDATE()";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                DataRow item = data.Rows[0];
                return new Discount
                {
                    Id = (int)item["MaKhuyenMai"],
                    NameCode = item["TenKhuyenMai"].ToString(),
                    StartDate = (DateTime)item["NgayBatDau"],
                    EndDate = (DateTime)item["NgayKetThuc"],
                    Active = item["TrangThai"].ToString(),
                    Condition = item["DieuKien"].ToString(),
                    GiaTri = (float)Convert.ToDouble(item["GiaTri"])
                };
            }

            return null;
        }
        public List<Discount> getListDiscount()
        {
            List<Discount> ds = new List<Discount>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from KhuyenMai");
            foreach (DataRow row in dt.Rows)
            {
                Discount discount = new Discount
                {
                    Id = (int)row["MaKhuyenMai"],
                    NameCode = row["TenKhuyenMai"].ToString(),
                    GiaTri = (float)Convert.ToDouble(row["GiaTri"]),
                    Condition = row["DieuKien"].ToString(),
                    StartDate = Convert.ToDateTime(row["NgayBatDau"]),
                    EndDate = Convert.ToDateTime(row["NgayKetThuc"]),
                    Active = row["TrangThai"].ToString()
                };
                ds.Add(discount);
            }
            return ds;
        }
        public bool InsertDiscount(int maKM, string tenKM, float giaTri, string dieuKien, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            string query = $"insert KhuyenMai (MaKhuyenMai, TenKhuyenMai, GiaTri, DieuKien, NgayBatDau, NgayKetThuc, TrangThai) values ({maKM}, N'{tenKM}', {giaTri}, N'{dieuKien}', '{ngayBatDau}', '{ngayKetThuc}', N'{trangThai}')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool updateDiscount(int maKM, string tenKM, float giaTri, string dieuKien, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("update KhuyenMai set TenKhuyenMai = N'{1}' , GiaTri = {2} , DieuKien = N'{3}' , NgayBatDau = '{4}' , NgayKetThuc = '{5}' , TrangThai = N'{6}' where MaKhuyenMai = {0}", maKM, tenKM, giaTri, dieuKien, ngayBatDau, ngayKetThuc, trangThai));
            return kq > 0;
        }
        public List<Discount> SearchDiscount(string name)
        {
            List<Discount> ds = new List<Discount>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(string.Format("select * from KhuyenMai where [dbo].[fuConvertToUnsign1](TenKhuyenMai) like N'%' + [dbo].[fuConvertToUnsign1](N'{0}') + '%'", name));
            foreach (DataRow row in dt.Rows)
            {
                Discount discount = new Discount
                {
                    Id = (int)row["MaKhuyenMai"],
                    NameCode = row["TenKhuyenMai"].ToString(),
                    GiaTri = (float)Convert.ToDouble(row["GiaTri"]),
                    Condition = row["DieuKien"].ToString(),
                    StartDate = Convert.ToDateTime(row["NgayBatDau"]),
                    EndDate = Convert.ToDateTime(row["NgayKetThuc"]),
                    Active = row["TrangThai"].ToString()
                };
                ds.Add(discount);
            }
            return ds;
        }


    }
}

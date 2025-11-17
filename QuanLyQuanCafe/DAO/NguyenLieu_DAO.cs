using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class NguyenLieu_DAO
    {
        private static NguyenLieu_DAO instance;
        public static NguyenLieu_DAO Instance
        {
            get { if (instance == null) instance = new NguyenLieu_DAO(); return NguyenLieu_DAO.instance; }
            private set { NguyenLieu_DAO.instance = value; }
        }
        private NguyenLieu_DAO() { }
        public List<NguyenLieu> getListNguyenLieu()
        {
            List<NguyenLieu> list = new List<NguyenLieu>();
            string query = "SELECT * FROM NguyenLieu";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NguyenLieu nguyenLieu = new NguyenLieu(item);
                list.Add(nguyenLieu);
            }

            return list;
        }
        public List<NguyenLieu> SearchNguyenLieuByName(string name)
        {
            List<NguyenLieu> list = new List<NguyenLieu>();
            string query = string.Format("SELECT * FROM NguyenLieu WHERE dbo.fuConvertToUnsign1(Name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NguyenLieu nguyenLieu = new NguyenLieu(item);
                list.Add(nguyenLieu);
            }

            return list;
        }
        public bool InsertNguyenLieu(string name, float soLuong)
        {
            string query = string.Format("INSERT INTO NguyenLieu (Name, SoLuong) VALUES (N'{0}', {1})", name, soLuong);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateNguyenLieu(int id, string name, float soLuong)
        {
            string query = string.Format("UPDATE NguyenLieu SET Name = N'{0}', SoLuong = {1} WHERE Id = {2}", name, soLuong, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteNguyenLieu(int id)
        {
            string query = string.Format("DELETE FROM NguyenLieu WHERE Id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

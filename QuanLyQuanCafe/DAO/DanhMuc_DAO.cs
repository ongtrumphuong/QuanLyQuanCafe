using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DanhMuc_DAO
    {
        private static DanhMuc_DAO instance;
        public static DanhMuc_DAO Instance
        {
            get { if (instance == null) instance = new DanhMuc_DAO(); return DanhMuc_DAO.instance; }
            private set { DanhMuc_DAO.instance = value; }
        }
        public DanhMuc_DAO() { }
        public List<DanhMuc> getDsDanhMuc()
        {
            List<DanhMuc> ds = new List<DanhMuc>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from FoodCategory");
            foreach (DataRow item in dt.Rows)
            {
                DanhMuc dm = new DanhMuc(item);
                ds.Add(dm);
            }
            return ds;
        }
        public DanhMuc getDanhMucById(int id)
        {
            DanhMuc dm = null;
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from FoodCategory where id = " + id);
            foreach (DataRow item in dt.Rows)
            {
                dm = new DanhMuc(item);
                return dm;
            }

            return dm;
        }
        public bool insertCategory(string name)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("insert FoodCategory (Ten) values (N'{0}')", name));
            return kq > 0;
        }
        public bool updateCategory(string name, int id)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("update FoodCategory set Ten = N'{0}' where id = {1}", name, id));
            return kq > 0;
        }
        public bool deleteCategory(int id)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("delete FoodCategory where id = {0}", id));
            return kq > 0;
        }
        public bool isCategoryInUse(int categoryId)
        {
            string query = "select count(*) from Food where idCategory = @categoryId";
            int result = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { categoryId });
            return result > 0;
        }

    }
}

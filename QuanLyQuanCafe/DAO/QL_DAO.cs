using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class QL_DAO
    {
        private static QL_DAO instance;
        public static QL_DAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new QL_DAO();
                return QL_DAO.instance;
            }
            private set
            {
                QL_DAO.instance = value;
            }
        }
        private QL_DAO() { }

        public DataTable GetQuanLy()
        {
            string query = "SELECT * FROM QuanLy";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public void UpdateQuanLy(string tenTaiKhoan, string buoi, bool thu2, bool thu3, bool thu4, bool thu5, bool thu6, bool thu7, bool chuNhat)
        {
            string query = "UPDATE QuanLy SET Thu2 = @Thu2, Thu3 = @Thu3, Thu4 = @Thu4, Thu5 = @Thu5, Thu6 = @Thu6, Thu7 = @Thu7, ChuNhat = @ChuNhat WHERE TenTaiKhoan = @TenTaiKhoan AND Buoi = @Buoi";
            object[] parameters = new object[] { "@Thu2", thu2, "@Thu3", thu3, "@Thu4", thu4, "@Thu5", thu5, "@Thu6", thu6, "@Thu7", thu7, "@ChuNhat", chuNhat, "@TenTaiKhoan", tenTaiKhoan, "@Buoi", buoi };
            DataProvider.Instance.ExecuteNonQueryQLLam(query, parameters);
        }

    }
}

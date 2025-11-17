using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class ThongTinBill_DAO
    {
        private static ThongTinBill_DAO instance;
        public static ThongTinBill_DAO Instance
        {
            get { if (instance == null) instance = new ThongTinBill_DAO(); return ThongTinBill_DAO.instance; }
            private set { ThongTinBill_DAO.instance = value; }
        }
        private ThongTinBill_DAO() { }
        public void deleteBillIn4ByFoodId (int id)
        {
            DataProvider.Instance.ExecuteQuery("delete BillInfo where idFood = " + id);
        }
        public List<ThongTinBill> GetListBillInfo(int id)
        {
            List<ThongTinBill> listBillInfo = new List<ThongTinBill>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from BillInfo where idBill = " + id);
            foreach (DataRow dr in dt.Rows)
            {
                ThongTinBill in4 = new ThongTinBill(dr);
                listBillInfo.Add(in4);
            }

            return listBillInfo;
        }
        public void insertBillInfo(int idBill, int idFood, int soLuong)
        {
            DataProvider.Instance.ExecuteNonQuery("exec usp_insertBillInfo @idBill , @idFood , @soLuong", new object[] { idBill, idFood, soLuong });
        }
    }
}

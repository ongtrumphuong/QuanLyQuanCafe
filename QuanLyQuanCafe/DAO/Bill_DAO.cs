using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class Bill_DAO
    {
        private static Bill_DAO instance;

        public static Bill_DAO Instance
        {
            get { if (instance == null) instance = new Bill_DAO(); return Bill_DAO.instance; }
            private set { Bill_DAO.instance = value; }
        }
        private Bill_DAO() { }
        public int getUnCheckBillIdByTableId(int id)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from Bill where idTable = " + id + " and TrangThai = 0");
            if (dt.Rows.Count > 0)
            {
                Bill b = new Bill(dt.Rows[0]);
                return b.Id;
            }
            return -1;
        }
        public void insertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec usp_InsertBill @idTable", new object[] { id });
        }
        public int getMaxIdBill()
        {
            return (int)DataProvider.Instance.ExecuteScalar("select max(id) from Bill");
        }
        public void checkOut(int id, int giamGia, float tongTien)
        {
            DataProvider.Instance.ExecuteNonQuery("update Bill set DateCheckOut = GETDATE(), TrangThai = 1, " + "GiamGia = " + giamGia + ", totalPrice = " + tongTien + " where id = " + id);
        }
        public DataTable getBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("exec usp_getListBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
        public DataTable getListNameFood()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Ten as [Tên sản phẩm], SUM(SoLuong) * Gia as [Tổng tiền] FROM BillInfo JOIN Food ON BillInfo.idFood = Food.id GROUP BY Ten, Gia");
        }
        public DataRow getBestSellingProduct()
        {
            string query = "SELECT TOP 1 Ten as [Tên sản phẩm], SUM(SoLuong) * Gia as [Tổng tiền] FROM BillInfo JOIN Food ON BillInfo.idFood = Food.id GROUP BY Ten, Gia ORDER BY SUM(SoLuong) * Gia DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return data.Rows[0];
            }
            return null;
        }
        public DataRow getTotalRevenueForMonth(DateTime month)
        {
            string query = "select SUM(totalPrice) as [Tổng doanh thu] from Bill where MONTH(DateCheckOut) = @month and YEAR(DateCheckOut) = @year";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { month.Month, month.Year });
            if (data.Rows.Count > 0)
            {
                return data.Rows[0];
            }
            return null;
        }
    }
}

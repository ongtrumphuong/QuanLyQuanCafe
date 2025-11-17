using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class ThucDon_DAO
    {
        private static ThucDon_DAO instance;
        public static ThucDon_DAO Instance
        {
            get { if (instance == null) instance = new ThucDon_DAO(); return ThucDon_DAO.instance; }
            private set { ThucDon_DAO.instance = value; }
        }
        private ThucDon_DAO() { }
        public List<ThucDon> getListMenuByTable(int id)
        {
            List<ThucDon> list = new List<ThucDon>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select Ten, SoLuong, Gia, Gia * SoLuong as ThanhTien from Bill b, BillInfo bi, Food f where bi.idBill = b.id and bi.idFood = f.id and b.TrangThai = 0 and b.idTable = " + id);
            foreach (DataRow dr in dt.Rows)
            {
                ThucDon menu = new ThucDon(dr);
                list.Add(menu);
            }
            return list;
        }
    }
}

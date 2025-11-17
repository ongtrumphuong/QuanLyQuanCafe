using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class Manager_DAO
    {
        private static Manager_DAO instance;
        public static Manager_DAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new Manager_DAO();
                return Manager_DAO.instance;
            }
            private set
            {
                Manager_DAO.instance = value;
            }
        }
        public static int TableWidth = 95;
        public static int TableHeight = 95;
        private Manager_DAO() { }
        public void SwitchTable (int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("usp_SwitchTable @idTable1 , @idTable2", new object[] {id1, id2});
        }
        public List<BanAn> LoadTableList()
        {
            List<BanAn> dsBan = new List<BanAn>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("usp_getTableList");
            foreach (DataRow dr in dt.Rows)
            {
                BanAn banan = new BanAn(dr);
                dsBan.Add(banan);
            }
            return dsBan;
        }
    }
}

using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class Food_DAO
    {
        private static Food_DAO instance;
        public static Food_DAO Instance
        {
            get { if (instance == null) instance = new Food_DAO(); return Food_DAO.instance; }
            private set { Food_DAO.instance = value; }
        }
        public Food_DAO() { }
        public List<Food> getFoodByCategoryId(int id)
        {
            List<Food> ds = new List<Food>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from Food where idCategory = " + id);
            foreach (DataRow dr in dt.Rows)
            {
                Food f = new Food(dr);
                ds.Add(f);
            }
            return ds;
        }
        public List<Food> getListFood()
        {
            /* List<Food> ds = new List<Food>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from Food");
            foreach (DataRow dr in dt.Rows)
            {
                Food f = new Food(dr);
                ds.Add(f);
            }
            return ds; */
            string query = @"SELECT f.* FROM Food f WHERE NOT EXISTS (  SELECT 1 FROM TPFood fi
                    INNER JOIN NguyenLieu nl ON fi.idNguyenLieu = nl.id
                    WHERE fi.TPFoodId = f.id AND nl.SoLuong < fi.SoLuong )";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return (from DataRow row in data.Rows select new Food(row)).ToList();
        }
        public List<Food> searchFoodByName (string name)
        {
            List<Food> ds = new List<Food>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(string.Format("select * from Food where [dbo].[fuConvertToUnsign1](Ten) like N'%' + [dbo].[fuConvertToUnsign1](N'{0}') + '%'", name));
            foreach (DataRow dr in dt.Rows)
            {
                Food f = new Food(dr);
                ds.Add(f);
            }
            return ds;
        }
        public bool insertFood(string name, int id, float gia)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("insert Food (Ten, idCategory, Gia) values (N'{0}', {1}, {2})", name, id, gia));
            return kq > 0;
        }
        public bool updateFood(int idFood, string name, int id, float gia)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("update Food set Ten = N'{0}', idCategory = {1}, Gia = {2} where id = {3}", name, id, gia, idFood));
            return kq > 0;
        }
        public bool deleteFood(int idFood)
        {
            ThongTinBill_DAO.Instance.deleteBillIn4ByFoodId(idFood);
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("delete Food where id = {0}", idFood));
            return kq > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class ThongTinBill
    {
        private int id;
        private int billId;
        private int foodId;
        private int count;
        public int Id { get { return id; } set { id = value; } }
        public int BillId { get { return billId; } set { billId = value; } }
        public int FoodId { get { return foodId; } set { foodId = value; } }
        public int Count { get { return count; } set { count = value; } }
        public ThongTinBill(int id, int billId, int foodId, int count)
        {
            this.Id = id;
            this.BillId = billId;
            this.FoodId = foodId;
            this.Count = count;
        }
        public ThongTinBill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.BillId = (int)row["idBill"];
            this.FoodId = (int)row["idFood"];
            this.Count = (int)row["SoLuong"];
        }
    }
}

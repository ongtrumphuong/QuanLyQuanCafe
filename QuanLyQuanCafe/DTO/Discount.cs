using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Discount
    {
        private int id;
        private string nameCode;
        private DateTime startDate;
        private DateTime endDate;
        private string active;
        private string condition;
        private float giaTri;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string NameCode
        {
            get { return nameCode; }
            set { nameCode = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public string Active
        {
            get { return active; }
            set { active = value; }
        }
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public float GiaTri
        {
            get { return giaTri; }
            set { giaTri = value; }
        }
    }
}

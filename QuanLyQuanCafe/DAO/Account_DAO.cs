using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class Account_DAO
    {
        private static Account_DAO instance;

        public static Account_DAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new Account_DAO();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private Account_DAO() { }
        public bool login(string username, string password)
        {
            string query = "usp_login @username , @password";
            DataTable kq = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return kq.Rows.Count > 0;
        }
        public bool UpdateAcc(string userName, string displayName, string pass, string newPass, string newEmail, string newPhone)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery("exec usp_updateAcc @userName , @displayName , @passWord , @newPass , @newEmail , @newPhoneNumber", new object[] { userName, displayName, pass, newPass, newEmail, newPhone });
            return kq > 0;
        }
        public DataTable getDsAcc()
        {
            return DataProvider.Instance.ExecuteQuery("select TenTaiKhoan as [Tên tài khoản], TenHienThi as [Tên hiển thị], VaiTro as [Vai trò], LoaiTK as [Loại TK] from TaiKhoan");
        }
        public Account GetAccountByUserName(string username)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("select * from TaiKhoan where TenTaiKhoan = '" + username + "'");
            foreach (DataRow row in dt.Rows)
            {
                return new Account(row);
            }
            return null;
        }
        public bool insertAccount(string tenTaiKhoan, string tenHienThi, string vaiTro, int loai)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("insert TaiKhoan (TenTaiKhoan, TenHienThi, VaiTro, LoaiTK) values (N'{0}', N'{1}', N'{2}', {3})", tenTaiKhoan, tenHienThi, vaiTro, loai));
            return kq > 0;
        }
        public bool updateAccount(string tenTaiKhoan, string tenHienThi, string vaiTro, int loai)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("update TaiKhoan set TenHienThi = N'{1}', VaiTro = N'{2}', LoaiTK = {3} where TenTaiKhoan = N'{0}'", tenTaiKhoan, tenHienThi, vaiTro, loai));
            return kq > 0;
        }
        public bool deleteAccount(string name)
        {            
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("delete TaiKhoan where TenTaiKhoan = N'{0}'", name));
            return kq > 0;
        }
        public bool resetPass (string name)
        {
            int kq = DataProvider.Instance.ExecuteNonQuery(string.Format("update TaiKhoan set MatKhau = N'0' where TenTaiKhoan = N'{0}'", name));
            return kq > 0;
        }
        public DataRow getEmployeeAndManagerCount()
        {
            string query = "SELECT " +
                           "(SELECT COUNT(*) FROM TaiKhoan WHERE VaiTro = N'Nhân viên') AS EmployeeCount, " +
                           "(SELECT COUNT(*) FROM TaiKhoan WHERE VaiTro = N'Chủ cửa hàng') AS ManagerCount";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return data.Rows[0];
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return DataProvider.instance;
            }
            private set
            {
                DataProvider.instance = value;
            }
        }
        private DataProvider() { }

        private string ketNoiStr = "Data Source=DESKTOP-ESEK6VO;Initial Catalog=QuanLyQuanCafe;Integrated Security=True;Encrypt=False";
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection ketNoi = new SqlConnection(ketNoiStr))         //Sử dụng using để tự giải phóng dữ liệu sau khi dùng xong
            {
                ketNoi.Open();
                SqlCommand command = new SqlCommand(query, ketNoi);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); int i = 0;
                    foreach (string s in listPara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                ketNoi.Close();
            }
            return dt;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int dt = 0;
            using (SqlConnection ketNoi = new SqlConnection(ketNoiStr))         //Sử dụng using để tự giải phóng dữ liệu sau khi dùng xong
            {
                ketNoi.Open();
                SqlCommand command = new SqlCommand(query, ketNoi);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); int i = 0;
                    foreach (string s in listPara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                dt = command.ExecuteNonQuery();
                ketNoi.Close();
            }
            return dt;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object dt = 0;
            using (SqlConnection ketNoi = new SqlConnection(ketNoiStr))         //Sử dụng using để tự giải phóng dữ liệu sau khi dùng xong
            {
                ketNoi.Open();
                SqlCommand command = new SqlCommand(query, ketNoi);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); int i = 0;
                    foreach (string s in listPara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                dt = command.ExecuteScalar();
                ketNoi.Close();
            }
            return dt;
        }
        public int ExecuteNonQueryQLLam(string query, object[] parameter = null)
        {
            int dt = 0;
            using (SqlConnection ketNoi = new SqlConnection(ketNoiStr))         //Sử dụng using để tự giải phóng dữ liệu sau khi dùng xong
            {
                ketNoi.Open();
                SqlCommand command = new SqlCommand(query, ketNoi);
                if (parameter != null)
                {
                    for (int i = 0; i < parameter.Length; i += 2)
                    {
                        command.Parameters.AddWithValue(parameter[i].ToString(), parameter[i + 1]);
                    }
                }
                dt = command.ExecuteNonQuery();
                ketNoi.Close();
            }
            return dt;
        }
    }
}

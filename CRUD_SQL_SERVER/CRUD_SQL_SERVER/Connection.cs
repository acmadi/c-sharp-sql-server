using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace CRUD_SQL_SERVER
{
    class Connection
    {
        public Connection()
        {
            koneksi = new SqlConnection(conn_str);
        }

        private string conn_str = "server=MADARA-LAPTOP\\SQLEXPRESS; user id=madara; password=admin; database=kampus;";
        private SqlConnection koneksi;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataSet ds;
        private int res;
        private string sql;

        /* using singleton */
        private static Connection obj = null;
        public static Connection GetInstance()
        {
            if(obj == null){
                obj = new Connection();
            }
            return obj;
        }
        /* end singleton */

        private DataSet GetData(string sql)
        {
            ds = new DataSet();
            koneksi.Open();
            cmd = new SqlCommand(sql, koneksi);
            adapter = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            adapter.Fill(ds);
            koneksi.Close();
            return ds;
        }

        private int ManipulasiData(string sql)
        {
            res = 0;
            koneksi.Open();
            cmd = new SqlCommand(sql,koneksi);
            adapter = new SqlDataAdapter(cmd);
            res = cmd.ExecuteNonQuery();
            koneksi.Close();
            return res;
        }

        public DataSet GetMahasiswa()
        {
            sql = "select * from mahasiswa";
            return GetData(sql);
        }

        public DataSet GetMahasiswa(string where)
        {
            sql = "select * from mahasiswa " + where;
            return GetData(sql);
        }

        public int InsertMahasiswa(string nim, string nama, string alamat)
        {
            sql = "insert into mahasiswa values ('"+nim+"','"+nama+"','"+alamat+"');";
            return ManipulasiData(sql);
        }

        public int UpdateMahasiswa(string nim, string nama, string alamat)
        {
            sql = "update mahasiswa set nama = '"+nama+"', alamat = '"+alamat+"' where nim = '"+nim+"';";
            return ManipulasiData(sql);
        }

        public int DeleteMahasiswa(string nim)
        {
            sql = "delete from mahasiswa where nim = '"+nim+"'";
            return ManipulasiData(sql);
        }
    }
}

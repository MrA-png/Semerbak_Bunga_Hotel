using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    class Koneksi
    {
        static String query = "Data Source=DESKTOP-I31864H;Initial Catalog=MODUL1_SQL_10_SMKN1CIREBON;Integrated Security=True";
        SqlConnection koneksi = new SqlConnection(query);
        public SqlCommand cmd;
        public SqlDataAdapter da = new SqlDataAdapter();
        public SqlDataReader dr;
        public DataSet ds = new DataSet();
        public  DataTable dt = new DataTable();
        public void select(String query)
        {
            try
            {
                dt.Clear();
                koneksi.Open();
                cmd = new SqlCommand(query, koneksi);
                da.SelectCommand = cmd;
                da.Fill(dt);
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
        public void cud(String query)
        {
            try
            {
                koneksi.Open();
                cmd = new SqlCommand(query, koneksi);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
    }
}

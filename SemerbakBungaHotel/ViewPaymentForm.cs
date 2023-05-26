using System;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ViewPaymentForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable();
        String ID;
        public ViewPaymentForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            load_data();
        }

        private void load_data()
        {
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select pb.IDPembayaran,pb.TglPembayaran,pm.IDPemesanan,ph.NamaPenghuni,km.NomorKamar,kr.NamaKaryawan,pb.TotalPembayaran,pm.Status from Pembayaran pb "
                +"join Pemesanan pm on pm.IDPemesanan=pb.IDPemesanan "
                +"join Penghuni ph on ph.IDPenghuni = pm.IDPenghuni "
                +"join Kamar km on km.IDKamar = pm.IDKamar "
                +"join Karyawan kr on kr.IDKaryawan = pb.IDKaryawan"
                +" where pb.IDPembayaran like '%" + txtSearch.Text + "%' or pb.TglPembayaran like '%" + txtSearch.Text + "%' or pm.IDPemesanan like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow dtr in dt.Rows)
            {
                string[] row = new string[]
                {
                    dtr[0].ToString(),
                    dtr[1].ToString(),
                    dtr[2].ToString(),
                    dtr[3].ToString(),
                    dtr[4].ToString(),
                    dtr[5].ToString(),
                    dtr[6].ToString(),
                    dtr[7].ToString()
                };
                dataGridView1.Rows.Add(row);
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }
    }
}

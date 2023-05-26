using System;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageBookingForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable();
        String ID;
        public ManageBookingForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            load_data();
        }

        private void load_data()
        {
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select Pemesanan.IDPemesanan,Pemesanan.TglPemesanan,Pemesanan.WaktuMasukHotel"
                + ",Penghuni.NamaPenghuni,Penghuni.NomorHP,Kamar.NomorKamar,Pemesanan.Status from Pemesanan inner join Penghuni on Pemesanan.IDPenghuni=Penghuni.IDPenghuni inner join Kamar on Pemesanan.IDKamar=Kamar.IDKamar where Pemesanan.IDPemesanan like '%" + txtSearch.Text + "%' or"
                + " Penghuni.NamaPenghuni like '%" + txtSearch.Text + "%' or Kamar.NomorKamar like '%" + txtSearch.Text + "%'");
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
                    dtr[6].ToString()
                };
                dataGridView1.Rows.Add(row);
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Action")
            {
                koneksi.cud("update Pemesanan Set Status = 'C' where IDPemesanan = '"+row.Cells[0].Value.ToString()+"'");
            }
            load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void ManageBookingForm_Load(object sender, EventArgs e)
        {

        }
    }
}

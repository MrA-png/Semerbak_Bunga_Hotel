using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class TransactionReport : Form
    {
        String ID,idtype,nmtype;
        Koneksi koneksi = new Koneksi();
        DataTable dts = new DataTable(),dt = new DataTable();
        Random data = new Random();
        public TransactionReport(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            type();
            cbRoom.Text = "All";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void type()
        {
            
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeKamar");
            koneksi.da.Fill(dts);
            foreach(DataRow dtr in dts.Rows)
            {
                cs.Add(dtr["IDTipeKamar"].ToString(), dtr["NamaKamar"].ToString());
            }
            
            cbRoom.DataSource = new BindingSource(cs, null);
            cbRoom.DisplayMember = "Value";
            cbRoom.ValueMember = "Key";
            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void TransactionReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.Transaksi' table. You can move, or remove it, as needed.
            refreshreport();
        }

        private void reportViewer1_Load_2(object sender, EventArgs e)
        {

        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            idtype = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
            nmtype = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Value;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            refreshreport();
        }
        private void refreshreport()
        {
            try
            {
                String tipe = "";
                if (!nmtype.Equals("All"))
                {
                    tipe = " and Kamar.IDTipeKamar = " + idtype + "";
                }

                koneksi.select("select Pembayaran.TglPembayaran,Pembayaran.TotalPembayaran,Kamar.IDTipeKamar," +
                    "(Case when Pemesanan.IDHargaFluktuatif is null then 'Normal' else 'Fluktuatif' end) as tipe_harga " +
                    "from Pembayaran join Pemesanan on Pemesanan.IDPemesanan = Pembayaran.IDPemesanan " +
                    "join Kamar on Kamar.IDKamar = Pemesanan.IDKamar "
                    + "where (Pembayaran.TglPembayaran between '" + dtp1.Value.ToString("yyyy/MM/dd") + "' and '" + dtp2.Value.ToString("yyyy/MM/dd") + "')" + tipe);
                this.DataSet1.Clear();
                koneksi.da.Fill(this.DataSet1.Transaksi);
                ReportParameterCollection rp = new ReportParameterCollection();
                rp.Add(new ReportParameter("ReportParameter1", nmtype));
                this.reportViewer1.LocalReport.SetParameters(rp);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

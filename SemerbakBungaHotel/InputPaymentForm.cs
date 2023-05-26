using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class InputPaymentForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dts = new DataTable();
        String idbooking,ID,total;

        public InputPaymentForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            booking();
        }
        private void jumlah()
        {
            koneksi.select("select*from Pemesanan left join HargaFluktuatif on Pemesanan.IDHargaFluktuatif=HargaFluktuatif.IDHargaFluktuatif where IDPemesanan ='"+idbooking+"'");
            if (koneksi.dt.Rows[0]["IDHargaFluktuatif"].ToString() != "")
            {
                string hpm = koneksi.dt.Rows[0]["HargaPerMalam"].ToString();
                string lt = koneksi.dt.Rows[0]["LamaTinggal"].ToString();
                string hf = koneksi.dt.Rows[0]["Persentase"].ToString();
                int hargapermalam = int.Parse(hpm) * int.Parse(lt);
                int hargafluktuatif = hargapermalam - (hargapermalam * int.Parse(hf) / 100);
                koneksi.select("select sum(HargaSatuan) as hs from PemesananDetail where IDPemesanan = '" + idbooking + "'");
                string hs = koneksi.dt.Rows[0]["hs"].ToString();
                if (hs != "")
                {
                    int ttl = hargafluktuatif + int.Parse(hs);
                    total = ttl.ToString();
                }
                else
                {
                    int ttl = hargafluktuatif;
                    total = ttl.ToString();
                }
            }
            else
            {
                string hpm = koneksi.dt.Rows[0]["HargaPerMalam"].ToString();
                string lt = koneksi.dt.Rows[0]["LamaTinggal"].ToString();
                int hargapermalam = int.Parse(hpm) * int.Parse(lt);
                koneksi.select("select sum(HargaSatuan) as hs from PemesananDetail where IDPemesanan = '" + idbooking + "'");
                string hs = koneksi.dt.Rows[0]["hs"].ToString();
                if (hs != "")
                {
                    int ttl = hargapermalam + int.Parse(hs);
                    total = ttl.ToString();
                }
                else
                {
                    int ttl = hargapermalam;
                    total = ttl.ToString();
                }
            }
            
        }
        private void booking()
        {
            try
            {
                Dictionary<String, String> cs = new Dictionary<string, string>();
                koneksi.select("select*from Pemesanan where Status = 'B'");
                koneksi.da.Fill(dts);
                foreach (DataRow dtr in dts.Rows)
                {
                    cs.Add(dtr["IDPemesanan"].ToString(), dtr["IDPemesanan"].ToString());
                }
                cbBooking.DataSource = new BindingSource(cs, null);
                cbBooking.DisplayMember = "value";
                cbBooking.ValueMember = "Key";
            }
            catch (Exception)
            {
                MessageBox.Show("Payment is Null");
            }
            
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cbBooking.Text.Equals("") || txtPayment.Text.Equals("") || dateTimePicker1.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("insert into Pembayaran(tglPembayaran,IDPemesanan,IDKaryawan,TotalPembayaran) values('" + dateTimePicker1.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','" + idbooking + "','" + ID + "','" + txtRemaining.Text + "')");
                koneksi.cud("update Pemesanan set Status = 'I' where IDPemesanan =" + idbooking);
                MessageBox.Show("Payment Success");
                cbBooking.Text = "";
                txtBalance.Text = "";
                txtPayment.Text = "";
                txtRemaining.Text = "";
            }
            MessageBox.Show("Payment Success");
            booking();
        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            int bayar;
            if(txtPayment.Text != "")
            {
                bayar = int.Parse(txtPayment.Text) - int.Parse(txtRemaining.Text);
                txtBalance.Text = bayar.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void cbBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                idbooking = ((KeyValuePair<String, String>)cbBooking.SelectedItem).Key;
                jumlah();
                txtRemaining.Text = total;
            }
            catch (Exception)
            {
                MessageBox.Show("Payment is Null");
            }
            
        }
    }
}

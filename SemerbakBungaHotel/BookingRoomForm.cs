using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class BookingRoomForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DateTime jamku;
        DataTable dts = new DataTable(), dts1 = new DataTable(),dts2 = new DataTable(),dts3=new DataTable(),dts4 = new DataTable(), dts5 = new DataTable();
        String roomid, ID, bookingid, facilityid, guestid,price,harga;
        int ttl, Price;
        String persentase = "0",idfh = "NULL";
        public BookingRoomForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            tipe();
            facility();
            guest();
            booking();
            fluktuatif();
            txtTax.Text = "500000";
            txtAdditional.Text = "0";
            txtPrice.Text = "0";
            txtTotal.Text = "0";
        }

        private void tipe()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeKamar");
            koneksi.da.Fill(dts);
            foreach (DataRow dtr in dts.Rows)
            {
                cs.Add(dtr["IDTipeKamar"].ToString(), dtr["NamaKamar"].ToString());
            }
            cbRoom.DataSource = new BindingSource(cs, null);
            cbRoom.DisplayMember = "Value";
            cbRoom.ValueMember = "Key";
        }
        private void floor()
        {
            flowLayoutPanel1.Controls.Clear();
            cbFloor.Text = "";
            dts1.Clear();
            cbFloor.Items.Clear();
            koneksi.select("select distinct Lantai from Kamar WHERE IDTipeKamar = '"+roomid+"'");
            koneksi.da.Fill(dts1);
            foreach (DataRow dtr in dts1.Rows)
            {
                cbFloor.Items.Add(dtr["Lantai"].ToString());
            }
        }

        private void cbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            facilityid = ((KeyValuePair<String, String>)cbFacility.SelectedItem).Key;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                String ttl1 = row.Cells[2].Value.ToString();

                ttl = int.Parse(txtAdditional.Text) - int.Parse(ttl1);
                txtAdditional.Text = ttl.ToString();
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Cancel")
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    dataGridView1.Refresh();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cbRoom.Text = "";
            cbFloor.Text = "";
            txtGuest.Text = "";
            cbBooking.Text = "";
            cbName.Text = "";
            txtNumber.Text = "";
            txtStay.Text = "";
            txtTotal.Text = "0";
            txtPrice.Text = "0";
            txtAdditional.Text = "0";
            dataGridView1.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
        }
        private void total()
        {
            if(txtTax.Text != ""||txtPrice.Text != "")
            {
                int total = 0;
                total = int.Parse(txtAdditional.Text) + int.Parse(txtPrice.Text) + int.Parse(txtTax.Text);
                txtTotal.Text = total.ToString();
            }
        }
        private void total_harga()
        {
            if (txtStay.Text != "")
            {
                if (idfh != "NULL")
                {
                    int hargakamar;
                    koneksi.select("select*from HargaKamar where IDTipekamar = '" + roomid + "'");
                    harga = koneksi.dt.Rows[0]["HargaKamar"].ToString();
                    int hargaflktuatif = int.Parse(harga) * int.Parse(txtStay.Text) * int.Parse(persentase) / 100;
                    hargakamar = int.Parse(harga) * int.Parse(txtStay.Text) - hargaflktuatif;
                    txtPrice.Text = hargakamar.ToString();
                    total();
                }
                else
                {
                    int hargakamar;
                    koneksi.select("select*from HargaKamar where IDTipekamar = '" + roomid + "'");
                    harga = koneksi.dt.Rows[0]["HargaKamar"].ToString();
                    hargakamar = int.Parse(harga) * int.Parse(txtStay.Text);
                    txtPrice.Text = hargakamar.ToString();
                    total();
                }                    
            }
        }
        private void txtStay_TextChanged(object sender, EventArgs e)
        {
            total_harga();
        }

        private void cbFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            koneksi.select("select max(Kamar.IDKamar) as  IDKamar, max(PerbaikanKamar.IDPerbaikanKamar) as IDPerbaikan, max(Kamar.NomorKamar) as NoKamar, "
                + "max(Pemesanan.Status) as Status from Kamar left join  PerbaikanKamar on (Kamar.IDKamar=PerbaikanKamar.IDKamar and ('" + dtpCheck.Value.ToString("yyyy/MM/dd") + "' between TglMulai and TglSelesai))" +
                "left join Pemesanan on (Kamar.IDKamar = Pemesanan.IDKamar and ('"+dtpCheck.Value.ToString("yyyy/MM/dd")+"' between WaktuMasukHotel and DATEADD(day,LamaTinggal,WaktuMasukHotel)))where Lantai = '" + cbFloor.Text+"' and IDTipeKamar = '"+roomid+"' group by Kamar.IDKamar");
            if(koneksi.dt.Rows.Count != 0)
            {
                for(int i = 0; i < koneksi.dt.Rows.Count; i++)
                {
                    string nokamar = koneksi.dt.Rows[i]["NoKamar"].ToString();
                    string idkamar = koneksi.dt.Rows[i]["IDKamar"].ToString();
                    string status = koneksi.dt.Rows[i]["Status"].ToString();
                    string idperbaikan = koneksi.dt.Rows[i]["IDPerbaikan"].ToString();
                    Button b = new Button();
                    b.Name = "ID"+idkamar;
                    b.Text = nokamar;
                    if(idperbaikan != "")
                    {
                        b.ForeColor = Color.Maroon;
                        b.BackColor = Color.White;
                    }
                    else if(status.Equals("B") || status.Equals("I"))
                    {
                        b.ForeColor = Color.White;
                        b.BackColor = Color.Maroon;
                    }else if(status.Equals(""))
                    {
                        b.ForeColor = Color.White;
                        b.BackColor = Color.Turquoise;
                    }

                    b.Click += new System.EventHandler(RoomClick);
                    flowLayoutPanel1.Controls.Add(b);

                }
            }
        }
        void RoomClick(object s, EventArgs e)
        {
            Button thisBtn = (Button)s;
            if(thisBtn.BackColor == Color.Turquoise)
            {
                MessageBox.Show("Kamar" + thisBtn.Text + "Availabel");
                txtNumber.Text = thisBtn.Text;
                total_harga();
            }
            else if (thisBtn.BackColor == Color.Maroon)
            {
                MessageBox.Show("Kamar " + thisBtn.Text + " Occupied");
            }
            else
            {
                MessageBox.Show("Kamar " + thisBtn.Text + " In Repaired");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            jamku = DateTime.Now;
            txtJam.Text = jamku.Hour.ToString();
            txtMenit.Text = jamku.Minute.ToString();
        }

        private void BookingRoomForm_Load(object sender, EventArgs e)
        {
            jamku = DateTime.Now;
            timer1.Enabled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            floor();
        }

        private void txtStay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Use Number");
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Use Number");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(txtNumber.Text.Equals("") || txtStay.Text.Equals("") || cbBooking.Text.Equals("") || cbName.Text.Equals(""))
            {
                MessageBox.Show("Booking Failed");
            }
            else
            {
                String kamar;
                koneksi.select("select*from Kamar where NomorKamar = '" + txtNumber.Text + "'");
                kamar = koneksi.dt.Rows[0]["IDKamar"].ToString();
                String date = DateTime.Now.ToString("yyyy/MM/dd");
                koneksi.cud("insert into Pemesanan(IDJenisPemesanan,TglPemesanan,WaktuMasukHotel,IDPenghuni,IDKamar,HargaPerMalam,IDHargaFluktuatif,LamaTinggal,Status,IDKaryawan) values('" + bookingid
                    + "','" + date
                    + "','" + dateTimePicker2.Value.ToString("yyyy/MM/dd HH:mm:ss")
                    + "','" + guestid
                    + "','" + kamar
                    + "','" + harga
                    + "'," + idfh
                    + ",'" + txtStay.Text
                    + "','B','" + ID
                    + "')");
                koneksi.select("select*from Kamar join Pemesanan on Pemesanan.IDKamar = Kamar.IDKamar where Kamar.NomorKamar='" + txtNumber.Text + "' and Pemesanan.IDJenisPemesanan = '" + bookingid + "' and Pemesanan.TglPemesanan = '" + date + "'");
                if (koneksi.dt.Rows.Count != 0)
                {
                    String idpemesanan = koneksi.dt.Rows[0]["IDPemesanan"].ToString();
                    if (dataGridView1.Rows.Count != 0)
                    {
                        for (int x = 0; x <= dataGridView1.Rows.Count-1; x++)
                        {

                             string idfasilitas = dataGridView1.Rows[x].Cells[4].Value.ToString();
                            int qty = int.Parse(dataGridView1.Rows[x].Cells[3].Value.ToString());
                            koneksi.select("select*from FasilitasTambahan where IDFasilitasTambahan = '" + idfasilitas + "'");
                            int hf = int.Parse(koneksi.dt.Rows[0]["HargaFasilitasTambahan"].ToString());
                            int harga12 = hf * qty;
                            string hs = harga12.ToString();
                            koneksi.cud("insert into PemesananDetail(IDPemesanan,IDFasilitasTambahan,JumlahFasilitasTambahan,HargaSatuan) values('" + idpemesanan + "','" + idfasilitas + "','" + qty.ToString() + "','" + hs + "')");
                        }
                    }
                }
                MessageBox.Show("Booking Success");
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int no = 1;
            koneksi.select("select*from FasilitasTambahan");
            price = koneksi.dt.Rows[0]["HargaFasilitasTambahan"].ToString();
            if (dataGridView1.Rows.Count != 0)
            {
                for (int x = 0; x < dataGridView1.Rows.Count-1; x++)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[x];
                    if (cbFacility.Text == row.Cells[1].Value.ToString())
                    {
                        ttl = int.Parse(txtAdditional.Text) - int.Parse(dataGridView1.Rows[x].Cells[2].Value.ToString());
                        txtAdditional.Text = ttl.ToString();
                        dataGridView1.Rows.Remove(dataGridView1.Rows[x]);
                    }
                }
            }
            Price = int.Parse(price) * int.Parse(txtQty.Text);
            string[] rows = new string[]
            {
                no.ToString(),
                cbFacility.Text,
                Price.ToString(),
                txtQty.Text,
                facilityid,
            };
            dataGridView1.Rows.Add(rows);
            ttl = int.Parse(txtAdditional.Text) + Price;
            txtAdditional.Text = ttl.ToString();
            no++;
            txtQty.Text = "";
            total();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            fluktuatif();
            total_harga();
        }

        private void facility()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from FasilitasTambahan");
            koneksi.da.Fill(dts2);
            foreach (DataRow dtr in dts2.Rows)
            {
                cs.Add(dtr["IDFasilitasTambahan"].ToString(), dtr["NamaFasilitasTambahan"].ToString());
            }
            cbFacility.DataSource = new BindingSource(cs, null);
            cbFacility.DisplayMember = "Value";
            cbFacility.ValueMember = "Key";
        }
        private void booking()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from JenisPemesanan");
            koneksi.da.Fill(dts3);
            foreach (DataRow dtr in dts3.Rows)
            {
                cs.Add(dtr["IDJenisPemesanan"].ToString(), dtr["NamaJenisPemesanan"].ToString());
            }
            cbBooking.DataSource = new BindingSource(cs, null);
            cbBooking.DisplayMember = "Value";
            cbBooking.ValueMember = "Key";
        }

        private void cbBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            bookingid = ((KeyValuePair<String, String>)cbBooking.SelectedItem).Key;
        }

        private void guest()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from Penghuni");
            koneksi.da.Fill(dts4);
            foreach (DataRow dtr in dts4.Rows)
            {
                String guest = dtr["NamaPenghuni"].ToString() + "-" + dtr["Email"].ToString();
                cs.Add(dtr["IDPenghuni"].ToString(), guest);
            }
            cbName.DataSource = new BindingSource(cs, null);
            cbName.DisplayMember = "Value";
            cbName.ValueMember = "Key";
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewGuest fr = new AddNewGuest(ID);
            fr.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            guestid = ((KeyValuePair<String, String>)cbName.SelectedItem).Key;
            txtGuest.Text = guestid;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void fluktuatif()
        {
            koneksi.select("select*from HargaFluktuatif where TglMulai <= '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "' and TglSelesai >= '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "' and IDTipeKamar =" + roomid);
            if (koneksi.dt.Rows.Count != 0)
            {
                idfh = koneksi.dt.Rows[0]["IDHargaFluktuatif"].ToString();
                persentase = koneksi.dt.Rows[0]["Persentase"].ToString();
            }
            else
            {
                idfh = "NULL";
                persentase = "NULL";
            }
        }
        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomid = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
            fluktuatif();
        }
    }
}

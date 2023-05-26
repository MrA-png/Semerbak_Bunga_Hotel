using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageDailyPriceForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(), dts = new DataTable(), dts1 = new DataTable();
        String idRoom, idDay,ID;

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRoom = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
        }

        private void room()
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
        private void day()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeHari");
            koneksi.da.Fill(dts1);
            foreach (DataRow dtr in dts1.Rows)
            {
                cs.Add(dtr["IDTipeHari"].ToString(), dtr["NamaTipeHari"].ToString());
            }
            cbDay.DataSource = new BindingSource(cs, null);
            cbDay.DisplayMember = "Value";
            cbDay.ValueMember = "Key";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            idDay = ((KeyValuePair<String, String>)cbDay.SelectedItem).Key;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                string hargaID = row.Cells[0].Value.ToString();

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    koneksi.cud("delete from HargaKamar where IDHargaKamar = '" + hargaID + "'");
                    MessageBox.Show("Delete Success");
                    dgView();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbRoom.Text.Equals("") || cbDay.Text.Equals("") || txtPrice.Text.Equals("") || txtRoom.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("Insert into HargaKamar(IDTipeKamar,IDTipeHari,HargaKamar,HargaSarapan) values('" + idRoom
                    + "','" + idDay
                    + "','" + txtRoom.Text
                    + "','" + txtPrice.Text
                    + "')");

                MessageBox.Show("Add Success");
                dgView();
            }
        }

        public ManageDailyPriceForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            room();
            day();
            dgView();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgView();
        }
        private void dgView()
        {
            int i = 1;
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select TipeKamar.NamaKamar,TipeHari.NamaTipeHari,HargaKamar.HargaKamar,HargaKamar.HargaSarapan,HargaKamar.IDHargaKamar from HargaKamar inner join TipeKamar on HargaKamar.IDTipeKamar=TipeKamar.IDTipeKamar inner join TipeHari on HargaKamar.IDTipeHari=TipeHari.IDTipeHari where TipeKamar.NamaKamar like '%" + txtSearch.Text + "%' or HargaKamar.IDHargaKamar like '%" + txtSearch.Text + "%' or TipeHari.NamaTipeHari like '%"
                + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach(DataRow dtr in dt.Rows)
            {
                string[] row = new string[]
                {
                    i.ToString(),
                    dtr[0].ToString(),
                    dtr[1].ToString(),
                    dtr[2].ToString(),
                    dtr[3].ToString(),
                    dtr[4].ToString()
                };
                dataGridView1.Rows.Add(row);
                i++;
            }
                

            }
        }
    }


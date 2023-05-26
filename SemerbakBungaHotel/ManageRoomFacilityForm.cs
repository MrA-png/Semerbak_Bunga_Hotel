using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageRoomFacilityForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(), dts = new DataTable(), dts1 = new DataTable();
        String idType, nmType, idfasility, ID;
        public ManageRoomFacilityForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            type();
            fasilitas();
            load_data();
        }

        private void type()
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
        private void fasilitas()
        {
            Dictionary<String, String> cs1 = new Dictionary<string, string>();
            koneksi.select("select*from Fasilitas");
            koneksi.da.Fill(dts1);
            foreach (DataRow dtr1 in dts1.Rows)
            {
                cs1.Add(dtr1["IDFasilitas"].ToString(), dtr1["NamaFasilitas"].ToString());
            }
            cbFacility.DataSource = new BindingSource(cs1, null);
            cbFacility.DisplayMember = "Value";
            cbFacility.ValueMember = "Key";
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            idType = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
            nmType = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Value;
        }

        private void cbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            idfasility = ((KeyValuePair<String, String>)cbFacility.SelectedItem).Key;
        }

        private void load_data()
        {
            int i = 1;
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select TipeKamar.NamaKamar,Fasilitas.NamaFasilitas,FasilitasBerdasarkanTipeKamar.JumlahFasilitas from FasilitasBerdasarkanTipeKamar join Fasilitas on FasilitasBerdasarkanTipeKamar.IDFasilitas=Fasilitas.IDFasilitas join TipeKamar on FasilitasBerdasarkanTipeKamar.IDTipeKamar = TipeKamar.IDTipeKamar where TipeKamar.NamaKamar like '%" + txtSearch.Text + "%' or Fasilitas.NamaFasilitas like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow dtr in dt.Rows)
            {
                string[] row = new string[]
                {
                    i.ToString(),
                    dtr[0].ToString(),
                    dtr[1].ToString(),
                    dtr[2].ToString(),
                };
                dataGridView1.Rows.Add(row);
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String kamar="", id = "";
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                koneksi.select("select*from Fasilitas where NamaFasilitas = '" + row.Cells[4].Value.ToString() + "'");
                id = koneksi.dt.Rows[0]["IDFasilitas"].ToString();
                kamar = row.Cells[2].Value.ToString();
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    koneksi.cud("delete from FasilitasBerdasarkanTipeKamar where IDFasilitas = '" + id + "' and IDKamar = '" + kamar + "'");
                    MessageBox.Show("Delete Success");
                    load_data();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbRoom.Text.Equals("") || cbFacility.Text.Equals("") || jumlah.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                    {
                        DataGridViewRow row = this.dataGridView1.Rows[x];
                        if (cbFacility.Text == row.Cells[2].Value.ToString() && cbRoom.Text == row.Cells[1].Value.ToString())
                        {
                            koneksi.cud("update FasilitasBerdasarkanTipeKamar set JumlahFasilitas ='" + jumlah.Text + "' where  IDFasilitas ='" + idfasility + "' and IDTipeKamar = '" + idType + "'");
                            MessageBox.Show("Add Success");
                        }
                        else
                        {
                            koneksi.cud("insert Into FasilitasBerdasarkanTipeKamar (IDFasilitas,IDTipeKamar,JumlahFasilitas) values('" + idfasility
                            + "','" + idType
                            + "','" + jumlah.Text + "')");
                            MessageBox.Show("Add Success");
                        }
                    }
                }
                load_data();
            }
        }
    }
    }



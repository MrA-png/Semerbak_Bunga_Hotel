using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageRoomRepirmentForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(), dts = new DataTable();
        String idRoom, nmRoom,ID;

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRoom = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
            nmRoom = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Value;
        }

        private void room()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from Kamar");
            koneksi.da.Fill(dts);
            foreach(DataRow dtr in dts.Rows)
            {
                cs.Add(dtr["IDKamar"].ToString(), dtr["NomorKamar"].ToString());
            }
            cbRoom.DataSource = new BindingSource(cs, null);
            cbRoom.DisplayMember = "Value";
            cbRoom.ValueMember = "Key";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbRoom.Text.Equals("") || dtpAkhir.Text.Equals("") || dtpAwal.Equals("") || txtNote.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("Insert into PerbaikanKamar(IDKamar,TglMulai,TglSelesai,Catatan,IDKaryawan) values('" + idRoom
                    + "','" + dtpAwal.Value.ToString("yyyy/MM/dd")
                    + "','" + dtpAkhir.Value.ToString("yyyy/MM/dd")
                    + "','" + txtNote.Text
                    + "','" + ID
                    + "')");

                MessageBox.Show("Add Success");
                dgView();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Finish")
            {
                koneksi.cud("delete from PerbaikanKamar where IDPerbaikanKamar = '"+row.Cells[8].Value.ToString()+"'");
                MessageBox.Show("Success");
                dgView();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        public ManageRoomRepirmentForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            room();
            dgView();
        }
        private void dgView()
        {
            int i = 1;
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select TipeKamar.NamaKamar,Kamar.NomorKamar,Kamar.Lantai,"
                + "PerbaikanKamar.TglMulai,PerbaikanKamar.TglSelesai,Karyawan.IDKaryawan,PerbaikanKamar.Catatan,PerbaikanKamar.IDPerbaikanKamar "
                + "from PerbaikanKamar inner join Kamar on PerbaikanKamar.IDKamar=Kamar.IDKamar inner join TipeKamar on Kamar.IDTipeKamar=TipeKamar.IDTipeKamar inner join Karyawan on PerbaikanKamar.IDKaryawan=Karyawan.IDKaryawan where "
                + "TipeKamar.NamaKamar like '%" + txtSearch.Text + "%' or Kamar.Lantai like '%" + txtSearch.Text + "%' or Kamar.NomorKamar like '%" + txtSearch.Text + "%' or TipeKamar.IDTipeKamar like '%"
                + txtSearch.Text + "%'");
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
                    dtr[3].ToString(),
                    dtr[4].ToString(),
                    dtr[5].ToString(),
                    dtr[6].ToString(),
                    dtr[7].ToString()
                };
                dataGridView1.Rows.Add(row);
                i++;
            }

        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgView();    
        }
    }
}

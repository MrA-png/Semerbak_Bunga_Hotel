using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageHolidayPriceForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(), dts = new DataTable();
        String idRoom, nmRoom,ID;
        int final;

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRoom = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Key;
            nmRoom = ((KeyValuePair<String, String>)cbRoom.SelectedItem).Value;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            String hargaID = row.Cells[0].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                koneksi.cud("delete from HargaFluktuatif where IDHargaFluktuatif = '" + hargaID + "'");
                MessageBox.Show("Delete Success");
                dgView();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbRoom.Text.Equals("") || dtpAkhir.Text.Equals("") || dtpAwal.Equals("") || txtPersen.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("Insert into HargaFluktuatif(IDTipeKamar,Persentase,TglMulai,TglSelesai) values('" + idRoom
                    + "','" + txtPersen.Text
                    + "','" + dtpAwal.Value.ToString("yyyy/MM/dd")
                    + "','" + dtpAkhir.Value.ToString("yyyy/MM/dd")
                    + "')");
                MessageBox.Show("Add Success");
                dgView();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void ManageHolidayPriceForm_Load(object sender, EventArgs e)
        {

        }

        public ManageHolidayPriceForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            room();
            dgView();
        }

        private void dgView()
        {
            int i= 1;
            dt.Clear();
            dataGridView1.Refresh();
            koneksi.select("select TipeKamar.NamaKamar,HargaKamar.HargaKamar,HargaFluktuatif.Persentase,HargaKamar.HargaKamar + ( Cast(HargaFluktuatif.Persentase as float) / 100 * HargaKamar.HargaKamar) as FinalPrice,HargaFluktuatif.TglMulai,HargaFluktuatif.TglSelesai,HargaFluktuatif.IDHargaFluktuatif from HargaFluktuatif left join TipeKamar on HargaFluktuatif.IDTipeKamar=TipeKamar.IDTipeKamar left join HargaKamar on HargaKamar.IDTipeKamar=TipeKamar.IDTipeKamar where "
            + "TipeKamar.NamaKamar like '%" + txtSearch.Text + "%' or HargaFluktuatif.IDHargaFluktuatif like '%" + txtSearch.Text + "%' or HargaFluktuatif.TglMulai like '%" + txtSearch.Text + "%' or HargaFluktuatif.TglSelesai like '%"
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
                    dtr[6].ToString()
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

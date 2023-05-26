using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageRoomForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(),dts = new DataTable();
        String idType, nmType,ID;
        public ManageRoomForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            type();
            dgView();
        }
        private void clear()
        {
            txtFloor.Text = "";
            txtID.Text = "";
            txtNumber.Text = "";
            cbType.Text = "";
        }
        private void type()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeKamar");
            koneksi.da.Fill(dts);
            foreach(DataRow dtr in dts.Rows){
                cs.Add(dtr["IDTipeKamar"].ToString(), dtr["NamaKamar"].ToString());
            }
            cbType.DataSource = new BindingSource(cs, null);
            cbType.DisplayMember = "Value";
            cbType.ValueMember = "Key";
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgView();
        }
        private void dgView()
        {
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            koneksi.select("select Kamar.IDKamar,TipeKamar.NamaKamar,Kamar.NomorKamar,Kamar.Lantai from Kamar,TipeKamar where Kamar.IDTipeKamar=TipeKamar.IDTipeKamar and Kamar.NomorKamar like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.Columns[0].HeaderText = "Room ID";
            dataGridView1.Columns[1].HeaderText = "Room Type Name";
            dataGridView1.Columns[2].HeaderText = "Room No";
            dataGridView1.Columns[3].HeaderText = "Floor";
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            txtID.Text = row.Cells[0].Value.ToString();
            cbType.Text = row.Cells[1].Value.ToString();
            txtNumber.Text = row.Cells[2].Value.ToString();
            txtFloor.Text = row.Cells[3].Value.ToString();
            btnAdd.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text.Equals("") || txtFloor.Text.Equals("")||cbType.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.select("insert Into Kamar (NomorKamar,Lantai,IDTipeKamar) values('" + txtNumber.Text
                    + "','" + txtFloor.Text
                    + "','" + idType
                    + "')");
                MessageBox.Show("Add Success");
                dgView();
                clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                koneksi.cud("delete from Kamar where IDKamar = '" + txtID.Text + "'");
                MessageBox.Show("Delete Success");
                dgView();
                clear();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            idType = ((KeyValuePair<String, String>)cbType.SelectedItem).Key;
            nmType = ((KeyValuePair<String, String>)cbType.SelectedItem).Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text.Equals("") || txtFloor.Text.Equals("") || cbType.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.select("update Kamar set Lantai = '" + txtFloor.Text
                    + "',NomorKamar = '" + txtNumber.Text
                    + "',IDTipeKamar = '" + idType
                    + "' where IDKamar = '" + txtID.Text + "'");
                MessageBox.Show("Update Success");
                dgView();
                clear();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void ManageGuestForm_Load(object sender, EventArgs e)
        {

        }
    }
}

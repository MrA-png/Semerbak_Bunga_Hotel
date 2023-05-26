using System;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageRoomTypeForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(),dts = new DataTable();
        String idType, nmType, idPenghuni,ID;
        public ManageRoomTypeForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            dgView();
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
            koneksi.select("select*from TipeKamar where NamaKamar like '%" + txtSearch.Text + "%' or IDTipeKamar like '%" + txtSearch.Text + "%' or Deskripsi like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.Columns[0].HeaderText = "Room Type ID";
            dataGridView1.Columns[1].HeaderText = "Room Type Name";
            dataGridView1.Columns[2].HeaderText = "Decription";
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
            txtID.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtDesc.Text = row.Cells[2].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDesc.Text.Equals("") || txtName.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.select("insert Into TipeKamar(NamaKamar,Deskripsi) values('" + txtName.Text
                    + "','" + txtDesc.Text
                    + "')");
                MessageBox.Show("Add Success");
                dgView();
                clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }
        private void clear()
        {
            txtDesc.Text = "";
            txtID.Text = "";
            txtName.Text = "";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                koneksi.cud("delete from TipeKamar where IDTipeKamar = '" + txtID.Text + "'");
                MessageBox.Show("Delete Success");
                dgView();
                clear();
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnAdd.Enabled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtDesc.Text.Equals("")|| txtName.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.select("update TipeKamar set NamaKamar = '" + txtName.Text
                    + "',Deskripsi = '" + txtDesc.Text
                    + "' where IDTipeKamar = '" + txtID.Text + "'");
                MessageBox.Show("Update Success");
                dgView();
                clear();
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnAdd.Enabled = true;
            }
        }

        private void ManageGuestForm_Load(object sender, EventArgs e)
        {

        }
    }
}

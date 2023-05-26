using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageGuestForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(),dts = new DataTable();
        String idType, nmType, idPenghuni,ID;
        public ManageGuestForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            vehicle();
            dgView();
        }

        private void vehicle()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeKendaraan");
            koneksi.da.Fill(dts);
            foreach(DataRow dtr in dts.Rows)
            {
                cs.Add(dtr["IDTipeKendaraan"].ToString(), dtr["NamaTipeKendaraan"].ToString());
            }
            cbType.DataSource = new BindingSource(cs, null);
            cbType.DisplayMember = "Value";
            cbType.ValueMember = "Key";

        }
        private void clear()
        {
            txtEmail.Text = "";
            txtID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtVehicle.Text = "";
            cbType.Text = "";
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
            koneksi.select("select Penghuni.IDPenghuni,Penghuni.NamaPenghuni,Penghuni.NomorKTP,Penghuni.Email,Penghuni.NomorHP,Penghuni.PlatNoKendaraan,TipeKendaraan.NamaTipeKendaraan from Penghuni,TipeKendaraan where Penghuni.IDTipeKendaraan = TipeKendaraan.IDTipeKEndaraan and Penghuni.NamaPenghuni like '%" + txtSearch.Text + "%' OR Penghuni.Email like '%" + txtSearch.Text + "%' OR Penghuni.NomorHP like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
            idPenghuni = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtID.Text = row.Cells[2].Value.ToString();
            txtEmail.Text = row.Cells[3].Value.ToString();
            txtPhone.Text = row.Cells[4].Value.ToString();
            txtVehicle.Text = row.Cells[5].Value.ToString();
            cbType.Text = row.Cells[6].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Equals("") || txtName.Text.Equals("") || txtPhone.Text.Equals("") || txtVehicle.Text.Equals("") || cbType.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("insert Into Penghuni(NamaPenghuni,NomorKTP,Email,NomorHP,PlatNoKendaraan,IDTipeKendaraan) values('" + txtName.Text
                    + "','" + txtID.Text
                    + "','" + txtEmail.Text
                    + "','" + txtPhone.Text
                    + "','" + txtVehicle.Text
                    + "','" + idType
                    + "')");
                MessageBox.Show("Add Success");
                dgView();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                koneksi.cud("delete from Penghuni where IDPenghuni = '" + idPenghuni + "'");
                MessageBox.Show("Delete Success");
                dgView();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Equals("")  || txtName.Text.Equals("") || txtPhone.Text.Equals("") || txtVehicle.Text.Equals("") || cbType.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("update Penghuni set NamaPenghuni = '" + txtName.Text
                        + "',NomorKTP = '" + txtID.Text
                        + "',Email = '" + txtEmail.Text
                        + "',NomorHP = '" + txtPhone.Text
                        + "',PlatNoKendaraan = '" + txtVehicle.Text
                        + "',IDTipeKendaraan = '" + idType
                        + "' where IDPenghuni = '" + idPenghuni + "'");
                MessageBox.Show("Update Success");
                dgView();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                clear();
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            idType = ((KeyValuePair<String, String>)cbType.SelectedItem).Key;
            nmType = ((KeyValuePair<String, String>)cbType.SelectedItem).Value;
        }

        private void ManageGuestForm_Load(object sender, EventArgs e)
        {

        }
    }
}

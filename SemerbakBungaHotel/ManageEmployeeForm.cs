using System;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ManageEmployeeForm : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dt = new DataTable(),dts = new DataTable();
        String idType, nmType, ID;
        public ManageEmployeeForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            dgView();
        }
        private void clear()
        {

            txtEmail.Text = "";
            txtID.Text = "";
            txtName.Text = "";
            txtPass.Text = "";
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
            koneksi.select("select*from Karyawan where NamaKaryawan like '%" + txtSearch.Text + "%' or Email like '%" + txtSearch.Text + "%'");
            koneksi.da.Fill(dt);
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
            txtEmail.Text = row.Cells[2].Value.ToString();
            txtPass.Text = row.Cells[3].Value.ToString();
            dateTimePicker1.Text = row.Cells[4].Value.ToString();
            String jk = row.Cells[5].Value.ToString();
            if (jk.Equals("M"))
            {
                Male.Checked = true;
            }
            else
            {
                Female.Checked = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            if(Male.Checked == false || Female.Checked == false)
            {
                if (txtEmail.Text.Equals("") || txtName.Text.Equals("") || txtPass.Text.Equals("") || dateTimePicker1.Text.Equals(""))
                {
                    MessageBox.Show("Please Fill All Input");
                }
                else
                {
                    String jk = "";
                    if (Male.Checked)
                    {
                        jk = "M";
                    }
                    else
                    {
                        jk = "F";
                    }
                    koneksi.cud("insert Into Karyawan(NamaKaryawan,Email,Password,TglLahir,JenisKelamin) values('" + txtName.Text
                        + "','" + txtEmail.Text
                        + "','" + txtPass.Text
                        + "','" + dateTimePicker1.Value.ToString("yyyy/MM/dd")
                        + "','" + jk
                        + "')");
                    MessageBox.Show("Add Success");
                    dgView();
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Input");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                koneksi.cud("delete from Karyawan where IDKaryawan = '" + txtID.Text + "'");
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
            if(Male.Checked == false || Female.Checked == false)
            {
                if (txtEmail.Text.Equals("") || txtName.Text.Equals("") || txtPass.Text.Equals("") || dateTimePicker1.Text.Equals(""))
                {
                    MessageBox.Show("Please Fill All Input");
                }
                else
                {
                    String jk = "";
                    if (Male.Checked)
                    {
                        jk = "M";
                    }
                    else
                    {
                        jk = "F";
                    }
                    koneksi.cud("update Karyawan set NamaKaryawan = '" + txtName.Text
                        + "',Email = '" + txtEmail.Text
                        + "',Password = '" + txtPass.Text
                        + "',TglLahir = '" + dateTimePicker1.Value.ToString("yyyy/MM/dd")
                        + "',JenisKelamin = '" + jk
                        + "' where IDKaryawan = '" + txtID.Text + "'");
                    MessageBox.Show("Update Success");
                    dgView();
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Input");
            }
        }

        private void ManageGuestForm_Load(object sender, EventArgs e)
        {

        }
    }
}

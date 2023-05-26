using System;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class ChangePasswordForm : Form
    {
        String ID;
        Koneksi koneksi = new Koneksi();
        public ChangePasswordForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            koneksi.select("select*from Karyawan where Password = '" + txtOld.Text + "'");
            if(koneksi.dt.Rows.Count <= 0)
            {
                MessageBox.Show("Old Passwrod Wrong");
            }
            else
            {
                if (txtNew.Text.Equals(txtConfirm.Text))
                {
                    koneksi.cud("update Karyawan set Password = '" + txtConfirm.Text + "'");
                    MessageBox.Show("Change Password Successs");
                }
                else
                {
                    MessageBox.Show("Password Not Match");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }
    }
}

using System;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class LoginForm : System.Windows.Forms.Form
    {
        Koneksi koneksi = new Koneksi();
        String ID;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            koneksi.select("select*from Karyawan where Email = '" + txtEmail.Text + "' and Password = '" + txtPassword.Text + "'");
            if (koneksi.dt.Rows.Count <= 0)
            {
                MessageBox.Show("Please Try Again, Your Data is not Valid");
            }
            else
            {
                ID = koneksi.dt.Rows[0]["IDKaryawan"].ToString();
                MessageBox.Show("Login Success");
                MainForm fr = new MainForm(ID);
                fr.Show();
                this.Hide();
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class RoomAvailabilityReport : Form
    {
        String ID;
        public RoomAvailabilityReport(String ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void TransactionReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm fr = new MainForm(ID);
            fr.Show();
            this.Hide();
        }
    }
}

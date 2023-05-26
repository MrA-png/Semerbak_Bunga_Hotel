using System;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class MainForm : Form
    {
        String ID;
        public MainForm(String ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm fr = new ChangePasswordForm(ID);
            fr.Show();
            this.Hide();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm fr = new LoginForm();
            fr.Show();
            this.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void bookingRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookingRoomForm fr = new BookingRoomForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageGuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageGuestForm fr = new ManageGuestForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageBookingForm fr = new ManageBookingForm(ID);
            fr.Show();
            this.Hide();
        }

        private void inputPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputPaymentForm fr = new InputPaymentForm(ID);
            fr.Show();
            this.Hide();
        }

        private void viewPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPaymentForm fr = new ViewPaymentForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageRoomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoomTypeForm fr = new ManageRoomTypeForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoomForm fr = new ManageRoomForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageRoomFacilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoomFacilityForm fr = new ManageRoomFacilityForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageRoomRepairmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRoomRepirmentForm fr = new ManageRoomRepirmentForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageDailyPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDailyPriceForm fr = new ManageDailyPriceForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageHolidayPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageHolidayPriceForm fr = new ManageHolidayPriceForm(ID);
            fr.Show();
            this.Hide();
        }

        private void manageEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageEmployeeForm fr = new ManageEmployeeForm(ID);
            fr.Show();
            this.Hide();
        }

        private void transactionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransactionReport fr = new TransactionReport(ID);
            fr.Show();
            this.Hide();
        }

        private void roomAvailabilityReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomAvailabilityReport fr = new RoomAvailabilityReport(ID);
            fr.Show();
            this.Hide();
        }
    }
}

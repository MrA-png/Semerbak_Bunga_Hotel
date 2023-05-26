using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SemerbakBungaHotel
{
    public partial class AddNewGuest : Form
    {
        Koneksi koneksi = new Koneksi();
        DataTable dts = new DataTable();
        String idType, nmtype,ID;
        public AddNewGuest(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            type();
        }

        private void type()
        {
            Dictionary<String, String> cs = new Dictionary<string, string>();
            koneksi.select("select*from TipeKendaraan");
            koneksi.da.Fill(dts);
            foreach (DataRow dtr in dts.Rows)
            {
                cs.Add(dtr["IDTipeKendaraan"].ToString(), dtr["NamaTipeKendaraan"].ToString());
            }
            cbType.DataSource = new BindingSource(cs, null);
            cbType.DisplayMember = "Value";
            cbType.ValueMember = "Key";
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            idType = ((KeyValuePair<String, String>)cbType.SelectedItem).Key;
            nmtype = ((KeyValuePair<String, String>)cbType.SelectedItem).Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookingRoomForm fr = new BookingRoomForm(ID);
            fr.Show();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text.Equals("")|| txtVehicle.Text.Equals("") || txtPhone.Text.Equals("") || txtID.Text.Equals("") || txtGuest.Text.Equals("") || cbType.Text.Equals(""))
            {
                MessageBox.Show("Please Fill All Input");
            }
            else
            {
                koneksi.cud("Insert into Penghuni(NamaPenghuni,NomorKTP,Email,NomorHP,PlatNoKendaraan,IDTipeKendaraan) values('" + txtGuest.Text
                    + "','" + txtID.Text
                    + "','" + txtEmail.Text
                    + "','" + txtPhone.Text
                    + "','" + txtVehicle.Text
                    + "','" + idType
                    + "')");
                MessageBox.Show("Insert Success");
            }
        }
    }
}

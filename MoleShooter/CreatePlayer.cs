using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleShooter
{
    public partial class CreatePlayer : Form
    {
        public CreatePlayer()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-S19E9BQ\\SQLEXPRESS;Initial Catalog=gamedb;Integrated Security=True";
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if(con.State == System.Data.ConnectionState.Open)
            {
                string q = "insert into Users(username) Values('" + username.text.ToString() + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful");


                MessageBox.Show("Сайн байна уу " + username.text.ToString() + " Ангийн улиралд тавтай морил ");

                MoleShooter ms = new MoleShooter(username.text.ToString());
                this.Visible = false;
                ms.Visible = true;
            }
             
            //custom_dialog.ShowDialog("New Score!!");

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            GameIntro gi = new GameIntro();
            this.Visible = false;
            gi.Visible = true;
        }
    }
}

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
    public partial class Score : Form
    {
         DataSet ds = new DataSet();
        public Score()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Score_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-S19E9BQ\\SQLEXPRESS;Initial Catalog=gamedb;Integrated Security=True";
            string sql = "SELECT * from Users order by Score desc";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            connection.Open();
            dataadapter.Fill(ds, "Titles_table");
            connection.Close();

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            GameIntro gi = new GameIntro();
            this.Visible = false;
            gi.Visible = true;
        }
    }
}

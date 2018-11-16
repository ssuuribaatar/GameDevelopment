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
    public partial class GameIntro : Form
    {
        int _cursX = 0;
        int _cursY = 0;


        public string conString = "Data Source=DESKTOP-S19E9BQ\\SQLEXPRESS;Initial Catalog=gamedb;Integrated Security=True";

        public GameIntro()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "X=" + _cursX.ToString() + ":" + "Y=" + _cursY.ToString(), _font,
                new Rectangle(30, 28, 120, 20), SystemColors.ControlText, flags);

            base.OnPaint(e);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            _cursX = e.X;
            _cursY = e.Y;
           // label1.Text = _cursX.ToString();
           // label2.Text = _cursY.ToString();

            // this.Refresh();
        }

        private void GameIntro_Load(object sender, EventArgs e)
        {

        }

        private void GameIntro_MouseMove(object sender, MouseEventArgs e)
        {
            _cursX = e.X;
            _cursY = e.Y;
          //  label1.Text = _cursX.ToString();
            //label2.Text = _cursY.ToString();
        }

        private void GameIntro_MouseClick(object sender, MouseEventArgs e)
        {
            string user = this.user.text.ToString();

                if (e.X >= 375 && e.X <= 568 && e.Y >= 333 && e.Y <= 410)
                {
                if (this.user.text.ToString() != null)
                {/*
                    
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        string q = "select * from Users Where username = '" + this.user.text.ToString() + "'";
                        SqlCommand cmd = new SqlCommand(q, con);
                        SqlDataAdapter sda = new SqlDataAdapter(q, con);
                        DataTable dtbl = new DataTable();
                        sda.Fill(dtbl);
                        if(dtbl.Rows.Count == 1)
                        {
                           

                            MoleShooter ms = new MoleShooter(this.user.text.ToString());
                            this.Visible = false;
                            ms.Visible = true;
                        }
                        else
                            MessageBox.Show("Хэрэглэгчийн нэр бүртгэлгүй байна!");
                    }*/
                }
               // else MessageBox.Show("Хоосон талбар байна!");
                }
                else if (e.X >= 331 && e.X <= 366 && e.Y >= 505 && e.Y <= 558)
                {
                 //   CreatePlayer cp = new CreatePlayer();
                   // this.Visible = false;
                  //  cp.Visible = true;

                }

                else if (e.X >= 447 && e.X <= 505 && e.Y >= 506 && e.Y <= 555)
                {
                    //Score sc = new Score();
                   // this.Visible = false;
                    //sc.Visible = true;

                }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (this.user.text.ToString() != null)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "select * from Users Where username = '" + this.user.text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter sda = new SqlDataAdapter(q, con);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    if (dtbl.Rows.Count == 1)
                    {


                        MoleShooter ms = new MoleShooter(this.user.text.ToString());
                        this.Visible = false;
                        ms.Visible = true;
                    }
                    else
                        MessageBox.Show("Хэрэглэгчийн нэр бүртгэлгүй байна!");
                }
            }
            else MessageBox.Show("Хоосон талбар байна!");

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            CreatePlayer cp = new CreatePlayer();
            this.Visible = false;
            cp.Visible = true;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Score sc = new Score();
            this.Visible = false;
            sc.Visible = true;

        }

        private void user_OnTextChange(object sender, EventArgs e)
        {

        }
    }
}

//#define Debug
namespace MoleShooter
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Media;
    using System.Windows.Forms;
    using Models;
    using Properties;

    public partial class MoleShooter : Form
    {
        private const int SplashNum = 3;

        public static string conString = "Data Source=DESKTOP-S19E9BQ\\SQLEXPRESS;Initial Catalog=gamedb;Integrated Security=True";
#if Debug
        private int currX = 0;
        private int currY = 0;
#endif
        private int moleCounter;
        private int splashCounter;
        private Mole mole;
        private MenuBoard menuBoard;
        private ScoreBoard scoreBoard;
        private BloodSplash bloodSplash;
        private Random random;
        private bool isDead = false;
        
        private int hits;
        private int misses;
        private int shotsFired;
        private double avgHits;
        private int frameNum = 8;
        private string level = "Noob";
        private string username;
        public static string MaxScore;
        public static int point=60;
        GameIntro gi = new GameIntro();

        public MoleShooter(string _userid)
        {
            InitializeComponent();
            username = _userid;
           
            Bitmap bmp = Resources.Crosshair;
            this.Cursor = CustomCursor.CreateCursor(bmp, bmp.Height / 2, bmp.Width / 2);
            
            this.mole = new Mole(10, 200);
            this.menuBoard = new MenuBoard(770, 30);
            this.scoreBoard = new ScoreBoard(10, -20);
            this.bloodSplash = new BloodSplash(0, 0);
            this.random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select max(score) from Users ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MaxScore = dt.Rows[0][0].ToString();
        }
        public static void GetMaxScore()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select max(score) from Users ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MaxScore = dt.Rows[0][0].ToString();

        }

        private void timeGameLoop_Tick(object sender, EventArgs e)
        {
            if (this.avgHits <= 30)
            {
                this.frameNum = 8;
                this.level = "Энгийн";
            }
            else if (this.avgHits <= 50)
            {
                this.frameNum = 6;
                this.level = "Сайн";
            }
            else if (this.avgHits >= 75)
            {
                this.frameNum = 4;
                this.level = "Шилдэг";
            }

            if (this.moleCounter >= this.frameNum)
            {
                this.UpdateMole();
                this.moleCounter = 0;
            }

            if (this.isDead)
            {
                if (this.splashCounter >= SplashNum)
                {
                    this.splashCounter = 0;
                    this.UpdateMole();
                    this.isDead = false;
                }

                this.splashCounter++;
            }

            this.moleCounter++;
            this.Refresh();
        }

        private void UpdateMole()
        {
            this.mole.Update(this.random.Next(Resources.Mole.Width, this.Width - Resources.Mole.Width),
                            this.random.Next(this.Height/2, this.Height - Resources.Mole.Height * 2));
        }

        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;

#if Debug
            TextRenderer.DrawText(
                dc,
                $"X = {this.currX} : Y = {this.currY}",
                font,
                new Rectangle(0, 0, 150, 20),
                SystemColors.ControlText,
                textFormatFlags);
           dc.DrawRectangle(new Pen(Color.Black, 3), mole.moleHotSpot.X, this.mole.moleHotSpot.Y, this.mole.moleHotSpot.Width, this.mole.moleHotSpot.Height);

#endif
            if (this.isDead)
            {
                this.bloodSplash.DrawImage(dc);
            }
            else
            {
                if (this.moleCounter >= 1)
                {
                    this.mole.DrawImage(dc);
                }
            }


            this.menuBoard.DrawImage(dc);
            this.scoreBoard.DrawImage(dc);

           
            TextFormatFlags textFormatFlags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font font = new Font("Stencil", 10);
            TextRenderer.DrawText(dc, $"Шилдэг оноо: {MaxScore}", font, new Rectangle(44, 142, 147, 160), Color.Black, textFormatFlags);
            TextRenderer.DrawText(dc, $"Хэрэглэгч: {this.username}", font, new Rectangle(44, 158, 159, 168), Color.Black, textFormatFlags);
            TextRenderer.DrawText(dc, $"Буудалт: {this.shotsFired}", font, new Rectangle(40, 30, 120, 20), Color.Black,textFormatFlags);
            TextRenderer.DrawText(dc, $"Оносон: {this.hits}", font, new Rectangle(40, 47, 120, 20), Color.Black, textFormatFlags);
            TextRenderer.DrawText(dc, $"Алдсан: {this.misses}", font, new Rectangle(40, 69, 120, 20), Color.Black, textFormatFlags);
            TextRenderer.DrawText(dc, $"Дундаж: {this.avgHits:F0} %", font, new Rectangle(40, 85, 120, 20), Color.Black, textFormatFlags);
            TextRenderer.DrawText(dc, $"Үе: {this.level}", new Font("Times New Roman", 13, FontStyle.Bold), new Rectangle(40, 110, 200, 80), Color.Black, textFormatFlags);

            base.OnPaint(e);
        }


        
        private void MoleShooter_MouseMove(object sender, MouseEventArgs e)
        {
#if Debug
            this.currX = e.X;
            this.currY = e.Y;

#endif
      

            this.Refresh();
        }

        private void MoleShooter_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 816 && e.X <= 928 && e.Y >= 55 && e.Y <= 84  )
            {
                this.timeGameLoop.Start();
                timer1.Enabled = true;

            }

            else if (e.X >= 815 && e.X <= 909 && e.Y >= 87 && e.Y <= 113)
            {
                this.timeGameLoop.Stop();
                this.moleCounter = 0;
                this.splashCounter = 0;
                this.isDead = false;
                this.hits = 0;
                this.shotsFired = 0;
                this.avgHits = 0;
                this.misses = 0;
                this.level = "Энгийн";
                timer1.Enabled = false;
                point = 60;
            }
            else if (e.X > 816 && e.X < 928 && e.Y > 116 && e.Y < 145)
            {
                this.moleCounter = 0;
                this.splashCounter = 0;
                this.isDead = false;
                this.timeGameLoop.Stop();
                this.hits = 0;
                this.shotsFired = 0;
                this.avgHits = 0;
                this.misses = 0;
                this.level = "Энгийн";
            }

            else if (e.X >= 816 && e.X <= 901 && e.Y >= 147 && e.Y <= 176)
            {
                if (hits != null)
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string q = "UPDATE users SET score = '" + hits + "' WHERE username = '" + username + "' ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                  
                    SqlDataAdapter sda = new SqlDataAdapter("select max(score) from Users ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MaxScore = dt.Rows[0][0].ToString();

                    if (hits > Int32.Parse(MaxScore))
                    {
                        custom_dialog.ShowDialog("New High Score: " + hits.ToString());
                    }
                    else
                    {
                        custom_dialog.ShowDialog("Score: " + hits.ToString());
                        
                        this.Visible = false;
                        gi.Visible = true;
                    }
                    con.Close();
                }
                else
                    this.Visible = false;
                    gi.Visible = true;

            }
            else if(this.moleCounter >= 1)
            {
                if (this.mole.Hit(e.X, e.Y))
                {
                    this.isDead = true;
                    this.bloodSplash.Left = this.mole.Left;
                    this.bloodSplash.Top = this.mole.Top;
                    this.hits++;
                }

                this.ProduceGunshot();
                this.shotsFired++;
                this.misses = this.shotsFired - this.hits;
                this.avgHits = (double) this.hits/this.shotsFired * 100;
            }

        }


        private void ProduceGunshot()
        {
            SoundPlayer gunSound = new SoundPlayer(Resources.GunShot);
            gunSound.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (--point == 0)
            {
                timer1.Enabled = !timer1.Enabled;
                label1.Text = "Time's Out.";
                MessageBox.Show("Time is Over ", "End Of the Game");
                this.timeGameLoop.Stop();
                this.moleCounter = 0;
                this.splashCounter = 0;
                this.isDead = false;
                this.hits = 0;
                this.shotsFired = 0;
                this.avgHits = 0;
                this.misses = 0;
                this.level = "Noob";
            }
            else
                label1.Text = "" + point;
        }
    }
}
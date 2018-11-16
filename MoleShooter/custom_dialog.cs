using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleShooter
{
    public partial class custom_dialog : Form
    {
        public custom_dialog(string _message)
        {
            InitializeComponent();
            label1.Text = _message;
        }

        private void custom_dialog_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
             
        }

        private void bunifuFormFadeTransition1_TransitionEnd(object sender, EventArgs e)
        {
            icon_delay.Start();
            icon.Enabled = true;
        }

        private void icon_delay_Tick(object sender, EventArgs e)
        {
            icon.Enabled = false;
            icon_delay.Stop();
            btnOk.Visible = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void ShowDialog(string message)
        {
            custom_dialog cd = new custom_dialog(message);
            cd.ShowDialog();
        }
    }
}

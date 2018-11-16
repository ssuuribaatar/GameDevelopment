using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleShooter
{
    public partial class Progress : Form
    {
        GameIntro Gi = new GameIntro();
        public Progress()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(100);
                sum = sum + i;
                // Calling ReportProgress() method raises ProgressChanged event
                // To this method pass the percentage of processing that is complete
                backgroundWorker1.ReportProgress(i);
                if (i == 100)
                {


                    e.Cancel = true;
                    // Reset progress percentage to ZERO and return
                    backgroundWorker1.ReportProgress(100);

                    return;



                }

                // Check if the cancellation is requested
                if (backgroundWorker1.CancellationPending)
                {
                    // Set Cancel property of DoWorkEventArgs object to true
                    e.Cancel = true;
                    // Reset progress percentage to ZERO and return
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Processing cancelled";
                this.Visible = false;
                Gi.Show();


            }
            else if (e.Error != null)
            {
                label1.Text = e.Error.Message;
            }
            else
            {
                label1.Text = e.Result.ToString();
            }
        }

        private void Progress_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                // This method will start the execution asynchronously in the background
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}

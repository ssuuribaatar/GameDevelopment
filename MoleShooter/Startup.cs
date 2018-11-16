namespace MoleShooter
{
    using System;
    using System.Windows.Forms;

    static class Startup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Application.Run(new Score());
              Application.Run(new Progress());
             // Application.Run(new GameIntro());
            Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MoleShooter());
           // Application.Run(new custom_dialog("New High Score!!!"));
        }
    }
}

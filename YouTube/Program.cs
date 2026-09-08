using System;
using System.Windows.Forms;

namespace YouTube
{
    static class Program
    {
        public static Forms.BrowseForm browseForm;
        public static Forms.WatchForm watchForm;
        static bool isClosingApp;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string startVideoId = null;
            if (args.Length == 1)
            {
                // try to parse arg as URL, if it fails assume it's a video ID
                startVideoId = Utils.GetVideoUrlID(args[0], args[0]);
            }
            else if (args.Length >= 2)
            {
                switch (args[0])
                {
                    case "watch":
                        // try to parse arg as URL, if it fails assume it's a video ID
                        startVideoId = Utils.GetVideoUrlID(args[1], args[1]);
                        break;
                }
            }

            browseForm = new Forms.BrowseForm();
            watchForm = new Forms.WatchForm();
            watchForm.InitialVideoId = startVideoId;
            browseForm.FormClosing += BrowseForm_FormClosing;
            watchForm.FormClosing += WatchForm_FormClosing;

            if (startVideoId != null)
            {
                Application.Run(watchForm);
            }
            else
            {
                Application.Run(browseForm);
            }
        }

        private static void BrowseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseBoth(watchForm);
        }

        private static void WatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseBoth(browseForm);
        }

        private static void CloseBoth(Form other)
        {
            if (isClosingApp)
                return;
            isClosingApp = true;

            if (other != null && !other.IsDisposed)
                other.Close();
        }

        public static void SwitchView()
        {
            if (browseForm.Visible)
            {
                watchForm.Show();
                browseForm.Hide();
            }
            else if (watchForm.Visible)
            {
                browseForm.Show();
                watchForm.Hide();
            }
        }
    }
}
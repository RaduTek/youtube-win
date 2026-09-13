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

            if (Settings.Default.WindowBounds.Width > 0)
            {
                browseForm.StartPosition = watchForm.StartPosition = FormStartPosition.Manual;
                browseForm.DesktopBounds = watchForm.DesktopBounds = Settings.Default.WindowBounds;
                browseForm.WindowState = watchForm.WindowState = Settings.Default.WindowState;
            }

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
            CloseBoth(browseForm, watchForm);
        }

        private static void WatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseBoth(watchForm, browseForm);
        }

        private static void CloseBoth(Form current, Form other)
        {
            if (isClosingApp)
                return;
            isClosingApp = true;

            Settings.Default.WindowState = current.WindowState;
            Settings.Default.WindowBounds = current.WindowState == FormWindowState.Maximized ? current.RestoreBounds : current.DesktopBounds;
            Settings.Default.Save();

            if (other != null && !other.IsDisposed)
                other.Close();
        }

        private static void SyncWindowState(Form current, Form target)
        {
            target.DesktopBounds = current.WindowState == FormWindowState.Maximized ? current.RestoreBounds : current.DesktopBounds;
            target.WindowState = current.WindowState;
        }

        public static void SwitchView()
        {
            if (browseForm.Visible)
            {
                SyncWindowState(browseForm, watchForm);
                watchForm.Show();
                browseForm.Hide();
            }
            else if (watchForm.Visible)
            {
                SyncWindowState(watchForm, browseForm);
                browseForm.Show();
                watchForm.Hide();
            }
        }
    }
}
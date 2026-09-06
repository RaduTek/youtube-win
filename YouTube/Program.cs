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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            browseForm = new Forms.BrowseForm();
            watchForm = new Forms.WatchForm();
            browseForm.FormClosing += BrowseForm_FormClosing;
            watchForm.FormClosing += WatchForm_FormClosing;
            Application.Run(browseForm);
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
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YouTube.ExControls
{

    public class ExWebBrowser : WebBrowser
    {
        // ---------- Expose ActiveXInstance ----------
        public object Instance => this.ActiveXInstance;

        // ---------- Disable navigation click sound ----------
        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        private static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        // Static ctor runs once, before any ExWebBrowser instance is created/navigates
        static ExWebBrowser()
        {
            CoInternetSetFeatureEnabled(FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, true);
        }

        // ---------- Zoom (DPI matching) ----------
        private static readonly Guid CGID_DocHostCommandHandler =
            new Guid("f38bc242-b950-11d1-8918-00c04fc2c836");

        private const uint OLECMDID_OPTICAL_ZOOM = 63;
        private const uint OLECMDEXECOPT_DONTPROMPTUSER = 2;

        public int CurrentZoomPercent { get; private set; } = 100;

        public ExWebBrowser()
        {
            this.DocumentCompleted += ExWebBrowser_DocumentCompleted;
        }

        private void ExWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ApplyDpiZoom();
        }

        /// <summary>Computes zoom from the control's current DPI (dpi/96*100) and applies it.</summary>
        public void ApplyDpiZoom()
        {
            float dpi;
            using (Graphics g = this.CreateGraphics())
                dpi = g.DpiX;

            int zoom = (int)Math.Round(dpi / 96f * 100f); // 120 DPI -> 125
            SetZoom(zoom);
        }

        public void SetZoom(int zoomPercent)
        {
            try
            {
                object iwb2 = this.Instance; // ActiveXInstance implements IWebBrowser2
                object zoom = (int)(zoomPercent * zoomPercent / 100); // zoomPercent; value needs to be scaled by the DPI for some reason

                iwb2.GetType().InvokeMember(
                    "ExecWB",
                    System.Reflection.BindingFlags.InvokeMethod,
                    null,
                    iwb2,
                    new object[] { OLECMDID_OPTICAL_ZOOM, OLECMDEXECOPT_DONTPROMPTUSER, zoom, zoom });

                CurrentZoomPercent = zoomPercent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExecWB zoom failed: {ex.Message}");
            }
        }
    }
}

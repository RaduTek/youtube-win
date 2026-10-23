using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace YouTube
{
    static class Utils
    {
        public static string GetMyVideosFolder()
        {
            bool isWinXP = Environment.OSVersion.Platform == PlatformID.Win32NT &&
                                 Environment.OSVersion.Version.Major < 6;

            if (isWinXP)
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "My Videos");
            }
            else
            {
                return Path.Combine(
                    Environment.GetEnvironmentVariable("USERPROFILE"),
                    "Videos");
            }
        }

        public static void InitialSettings()
        {
            if (Settings.Default.DownloadFolder == "")
                Settings.Default.DownloadFolder =
                    Path.Combine(
                        GetMyVideosFolder(),
                        "YouTube Downloads"
                    );

            Settings.Default.Save();
        }

        public static string FixUrlDomain(string url)
        {
            var sourceUri = new Uri(Settings.Default.InstanceBaseUrl);

            var ub = new UriBuilder(url);

            ub.Scheme = sourceUri.Scheme;
            ub.Host = sourceUri.Host;
            ub.Port = sourceUri.Port;

            return ub.ToString();
        }

        public static string FormatRelativeDate(DateTime date)
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = now - date;

            // Handle future dates
            if (difference.TotalSeconds < 0)
            {
                return date.ToShortDateString();
            }

            int days = difference.Days;

            if (days == 0)
            {
                return "today";
            }

            if (days < 7)
            {
                return days + " day" + (days == 1 ? "" : "s") + " ago";
            }

            int weeks = days / 7;

            if (days < 30)
            {
                return weeks + " week" + (weeks == 1 ? "" : "s") + " ago";
            }

            int months = days / 30;

            if (days < 365)
            {
                return months + " month" + (months == 1 ? "" : "s") + " ago";
            }

            int years = days / 365;

            return years + " year" + (years == 1 ? "" : "s") + " ago";
        }

        public static string FormatFileName(string name)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string sanitizedName = name;
            foreach (char c in invalidChars)
            {
                sanitizedName = sanitizedName.Replace(c.ToString(), "");
            }
            return sanitizedName;
        }

        public static string GetVideoFileName(string videoId, string videoTitle)
        {
            return FormatFileName(videoTitle) + " [" + videoId + "].mp4";
        }

        public static string GetVideoFilePath(string videoId, string videoTitle)
        {
            return Path.Combine(Settings.Default.DownloadFolder, GetVideoFileName(videoId, videoTitle));
        }

        public static void PlayVideo(string location)
        {
            var args = "\"" + location + "\"";

            //if (Settings.Default.PlayerCustomEnabled)
            //{
            //    Process.Start(Settings.Default.PlayerCustomPath, args);
            //    return;
            //}

            var wmplayer = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "Windows Media Player\\wmplayer.exe"
            );

            //if (Settings.Default.PlayerWmpFullscreen)
            //    args += " /fullscreen";

            Process.Start(wmplayer, args);
        }

        /// <summary>
        /// Header downloads indicator: only visible while downloads are waiting or in progress
        /// </summary>
        public static void UpdateDownloadsButton(ExControls.ExButton button, System.Windows.Forms.ToolTip toolTip)
        {
            var count = DownloadManager.ActiveCount;

            button.Visible = count > 0;
            if (count == 0) return;

            var text = "Downloading " + count + (count == 1 ? " video" : " videos");

            var current = DownloadManager.Current;
            if (current != null && current.Status == DownloadStatus.Downloading)
                text += ", " + current.Progress + "%";

            if (toolTip.GetToolTip(button) != text)
                toolTip.SetToolTip(button, text);
        }

        public static string ThumbnailFolder
        {
            get { return Path.Combine(LocalFeed.DataFolder, "thumbs"); }
        }

        public static string GetThumbnailCachePath(string videoId, bool hq)
        {
            return Path.Combine(ThumbnailFolder, videoId + (hq ? "_hq" : "") + ".jpg");
        }

        public static string GetThumbnailWebUrl(string videoId, bool hq)
        {
            return "http://i.ytimg.com/vi/" + videoId + (hq ? "/hqdefault.jpg" : "/default.jpg");
        }

        /// <summary>
        /// Thumbnail URL for templates: the locally cached copy (downloaded videos) if there is one
        /// </summary>
        public static string GetThumbnailUrl(string videoId, bool hq)
        {
            var cachePath = GetThumbnailCachePath(videoId, hq);

            if (File.Exists(cachePath))
                return "file://" + cachePath.Replace('\\', '/');

            return GetThumbnailWebUrl(videoId, hq);
        }

        public static string FormatDurationSeconds(int seconds)
        {
            TimeSpan duration = TimeSpan.FromSeconds(seconds);

            if (duration.TotalHours >= 1)
                return string.Format("{0}:{1:D2}:{2:D2}",
                    (int)duration.TotalHours,
                    duration.Minutes,
                    duration.Seconds);
            else
                return string.Format("{0}:{1:D2}",
                    duration.Minutes,
                    duration.Seconds);
        }

        public static bool IsUrlOk(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (WebException)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        // Escapes user-written text for HTML and keeps line breaks.
        // WebUtility/System.Web are not available in .NET Fx 2.0 (client), so escape manually.
        public static string HtmlText(string text)
        {
            var encoded = (text ?? "")
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");

            return encoded.Replace("\r\n", "\n").Replace("\n", "<br />");
        }

        public static string CreateTemplateDoc(string templateName, Dictionary<string, string> values)
        {
            string template = Properties.Resources.ResourceManager.GetObject(templateName) as string;

            foreach (var key in values.Keys)
            {
                template = template.Replace($"__{key}__", values[key]);
            }

            var cssfile = "file://" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Res\\styles.css").Replace('\\', '/');
            var jsfile = "file://" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Res\\common.js").Replace('\\', '/');

            template = template.Replace("__cssfile__", cssfile);
            template = template.Replace("__jsfile__", jsfile);

            return template;
        }

        public static void LoadTemplate(string templateName, System.Windows.Forms.WebBrowser browser)
        {
            LoadTemplate(templateName, new Dictionary<string, string>(), browser);
        }

        public static void LoadTemplate(string templateName, Dictionary<string, string> values, System.Windows.Forms.WebBrowser browser)
        {
            var html = CreateTemplateDoc(templateName, values);

            browser.DocumentText = "0";
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            browser.Refresh();

            browser.Visible = true;
        }

        public static void LoadTemplateSlot(string templateName, Dictionary<string, string> values, string templateSlot, System.Windows.Forms.WebBrowser browser)
        {
            var slot = browser.Document?.GetElementById(templateSlot);
            if (slot == null) return;

            var html = CreateTemplateDoc(templateName, values);
            slot.InnerHtml = html;
        }

        public static void LoadTemplateListSlot(string templateName, List<Dictionary<string, string>> valuesList, string templateSlot, System.Windows.Forms.WebBrowser browser)
        {
            var slot = browser.Document?.GetElementById(templateSlot);
            if (slot == null) return;

            var html = new StringBuilder();

            foreach (var values in valuesList)
            {
                html.AppendLine(CreateTemplateDoc(templateName, values));
            }

            slot.InnerHtml = html.ToString();
        }

        public static string GetVideoUrlID(string url)
        {
            return GetVideoUrlID(url, null);
        }

        public static string GetVideoUrlID(string url, string fallback)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (UriFormatException)
            {
                return fallback;  // only fallback when it's not a valid URL, may already be a video ID
            }

            if (!string.Equals(uri.AbsolutePath, "/watch", StringComparison.OrdinalIgnoreCase))
                return null;

            string query = uri.Query;
            if (string.IsNullOrEmpty(query))
                return null;

            if (query.StartsWith("?"))
                query = query.Substring(1);

            string[] pairs = query.Split('&');
            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairs[i];
                if (pair.Length == 0)
                    continue;

                int eqIndex = pair.IndexOf('=');
                string key = eqIndex >= 0 ? pair.Substring(0, eqIndex) : pair;
                string value = eqIndex >= 0 ? pair.Substring(eqIndex + 1) : string.Empty;

                if (string.Equals(key, "v", StringComparison.OrdinalIgnoreCase))
                {
                    return Uri.UnescapeDataString(value.Replace("+", " "));
                }
            }

            return null;
        }
    }
}

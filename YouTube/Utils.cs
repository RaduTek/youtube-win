using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YouTube
{
    static class Utils
    {
        public static void InitalSettings()
        {
            if (Settings.Default.VideoPlayer == "")
                Settings.Default.VideoPlayer = 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), 
                        "Windows Media Player\\wmplayer.exe"
                    );

            if (Settings.Default.DownloadFolder == "")
                Settings.Default.DownloadFolder =
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
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

    }
}

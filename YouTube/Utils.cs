using System;
using System.Diagnostics;
using System.IO;

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

        public static void InitalSettings()
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

            if (Settings.Default.PlayerCustomEnabled)
            {
                Process.Start(Settings.Default.PlayerCustomPath, args);
                return;
            }

            var wmplayer = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "Windows Media Player\\wmplayer.exe"
            );

            if (Settings.Default.PlayerWmpFullscreen)
                args += " /fullscreen";

            Process.Start(wmplayer, args);
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
    }
}

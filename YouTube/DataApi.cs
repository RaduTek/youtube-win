using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using YouTube.Data;

namespace YouTube
{
    static class DataApi
    {
        public static string MergeUrl(string baseUrl, string path)
        {
            return baseUrl.TrimEnd('/') + "/" + path.TrimStart('/');
        }

        public static string GetFullUrl(string path)
        {
            return MergeUrl(Settings.Default.InstanceBaseUrl, path);
        }

        public static string GetFinalUrl(string url, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = 10;
            request.Method = method;
            request.Timeout = 10000;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string finalUrl = response.ResponseUri.ToString();

                return finalUrl;
            }
        }

        public static Feed GetFeed(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Feed));
                Feed feed = (Feed)serializer.Deserialize(stream);

                return feed;
            }
        }

        public static Feed GetSearchResults(string queryText)
        {
            var query = queryText.Replace(' ', '+');
            var url = GetFullUrl("feeds/api/videos?q=" + query);

            return GetFeed(url);
        }

        public static string GetVideoUrl(string videoId)
        {
            return GetVideoUrl(videoId, Settings.Default.VideoQuality, Settings.Default.InstanceType);
        }

        public static string GetVideoUrl(string videoId, string quality)
        {
            return GetVideoUrl(videoId, quality, Settings.Default.InstanceType);
        }

        public static string GetVideoUrl(string videoId, string quality, string instanceType)
        {
            switch (instanceType)
            {
                case "BackTube":
                    switch (quality)
                    {
                        case "1080p":
                            return GetFullUrl("get_video?v=" + videoId + "&q=hd1080&fallback=1");
                        case "720p":
                            return GetFullUrl("get_video?v=" + videoId + "&q=hd720&fallback=1");
                        case "480p":
                            return GetFullUrl("get_video?v=" + videoId + "&q=large&fallback=1");
                        case "360p":
                            return GetFullUrl("get_video?v=" + videoId + "&q=medium&fallback=1");
                        default:
                            throw new Exception("Invalid quality for BackTube: " + quality);
                    }
                case "yt2009":
                    switch (quality)
                    {
                        case "1080p":
                            return GetFullUrl("exp_hd?video_id=" + videoId);
                        case "720p":
                            return GetFullUrl("exp_hd?video_id=" + videoId);
                        case "480p":
                            return GetFullUrl("get_video?video_id=" + videoId);
                        case "360p":
                            return GetFullUrl("get_video?video_id=" + videoId);
                        default:
                            throw new Exception("Invalid quality for yt2009: " + quality);
                    }
                default:
                    throw new Exception("Unsupported instance: " + instanceType);
            }

        }

        public static string GetInstanceType(string baseUrl)
        {
            if (Utils.IsUrlOk(MergeUrl(baseUrl, "/backtube_test")))
            {
                return "BackTube";
            }
            else if (Utils.IsUrlOk(MergeUrl(baseUrl, "/yt2009_flags.htm")))
            {
                return "yt2009";
            }
            else if (Utils.IsUrlOk(MergeUrl(baseUrl, "/feeds/api/videos?q=test")))
            {
                return "Unknown";
            }
            else
            {
                return "Bad Instance";
            }
        }

        public static void UpdateInstanceType()
        {
            Settings.Default.InstanceType = GetInstanceType(Settings.Default.InstanceBaseUrl);
            Settings.Default.Save();
        }
    }
}

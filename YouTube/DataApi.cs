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
        public static string GetFullUrl(string path)
        {
            return Settings.Default.InstanceBaseUrl.TrimEnd('/') + "/" + path.TrimStart('/');
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
            return GetVideoUrl(videoId, VideoQuality.Default);
        }

        public static string GetVideoUrl(string videoId, VideoQuality quality)
        {
            string url;

            if (quality == VideoQuality.HighDef || (quality == VideoQuality.Default && Settings.Default.EnableHd))
            {
                url = GetFullUrl("exp_hd?video_id=" + videoId);
            }
            else
            {
                url = GetFullUrl("get_video?video_id=" + videoId + "/mp4");
            }

            return url;
        }
    }
}

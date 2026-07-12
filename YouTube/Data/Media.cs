using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YouTube.Data
{
    public class MediaGroup
    {
        [XmlElement("title", Namespace = Xmlns.Media)]
        public string Title { get; set; }

        [XmlElement("category", Namespace = Xmlns.Media)]
        public MediaCategory Category { get; set; }

        [XmlElement("content", Namespace = Xmlns.Media)]
        public MediaContent Content { get; set; }

        [XmlElement("description", Namespace = Xmlns.Media)]
        public MediaDescription Description { get; set; }

        [XmlElement("keywords", Namespace = Xmlns.Media)]
        public string Keywords { get; set; }

        [XmlElement("player", Namespace = Xmlns.Media)]
        public MediaPlayer Player { get; set; }

        [XmlElement("thumbnail", Namespace = Xmlns.Media)]
        public MediaThumbnail[] Thumbnails { get; set; }

        [XmlElement("duration", Namespace = Xmlns.YouTube)]
        public YouTubeDuration Duration { get; set; }

        [XmlElement("uploaded", Namespace = Xmlns.YouTube)]
        public DateTime Uploaded { get; set; }

        [XmlElement("uploaderId", Namespace = Xmlns.YouTube)]
        public string UploaderId { get; set; }

        [XmlElement("videoid", Namespace = Xmlns.YouTube)]
        public YouTubeVideoId VideoId { get; set; }

        [XmlElement("credit", Namespace = Xmlns.Media)]
        public MediaCredit Credit { get; set; }
    }

    public class MediaCategory
    {
        [XmlAttribute("label")]
        public string Label { get; set; }

        [XmlAttribute("scheme")]
        public string Scheme { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class MediaContent
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("medium")]
        public string Medium { get; set; }

        [XmlAttribute("expression")]
        public string Expression { get; set; }

        [XmlAttribute("duration")]
        public int Duration { get; set; }

        [XmlAttribute("format", Namespace = Xmlns.YouTube)]
        public int Format { get; set; }
    }

    public class MediaDescription
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class MediaPlayer
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
    }

    public class MediaThumbnail
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlAttribute("name", Namespace = Xmlns.YouTube)]
        public string Name { get; set; }
    }

    public class YouTubeDuration
    {
        [XmlAttribute("seconds")]
        public int Seconds { get; set; }
    }

    public class YouTubeVideoId
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class MediaCredit
    {
        [XmlAttribute("role")]
        public string Role { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("display", Namespace = Xmlns.YouTube)]
        public string Display { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}

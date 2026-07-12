using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YouTube.Data
{
    [XmlRoot("entry", Namespace = Xmlns.Atom)]
    public class Entry
    {
        [XmlElement("id", Namespace = Xmlns.Atom)]
        public string Id { get; set; }

        [XmlElement("title", Namespace = Xmlns.Atom)]
        public string Title { get; set; }

        [XmlElement("published", Namespace = Xmlns.Atom)]
        public DateTime Published { get; set; }

        [XmlElement("content", Namespace = Xmlns.Atom)]
        public string Content { get; set; }

        [XmlElement("author", Namespace = Xmlns.Atom)]
        public Author Author { get; set; }

        [XmlElement("youTubeId", Namespace = Xmlns.Atom)]
        public YouTubeId YouTubeId { get; set; }

        [XmlElement("group", Namespace = Xmlns.Media)]
        public MediaGroup Media { get; set; }

        [XmlElement("rating", Namespace = Xmlns.YouTube)]
        public YtRating Rating { get; set; }

        [XmlElement("statistics", Namespace = Xmlns.YouTube)]
        public YtStatistics Statistics { get; set; }

        [XmlElement("yt9full", Namespace = Xmlns.Atom)]
        public string ChannelName { get; set; }

        [XmlElement("yt9aid", Namespace = Xmlns.Atom)]
        public string ChannelId { get; set; }
    }

    public class Author
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("uri")]
        public string Uri { get; set; }

        [XmlElement("userId")]
        public string UserId { get; set; }
    }

    public class YouTubeId
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class YtRating
    {
        [XmlAttribute("numLikes")]
        public int Likes { get; set; }

        [XmlAttribute("numDislikes")]
        public int Dislikes { get; set; }
    }

    public class YtStatistics
    {
        [XmlAttribute("favoriteCount")]
        public int FavoriteCount { get; set; }

        [XmlAttribute("viewCount")]
        public int ViewCount { get; set; }
    }
}

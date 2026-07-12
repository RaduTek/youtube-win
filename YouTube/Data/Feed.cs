using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YouTube.Data
{

    [XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("entry")]
        public List<Entry> Entries { get; set; }

        [XmlElement("link")]
        public List<Link> Links { get; set; }
    }

    public class Link
    {
        [XmlAttribute("rel")]
        public string Relationship { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}

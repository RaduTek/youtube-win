using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using YouTube.Data;

namespace YouTube
{
    /// <summary>
    /// A list of videos stored locally as an Atom feed XML file in AppData.
    /// Only meant to be used from the UI thread.
    /// </summary>
    public class LocalFeed
    {
        public static readonly LocalFeed History = new LocalFeed("history", "History", 500);
        public static readonly LocalFeed Favorites = new LocalFeed("favorites", "Favorites", 0);
        public static readonly LocalFeed Downloads = new LocalFeed("downloads", "Downloads", 0);

        public static string DataFolder
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YouTube"); }
        }

        private readonly string name, title;
        private readonly int maxEntries; // 0 = unlimited
        private Feed feed;

        public LocalFeed(string name, string title, int maxEntries)
        {
            this.name = name;
            this.title = title;
            this.maxEntries = maxEntries;
        }

        public event EventHandler Changed;

        public string FilePath
        {
            get { return Path.Combine(DataFolder, name + ".xml"); }
        }

        public Feed Feed
        {
            get
            {
                if (feed == null) Load();
                return feed;
            }
        }

        public Entry Find(string videoId)
        {
            foreach (var entry in Feed.Entries)
            {
                if (entry.YouTubeId != null && entry.YouTubeId.Id == videoId)
                    return entry;
            }

            return null;
        }

        public bool Contains(string videoId)
        {
            return Find(videoId) != null;
        }

        /// <summary>
        /// Adds a copy of the entry at the top of the feed, replacing any existing entry for the same video.
        /// </summary>
        public Entry Add(Entry entry)
        {
            var item = entry.Clone();
            item.Added = DateTime.Now;
            item.AddedSpecified = true;

            RemoveEntry(item.YouTubeId.Id);
            Feed.Entries.Insert(0, item);

            if (maxEntries > 0 && Feed.Entries.Count > maxEntries)
                Feed.Entries.RemoveRange(maxEntries, Feed.Entries.Count - maxEntries);

            SaveChanged();
            return item;
        }

        public bool Remove(string videoId)
        {
            if (!RemoveEntry(videoId)) return false;

            SaveChanged();
            return true;
        }

        public void Clear()
        {
            Feed.Entries.Clear();
            SaveChanged();
        }

        public void Save()
        {
            Directory.CreateDirectory(DataFolder);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", Xmlns.Atom);
            ns.Add("media", Xmlns.Media);
            ns.Add("yt", Xmlns.YouTube);
            ns.Add("local", Xmlns.Local);

            // write to a temp file first so a failed write can't corrupt the existing file
            var tempPath = FilePath + ".tmp";

            using (var stream = File.Create(tempPath))
            {
                new XmlSerializer(typeof(Feed)).Serialize(stream, Feed, ns);
            }

            if (File.Exists(FilePath)) File.Delete(FilePath);
            File.Move(tempPath, FilePath);
        }

        private void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    using (var stream = File.OpenRead(FilePath))
                    {
                        feed = (Feed)new XmlSerializer(typeof(Feed)).Deserialize(stream);
                    }
                }
            }
            catch (Exception)
            {
                // keep the unreadable file around instead of overwriting it on next save
                try { File.Copy(FilePath, FilePath + ".bak", true); } catch (Exception) { }
                feed = null;
            }

            if (feed == null) feed = new Feed();
            if (feed.Entries == null) feed.Entries = new List<Entry>();
            feed.Title = title;
        }

        private bool RemoveEntry(string videoId)
        {
            var existing = Find(videoId);
            if (existing == null) return false;

            Feed.Entries.Remove(existing);
            return true;
        }

        private void SaveChanged()
        {
            Save();

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }
}

using System;
using System.Collections.Generic;
using YouTube.Data;

namespace YouTube
{
    /// <summary>
    /// In-memory list of videos to play, in order. Played items stay in the list,
    /// the one currently playing is tracked by CurrentIndex.
    /// Only meant to be used from the UI thread.
    /// </summary>
    public static class PlaybackQueue
    {
        public static readonly List<Entry> Items = new List<Entry>();

        // highlighted (currently loaded) item, -1 = none
        public static int CurrentIndex { get; private set; } = -1;

        // item to continue with when the current item was removed from the queue, -1 = none
        private static int resumeIndex = -1;

        public static event EventHandler Changed;

        /// <summary>
        /// Appends a copy of the entry, if not queued already. Returns its index.
        /// </summary>
        public static int Add(Entry entry)
        {
            var index = IndexOf(entry.YouTubeId.Id);
            if (index >= 0) return index;

            Items.Add(entry.Clone());
            OnChanged();

            return Items.Count - 1;
        }

        public static void Remove(int index)
        {
            if (index < 0 || index >= Items.Count) return;

            Items.RemoveAt(index);

            if (index < CurrentIndex)
            {
                CurrentIndex--;
            }
            else if (index == CurrentIndex)
            {
                // the item that moved into its place plays next
                CurrentIndex = -1;
                resumeIndex = index;
            }
            else if (index < resumeIndex)
            {
                resumeIndex--;
            }

            OnChanged();
        }

        public static void Clear()
        {
            Items.Clear();
            CurrentIndex = -1;
            resumeIndex = -1;
            OnChanged();
        }

        public static int IndexOf(string videoId)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].YouTubeId.Id == videoId)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Index of the item to play after the current one, or -1 if there is none.
        /// </summary>
        public static int GetNextIndex()
        {
            var next = CurrentIndex >= 0 ? CurrentIndex + 1 : resumeIndex;

            return next >= 0 && next < Items.Count ? next : -1;
        }

        public static void SetCurrent(string videoId)
        {
            var index = IndexOf(videoId);
            if (index == CurrentIndex && resumeIndex == -1) return;

            CurrentIndex = index;
            resumeIndex = -1;
            OnChanged();
        }

        private static void OnChanged()
        {
            if (Changed != null)
                Changed(null, EventArgs.Empty);
        }
    }
}

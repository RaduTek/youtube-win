using System;
using System.Collections.Generic;

namespace YouTube
{
    /// <summary>
    /// Back history of a window, like a web browser: adding an entry after going back
    /// discards the entries that came after the current one.
    /// </summary>
    public class NavigationHistory<T>
    {
        private const int MaxEntries = 50;

        private readonly List<T> items = new List<T>();
        private int index = -1; // current entry

        public event EventHandler Changed;

        public T Current
        {
            get { return index >= 0 ? items[index] : default(T); }
        }

        public bool CanGoBack
        {
            get { return index > 0; }
        }

        public void Push(T item)
        {
            // drop the forward history
            if (index < items.Count - 1)
                items.RemoveRange(index + 1, items.Count - index - 1);

            items.Add(item);

            if (items.Count > MaxEntries)
                items.RemoveAt(0);

            index = items.Count - 1;
            OnChanged();
        }

        public T GoBack()
        {
            if (!CanGoBack) return Current;

            index--;
            OnChanged();

            return items[index];
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }
}

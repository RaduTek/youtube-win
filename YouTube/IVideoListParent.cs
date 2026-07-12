using System;
using System.Collections.Generic;
using System.Text;

namespace YouTube
{
    interface IVideoListParent
    {
        void DownloadVideo(string videoId, string videoTitle);

        void QueueVideo(string videoId, string videoTitle, int videoDuration);

        void WatchVideo(string videoId, string videoTitle);
    }
}

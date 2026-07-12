using System;
using System.Collections.Generic;
using System.Text;

namespace YouTube
{
    interface IVideoListParent
    {
        void DownloadVideo(string videoId, string videoTitle);

        void WatchVideo(string videoId);
    }
}

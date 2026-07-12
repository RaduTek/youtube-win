using System;
using System.Collections.Generic;
using System.IO;

namespace YouTube
{
    public class PlaylistItem
    {
        public string Title { get; set; }

        public string VideoId { get; set; }

        public string Filename { get { return Utils.GetVideoFileName(VideoId, Title); } }

        public string FilePath { get { return Utils.GetVideoFilePath(VideoId, Title); } }

        public string VideoUrl { get { return DataApi.GetVideoUrl(VideoId); } }

        public int Duration { get; set; } = 0;
    }

    public class PlaylistUtil
    {

        public static void WriteToM3U(List<PlaylistItem> items, string filePath, bool tempPlaylist)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine("#EXTM3U");

                foreach (var item in items)
                {
                    sw.WriteLine($"#EXTINF:{item.Duration},{item.Title}");

                    if (File.Exists(item.FilePath))
                        sw.WriteLine(tempPlaylist ? item.FilePath : item.Filename);
                    else
                        sw.WriteLine(item.VideoUrl);
                }
            }
        }
    }
}

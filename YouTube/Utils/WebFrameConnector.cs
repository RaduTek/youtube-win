using System;
using System.Collections.Generic;
using System.Text;

namespace YouTube
{
    public class VideoClickEventArgs : EventArgs
    {
        public int Index { get; set; }

        public string Name { get; set; }
    }

    public class ActionClickEventArgs : EventArgs
    {
        public string Name { get; set; }

        public string Data { get; set; }
    }


    [System.Runtime.InteropServices.ComVisible(true)]
    public class WebFrameConnector
    {
        public event EventHandler<VideoClickEventArgs> OnVideoClick;

        public void VideoClick(int index, string name)
        {
            if (OnVideoClick != null)
                OnVideoClick.Invoke(null, new VideoClickEventArgs() { Index = index, Name = name });
        }

        public event EventHandler<ActionClickEventArgs> OnActionClick;

        public void ActionClick(string name, string data)
        {
            if (OnActionClick != null)
                OnActionClick.Invoke(null, new ActionClickEventArgs() { Name = name, Data = data });
        }
    }
}

using System;
using System.Drawing;

namespace YouTube.ExControls
{
    public static class ExResourceManager
    {
        public static string BuildName(string key, ExControlState state, int dpi)
        {
            if (dpi == 96 || dpi == 0)
            {
                if (state == ExControlState.None)
                    return key;

                return key + "_" + state;
            }

            if (state == ExControlState.None)
                return key + "_" + dpi;

            return key + "_" + state + "_" + dpi;
        }

        public static Image GetImage(string key, ExControlState state, int dpi)
        {
            string name = BuildName(key, state, dpi);

            Image image = Properties.Resources.ResourceManager.GetObject(name) as Image;

            // Fallback: initial DPI
            if (image == null)
            {
                name = BuildName(key, state, 96);
                image = Properties.Resources.ResourceManager.GetObject(name) as Image;
            }

            // Fallback: normal state
            if (image == null)
            {
                name = BuildName(key, ExControlState.Normal, 96);
                image = Properties.Resources.ResourceManager.GetObject(name) as Image;
            }

            // Fallback: no state
            if (image == null)
            {
                name = BuildName(key, ExControlState.None, 96);
                image = Properties.Resources.ResourceManager.GetObject(name) as Image;
            }

            return image;
        }
    }
}

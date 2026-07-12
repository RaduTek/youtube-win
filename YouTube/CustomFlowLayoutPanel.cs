using System;
using System.Collections.Generic;
using System.Text;

namespace YouTube
{
    internal class CustomFlowLayoutPanel : System.Windows.Forms.FlowLayoutPanel
    {
        protected override System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)
        {
            return DisplayRectangle.Location;
        }
    }

}

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XLHelperLib.Helpers
{
    public static class BorderHelper
    {
        public static void SetAllBorders(IXLRange range, XLBorderStyleValues borderStyle)
        {
            range.Style.Border.OutsideBorder = borderStyle;
            range.Style.Border.InsideBorder = borderStyle;
        }
    }
}

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XLHelperLib.TableManager
{
    public class TableReader
    {
        public int GetTableStart(IXLWorksheet worksheet, int tablePosition)
        {
            var lastRowUsed = worksheet.LastRowUsed();
            int currentTablePosition = 1;
            if (lastRowUsed == null) throw new Exception("No tables in the worksheet");
            if (tablePosition == 1) return 1;
            //If found space between tables add it to the end of tables rows;
            for (int i = 1; i <= lastRowUsed.RowNumber(); i++)
                if (worksheet.Cell(i, 1).IsEmpty() && !worksheet.Cell(i + 1, 1).IsEmpty())
                {
                    currentTablePosition++;
                    if (currentTablePosition == tablePosition)
                        return i + 1;
                }
            return lastRowUsed.RowNumber() + 2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelLibrary.SpreadSheet;

namespace ExcelLibrary
{
    public class TestExcelReader
    {
        public static void readExcel()
        {
            //"C:\\Users\\pantonio\\Documents\\rcl\\in\\essa_cuenta 3 noviembre 2012.xlsx"
            Workbook wb = Workbook.Open("C:\\Users\\pantonio\\Documents\\rcl\\in\\essa_cuenta 3 noviembre 2012.xlsx");
            List<Worksheet> lws = wb.Worksheets;
            foreach (Worksheet ws in lws)
            {
                CellCollection cells = ws.Cells;
                for (int i = cells.FirstRowIndex; i < cells.LastRowIndex; i++)
                {
                    Row lrow = cells.GetRow(i);
                    for (int j = lrow.FirstColIndex; j < lrow.LastColIndex; j++)
                    {
                        System.Windows.Forms.MessageBox.Show(cells[i, j].ToString());
                    }
                }
            }
        }
    }
}

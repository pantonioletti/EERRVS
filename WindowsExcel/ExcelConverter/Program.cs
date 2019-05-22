using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ExcelConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationClass xlApp;
            Workbook wBook;
            xlApp = new ApplicationClass();
            wBook = xlApp.Workbooks.Open("C:\\Users\\pantonio\\Documents\\rcl\\in\\essa_cuenta 3 noviembre 2012.xls", //Filename
                0, //UpdateLinks 
                true, //ReadOnly true
                System.Reflection.Missing.Value, //Format 5
                System.Reflection.Missing.Value, //Password ""
                System.Reflection.Missing.Value, //WriteResPassword ""
                true, //IgnoreReadOnlyRecommended true
                System.Reflection.Missing.Value, //Origin Microsoft.Office.Interop.Excel.XlPlatform.xlWindows
                System.Reflection.Missing.Value, //Delimiter "\t"
                System.Reflection.Missing.Value, //Editable false
                false, //Notify false
	            0, //Object Converter, 0
	            false,//Object AddToMru, true
	            true, //Object Local, 1
	            0 //Object CorruptLoad 0
                );

            wBook.SaveAs("C:\\Users\\pantonio\\Documents\\rcl\\in\\pmas.xlsx",
                XlFileFormat.xlOpenXMLWorkbook,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                false,
                false,
                XlSaveAsAccessMode.xlNoChange,
                false,
                false,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value);

            wBook.Close();
            xlApp = null;
    
	
        }
    }
}

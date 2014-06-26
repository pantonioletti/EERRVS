using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoResultadoWPF
{
    class Constants
    {
        public static string DBFILE = "dbfile";
        public static string QUERY_ITEMS = "select cod, desc from items;";
        public static string QUERY_AREA = "select area, marca, agrupacion from area;";
        public static string QUERY_EERR = "select length(prefix) l, prefix, desc from eerr order by l asc, prefix asc;";
        public static string ITEMS_1 = "COD";
        public static string ITEMS_2 = "DESC";
        public static string AREA_1 = "AREA";
        public static string AREA_2 = "MARCA";
        public static string AREA_3 = "AGRUPACION";
        public static string EERR_1 = "PREFIX";
        public static string EERR_2 = "DESC";
        public static string XLCONVERT_VBS = "xls2xlsx.vbs";
        public static string INV_ITEMS = "ITEMS";
        public static string INV_LINEAS = "LINEAS";
        public static string INV_AREAS = "AREAS";
        public static string[] ARR_MONTH = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    }
}

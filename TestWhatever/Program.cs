using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestWhatever
{

    /*
     * Input:
     *  +---------------+----------+----------+
     *  |               | Input    | Output   |
     *  |   Type        | Position | Position |
     *  +---------------+----------+----------+
        | date          |    1     |  10      |
        | compte        |    2     |  11      |
        | type          |    3     |  12      |
        | comment       |    4     |  13      |
        | area          |    5     |  14*     |
        | cost_center   |    6     |  15      |
        | item          |    7     |  16*     |
        | eff_date      |    8     |  18      |
        | analisys_date |    9     |  19      |
        | reference     |   10     |  20      |
        | ref_date      |   11     |  21      |
        | exp_date      |   12     |  22      |
        | debit         |   13     |  23      |
        | credit        |   14     |  24      |
        | balance       |   15     |  25      |
        | branch        |   16     |  26      |
     *  +---------------+----------+----------+
     * 
     * Output:
        01=status
        02=company
        03=desc_area
        04=brand
        05=eerr
        06=det_eerr
        07=acct_num
        08=acct_desc
        09=month
        10=date
        11=compte
        12=type
        13=comment
        14=area
        15=cost_center
        16=item
        17=item_desc
        18=eff_date
        19=analisys_date
        20=reference
        21=ref_date
        22=exp_date
        23=debit
        24=credit
        25=balance
        26=branch
     */
    class Program
    {
        //C:\\dev\\projects\\ScriptsUtil\\data\\CUENTAS 5 OCT.csv
        const int C_IN_DATE=1;
        const int C_IN_COMPTE=2;
        const int C_IN_TYPE=3;
        const int C_IN_COMMENT=4;
        const int C_IN_AREA=5;
        const int C_IN_COST_CENTER=6;
        const int C_IN_ITEM=7;
        const int C_IN_EFF_DATE=8;
        const int C_IN_ANALISYS_DATE=9;
        const int C_IN_REFERENCE=10;
        const int C_IN_REF_DATE=11;
        const int C_IN_EXP_DATE=12;
        const int C_IN_DEBIT=13; 
        const int C_IN_CREDIT=14;     
        const int C_IN_BALANCE=15;
        const int C_IN_BRANCH = 16;

        const string C_STR_IN_HEAD = "RCL SUDAMERICANA SOCIEDAD ANONIMA";
        const string C_STR_IN_ACCOUNT = "Cuenta Contable";
        char C_COL_SEPARATOR = ';';

        const string C_ERR_MSG_FILE_FMT_ERR = "File format incorrect";

        const string C_DATA_STATUS = "REAL";
        StreamReader in_fd;
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.readCsv(args[0]);
        }

        private int readCsv(string file)
        {
            int retVal = 0;
            string errMsg = "";
            string acct = "";
            string company = "";
            try
            {
                in_fd = new StreamReader(File.Open(file, FileMode.Open));
                Console.WriteLine("File opened");
                string line = in_fd.ReadLine();
                string[] splitedline;
                if (line.Length > 0)
                {
                    splitedline = line.Split(C_COL_SEPARATOR);
                    company = splitedline[0].Trim();
                }
                else
                {
                    errMsg = C_ERR_MSG_FILE_FMT_ERR;
                    retVal = -1;

                }

                if (retVal == -1)
                {
                    Console.WriteLine(errMsg);
                    return retVal;
                }

                while (in_fd.Peek() >= 0)
                {
                    line = in_fd.ReadLine();
                    if (line.StartsWith(C_STR_IN_ACCOUNT))
                    {
                        line = line.Substring(C_STR_IN_ACCOUNT.Length);
                        splitedline = (line.Trim()).Split(' ');
                        acct = splitedline[0].Trim();
                        //Console.WriteLine(acct);
                    }
                    else
                        if (!acct.Equals(""))
                        {
                            splitedline = line.Split(C_COL_SEPARATOR);
                            if (splitedline[0].Trim().Length > 0)
                            {
                                if (splitedline.Length >= 16)
                                {
                                    StringBuilder sb = new StringBuilder(C_DATA_STATUS);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(company);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[C_IN_AREA]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(acct);
                                    Console.WriteLine(sb.ToString());
                                }
                            }
                        }
                }
                /*do{
                    line = in_fd.ReadLine();
                }
                while (!line.StartsWith(C_STR_IN_ACCOUNT) && in_fd.Peek() >= 0);
                line = line.Substring(C_STR_IN_ACCOUNT.Length);
                Console.WriteLine(line);
                splitedline = (line.Trim()).Split(' ');
                //Console.WriteLine("Cols: " + splitedline.Length);

                Console.WriteLine(acct);//"Account : " + acct);
                 * */
                /*bool firstLineRead = false;
                while (in_fd.Peek() >= 0)
                {
                    line = in_fd.ReadLine();
                    if (!firstLineRead)
                    {
                        splitedline = line.Split(';');
                        for (int i = 0; i < splitedline.Length; i++)
                        {
                            if (splitedline[i].Trim().Length > 0)
                                Console.WriteLine(splitedline[i]);
                        }
                        firstLineRead = true;
                    }

                    //Console.WriteLine(line);
                }*/
                in_fd.Close();
                Console.WriteLine("File closed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return retVal;
        }
        private Program()
        {
        }
    }
}

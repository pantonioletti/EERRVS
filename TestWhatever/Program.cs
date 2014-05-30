using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestWhatever
{
    class Program
    {
        //C:\\dev\\projects\\ScriptsUtil\\data\\CUENTAS 5 OCT.csv
        string C_STR_IN_HEAD = "RCL SUDAMERICANA SOCIEDAD ANONIMA";
        string C_STR_IN_ACCOUNT = "Cuenta Contable";
        char C_COL_SEPARATOR = ';';

        string C_ERR_MSG_FILE_FMT_ERR = "File format incorrect";
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
            try
            {
                in_fd = new StreamReader(File.Open(file, FileMode.Open));
                Console.WriteLine("File opened");
                string line = in_fd.ReadLine();
                string[] splitedline;
                if (line.Length > 0)
                {
                    splitedline = line.Split(C_COL_SEPARATOR);
                    if (!splitedline[0].Equals(C_STR_IN_HEAD))
                    {
                        errMsg = C_ERR_MSG_FILE_FMT_ERR;
                        retVal = -1;
                    }
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

                do{
                    line = in_fd.ReadLine();
                }
                while (!line.StartsWith(C_STR_IN_ACCOUNT) && in_fd.Peek() >= 0);
                line = line.Substring(C_STR_IN_ACCOUNT.Length);
                Console.WriteLine(line);
                splitedline = (line.Trim()).Split(' ');
                Console.WriteLine("Cols: " + splitedline.Length);
                //string acct = (line.Split(' '))[0].Trim();

                Console.WriteLine(splitedline[0]);//"Account : " + acct);
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

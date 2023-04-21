using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base_Atestat;

namespace Base_Atestat.Debug
{
    public class PrintConsole
    {
        public PrintConsole() {
            
        }

        public static void Intialized()
        {
            Console.WriteLine("Program Initialized!");
        }

        public static void printDB(string text)
        {
            Console.WriteLine("[DB] " + text);
        }
    }
}

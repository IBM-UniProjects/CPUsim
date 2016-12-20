using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Microprocessor mp = new Microprocessor();
            mp.PrintRegisters();
            mp.Command("MOV 0101010111111111 DX");
            Console.WriteLine("----------------------------------");
            mp.PrintRegisters();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            mp.Command("MOV DH AL");
            Console.WriteLine("----------------------------------");
            mp.PrintRegisters();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            mp.Command("ADD 00000001 DL");
            Console.WriteLine("----------------------------------");
            mp.PrintRegisters();
            Console.WriteLine("----------------------------------");
            mp.Command("ADD 1010101011111100 DX");
            Console.WriteLine("----------------------------------");
            mp.PrintRegisters();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}

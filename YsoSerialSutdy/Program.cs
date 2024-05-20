using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YsoSerialSutdy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //cmd /c calc
            //Process.Start("cmd.exe", "/c calc");

            //ObjectDataProvider
            ObjectDataProvider o = new ObjectDataProvider();
            o.MethodParameters.Add("cmd.exe");
            o.MethodParameters.Add("/c calc");
            o.MethodName = "Start";
            o.ObjectInstance = new Process();

            Console.ReadLine();
        }


    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace GMS.Utils.AssemblyInfoUtil
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public static class Program
    {
        private static string fileName = "";

        private static int incParamNum = -1;
        private static string versionStr = null;
        private static int rstParamNum = -1;

        public static void SetVersionStr(string value)
        {
                versionStr = value;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {

//#if DEBUG
//            Debugger.Break()
//#endif

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-inc:"))
                {
                    string s = args[i].Substring("-inc:".Length);
                    incParamNum = int.Parse(s);
                }
                else if (args[i].StartsWith("-set:"))
                {
                    versionStr = args[i].Substring("-set:".Length);
                }
                else if (args[i].StartsWith("-rst:"))
                {
                    string s = args[i].Substring("-rst:".Length);
                    rstParamNum = int.Parse(s);
                }
                else
                {
                    fileName = args[i];
                }
            }

            if (fileName == "")
            {
                System.Console.WriteLine("Usage: AssemblyInfoUtil <path to AssemblyInfo.cs or AssemblyInfo.vb file> [options]");
                System.Console.WriteLine("Options: ");
                System.Console.WriteLine("  -set:<new version number> - set new version number (in NN.NN.NN.NN format)");
                System.Console.WriteLine("  -inc:<parameter index>  - increases the parameter with specified index (can be from 1 to 4)");
                System.Console.WriteLine("       -inc:1 - Major version - 1.0.0.0 -> 2.0.0.0");
                System.Console.WriteLine("       -inc:2 - Minor version - 1.0.0.0 -> 1.1.0.0");
                System.Console.WriteLine("       -inc:3 - Build - 1.0.0.0 -> 1.0.1.0");
                System.Console.WriteLine("       -inc:3 - Revision - 1.0.0.0 -> 1.0.0.1");
                System.Console.WriteLine("       -inc:0 - All(secuential) - 1.3.56.65489 -> 1.3.56.65490");
                System.Console.WriteLine("  -rst:< parameter index > -Reset to 0 the parameter specified index(can be from 2 to 4)");
                System.Console.WriteLine("       - rst:2 - Minor version - 1.5648.0.0-> 1.0.0.0");
                System.Console.WriteLine("       - rst:3 - Build - 1.0.4567.0-> 1.0.0.0");
                System.Console.WriteLine("       - rst:4 - Revision - 1.0.0.4567-> 1.0.0.0");

                return;
            }

            if (!File.Exists(fileName))
            {
                System.Console.WriteLine("Error: Can not find file \"" + fileName + "\"");
                return;
            }

            System.Console.Write("Processing \"" + fileName + "\"...");

            ProcessFile.StartProcessing(fileName, incParamNum, versionStr, rstParamNum);

            System.Console.WriteLine("Done!");
        }
    }
}

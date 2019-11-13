using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GMS.Utils.AssemblyInfoUtil
{
    public static class ProcessFile
    {
        public static bool StartProcessing(string fileName, int incParamNum, string versionStr, int rstParamNum)
        {
            bool isProcessed = false;

            bool isNSIS = false;

            GetAssemblyVersionType(fileName,out isNSIS);

            StreamWriter writer = new StreamWriter(fileName + ".out",false);
            String line;

            try
            {
                var AllLines = File.ReadAllLines(fileName);

                if (AllLines.Any())
                {
                    foreach (var originLine in AllLines)
                    {
                        line = Line.ProcessLine(originLine, isNSIS, incParamNum, versionStr, rstParamNum);
                        writer.WriteLine(line);
                    }

                    writer.Close();

                    isProcessed = true;
                }
                else
                {
                    isProcessed = false;
                }

            }
            catch (Exception ex)
            {
                writer.Close();

            }

            try
            {
                File.Copy(fileName + ".out", fileName, true);
                File.Delete(fileName + ".out");

            }
            catch (Exception ex)
            {
                isProcessed = false;
            }


            return isProcessed;

        }

        private static void GetAssemblyVersionType(string fileName, out bool isNSIS)
        {
            isNSIS = false;

            if (Path.GetExtension(fileName).ToLower() == ".nsi")
                isNSIS = true;
        }

    }
}

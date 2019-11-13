using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMS.Utils.AssemblyInfoUtil
{
    public static class Line
    {
/// <summary>
/// 
/// </summary>
/// <param name="line"></param>
/// <param name="isVB"></param>
/// <param name="isNSIS"></param>
/// <param name="incParamNum"></param>
/// <param name="versionStr"></param>
/// <returns></returns>
        public static string ProcessLine(string line, bool isNSIS, int incParamNum, string versionStr, int rstParamNum)
        {
            if (isNSIS)
            {
                line = ProcessAssembyVersion.IncreaseVersion(line, incParamNum, versionStr, rstParamNum, "ASSEMBLY_VERSION \"");
                line = ProcessAssembyVersion.IncreaseVersion(line, incParamNum, versionStr, rstParamNum, "ASSEMBLY_FILE_VERSION \"");

            }
            else
            {
                line = ProcessAssembyVersion.IncreaseVersion(line, incParamNum, versionStr, rstParamNum, "AssemblyVersion(\"");
                line = ProcessAssembyVersion.IncreaseVersion(line, incParamNum, versionStr, rstParamNum, "AssemblyFileVersion(\"");
                line = ProcessAssembyVersion.IncreaseVersion(line, incParamNum, versionStr, rstParamNum, "AssemblyInformationalVersion(\"");                
            }
            return line;

        }
    }
}

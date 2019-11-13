using System;
using System.Text;

namespace GMS.Utils.AssemblyInfoUtil
{
    public static class ProcessAssembyVersion
    {
        private const int MAX = 65534;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="incParamNum"></param>
        /// <param name="versionStr"></param>
        /// <param name="patter2Search"></param>
        /// <param name="rstParamNum"></param>
        /// <param name="ignoreComments">Not change numbers on version if true</param>
        /// <returns></returns>
        public static string IncreaseVersion(string line, int incParamNum, string versionStr, int rstParamNum, string patter2Search, bool ignoreComments = true)
        {            
            int posComments = line.IndexOf("//");

            if ( (posComments >= 0 && !ignoreComments) || (posComments<0 && ignoreComments))
            {
                int spos = line.IndexOf(patter2Search);

                if (spos > 0)
                {
                    spos += patter2Search.Length;
                    int epos = line.IndexOf('"', spos);
                    string oldVersion = line.Substring(spos, epos - spos);
                    bool performChange = false;

                    string[] newVersionNums = oldVersion.Split('.');

                    Try2SetVersion(versionStr, ref performChange, ref newVersionNums);

                    Try2IncreaseAllPositions(incParamNum, ref performChange, ref newVersionNums);

                    Try2SetSpecificPosition(rstParamNum, ref performChange, ref newVersionNums);

                    line = WriteLine(line, spos, epos, performChange, newVersionNums);
                }
            }


            return line;
        }

        private static void Try2SetSpecificPosition(int rstParamNum, ref bool performChange, ref string[] newVersionNums)
        {
            if (rstParamNum > VersionEnum.Major && rstParamNum <= VersionEnum.Revision)
            {
                performChange = SetSpecificVersionPosition(rstParamNum, performChange, newVersionNums, out newVersionNums);

            }
        }

        private static void Try2IncreaseAllPositions(int incParamNum, ref bool performChange, ref string[] newVersionNums)
        {
            if (incParamNum > VersionEnum.All && incParamNum <= VersionEnum.Revision)
            {
                performChange = IncreaseAllPositionsSequential(newVersionNums, incParamNum, out newVersionNums);

            }
        }

        private static void Try2SetVersion(string versionStr, ref bool performChange, ref string[] newVersionNums)
        {
            if (!string.IsNullOrEmpty(versionStr))
            {
                performChange = SetVersion(versionStr, out newVersionNums);
            }
        }

        private static string WriteLine(string line, int spos, int epos, bool performChange, string[] newVersionNums)
        {
            StringBuilder newVersion = new StringBuilder();

            if (performChange)
            {
                newVersion.Append(newVersionNums[0]);

                for (int i = 1; i < newVersionNums.Length; i++)
                {
                    newVersion.Append("." + newVersionNums[i]);
                }

                StringBuilder str = new StringBuilder(line);
                str.Remove(spos, epos - spos);
                str.Insert(spos, newVersion.ToString());
                line = str.ToString();
            }

            return line;
        }

        private static bool IncreaseAllPositionsSequential(string[] oldVersionNums, int incParam, out string[] newVersionNums)
        {
            //If the command line parameters do not specify the index, lets begin to update all the version number, starting with the revision(0.0.0.x) until the mayor version(x.0.0.0)
            //  the max number is 65534 for each.
            // When the revision reach the 65534, then start again in 0, and the Build Number add one and so on.

            //      Major Version
            //      Minor Version 
            //      Build Number
            //      Revision 

            bool performChange = false;

            bool bolUpdateNext = true; //Always true at the begining to change the revision first.
            string[] preRelease = new string[2];

            newVersionNums = new string[oldVersionNums.Length];

            oldVersionNums.CopyTo(newVersionNums, 0);

            for (int i = incParam; i > 0; i--)
            {
                Int64 val;
                string section = oldVersionNums[i - 1];

                val = GetPreReleseRevisionNumber(oldVersionNums, ref preRelease, i, section);

                if (bolUpdateNext)
                {
                    val++;
                    bolUpdateNext = false;
                }

                if (val > MAX)
                {
                    if (i == 1)
                    {
                        throw new IndexOutOfRangeException("Max life cicle for application");
                    }
                    else
                    {
                        val = 0;
                    }

                    bolUpdateNext = true;
                }

                newVersionNums[i - 1] = val.ToString();
            }

            if (preRelease[1] != null)
            {
                newVersionNums[3] = newVersionNums[3] + "-" + preRelease[1];
            }
                        
            performChange = true;


            return performChange;
        }

        private static long GetPreReleseRevisionNumber(string[] oldVersionNums, ref string[] preRelease, int i, string section)
        {
            long val;
            if (!section.IsNumeric())
            {
                preRelease = section.Split('-');
                Int64.TryParse(preRelease[0], out val);
            }
            else
            {
                Int64.TryParse(oldVersionNums[i - 1], out val);
            }

            return val;
        }

        private static bool SetVersion(string newVersionString, out string[] newVersionNums)
        {
            bool performChange = false;

            string[] oldVersionNums = newVersionString.Split('.');

            newVersionNums = new string[oldVersionNums.Length];

            for (int i = 0; i < oldVersionNums.Length; i++)
            {
                newVersionNums[i] = oldVersionNums[i];
            }

            performChange = true;

            return performChange;

        }

        private static bool IncreaseSpecificVersionPosition(int incParamNum, bool performChange, string[] oldVersionNums, out string[] newVersionNums)
        {
           newVersionNums = new string[oldVersionNums.Length];

            if (oldVersionNums.Length >= incParamNum && oldVersionNums[incParamNum - 1] != "*")
            {
                Int64 val = Int64.Parse(oldVersionNums[incParamNum - 1]);

                val++;

                if (val >= MAX)
                {
                    val = 0;
                }

                oldVersionNums[incParamNum - 1] = val.ToString();

                for (int i = 0; i < oldVersionNums.Length; i++)
                {
                    newVersionNums[i] = oldVersionNums[i];
                }

                performChange = true;
            }

            return performChange;
        }

        private static bool SetSpecificVersionPosition(int incParamNum, bool performChange, string[] oldVersionNums, out string[] newVersionNums)
        {
            string[] preRelease = new string[2];
            newVersionNums = new string[oldVersionNums.Length];            

            if (oldVersionNums.Length >= incParamNum && oldVersionNums[incParamNum - 1] != "*")
            {
                for (int i = oldVersionNums.Length; i > 0; i--)
                {
                    string section = oldVersionNums[i-1];

                    GetPreReleseRevisionNumber(oldVersionNums, ref preRelease, i, section);

                    if (preRelease[1] != null)
                    {
                        oldVersionNums[incParamNum - 1] = 0.ToString() + "-" + preRelease[1];
                    }
                    else
                    {
                        oldVersionNums[incParamNum - 1] = 0.ToString();
                    }

                    newVersionNums[i-1] = oldVersionNums[i-1];
                }

                performChange = true;
            }

            return performChange;
        }

    }
}

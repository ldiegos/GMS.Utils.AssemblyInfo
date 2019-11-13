using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMS.Utils.AssemblyInfoUtil
{
    static class InstallShield
    {
        private static string ProcessInstallShieldLinePart(string line, int incParamNum, string versionStr, string part)
        {
            int spos = line.IndexOf(part);

            if (spos >= 0)
            {
                spos += part.Length;
                int epos = line.IndexOf('<', spos);
                string oldVersion = line.Substring(spos, epos - spos);
                string newVersion = "";
                bool performChange = false;

                if (incParamNum > 0)
                {
                    string[] nums = oldVersion.Split('.');
                    if (nums.Length >= incParamNum && nums[incParamNum - 1] != "*")
                    {
                        Int64 val = Int64.Parse(nums[incParamNum - 1]);
                        val++;
                        nums[incParamNum - 1] = val.ToString();
                        newVersion = nums[0];
                        for (int i = 1; i < nums.Length; i++)
                        {
                            newVersion += "." + nums[i];
                        }
                        performChange = true;
                    }

                }
                else if (versionStr != null)
                {
                    newVersion = versionStr;
                    performChange = true;
                }
                else
                {
                    //If the command line parameters do not specify the index, lets begin to update all the version number, starting with the revision(0.0.0.x) until the mayor version(x.0.0.0)
                    //  the max number is 65534 for each.
                    // When the revision reach the 65534, then start again in 0, and the Build Number add one and so on.

                    //      Major Version
                    //      Minor Version 
                    //      Build Number
                    //      Revision 

                    string[] nums = oldVersion.Split('.');
                    bool bolUpdateNext = true; //Always true at the begining to change the revision first.

                    if (nums.Length >= 3 && nums[2] != "*") //Avoid changing the example of Visual Studio.
                    {
                        for (int i = nums.Length; i > 0; i--)
                        {
                            Int64 val = -1;
                            Int64.TryParse(nums[i - 1], out val);

                            if (bolUpdateNext)
                            {
                                val++;
                                bolUpdateNext = false;
                            }

                            if (val >= 65534)
                            {
                                val = 0;
                                bolUpdateNext = true;
                            }

                            if (String.IsNullOrEmpty(newVersion))
                            {
                                newVersion = val.ToString();
                            }
                            else
                            {
                                newVersion = val + "." + newVersion;
                            }


                        }

                        performChange = true;
                    }

                }

                if (performChange)
                {
                    StringBuilder str = new StringBuilder(line);
                    str.Remove(spos, epos - spos);
                    str.Insert(spos, newVersion);
                    line = str.ToString();
                }
            }
            return line;
        }

        private static string ProcessInstallShieldLineProductCode(string line, string part)
        {
            int spos = line.IndexOf(part);

            if (spos >= 0)
            {
                spos += part.Length;
                int epos = line.IndexOf('}', spos);
                string oldProductCode = line.Substring(spos, epos - spos);
                var guid = System.Guid.NewGuid();

                if (!String.IsNullOrEmpty(oldProductCode))
                {

                    StringBuilder str = new StringBuilder(line);
                    str.Remove(spos, epos - spos);
                    str.Insert(spos, guid.ToString().ToUpper());
                    line = str.ToString();
                }
            }
            return line;
        }
    }
}

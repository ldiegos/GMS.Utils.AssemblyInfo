using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GMS.Utils.AssemblyInfoUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UT_ProcessFile
    {
        /*
         * The AssemblyInfo.cs file is being copied at the "Pre-build events" on the Project properties/Build-Events
         */

        [TestMethod]
        public void ProcessFile_00001_TestStartProcessingWithFileNotExist()
        {
            Assert.AreEqual(false, ProcessFile.StartProcessing("AssemblyInfo.c", 0, "",0));
        }

        [TestMethod]
        public void ProcessFile_00002_TestStartProcessingWithFileExistSetAssemblyVersion()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 0, "3.1.7.53", 0));
        }

        [TestMethod]
        public void ProcessFile_00003_TestStartProcessingWithFileExist()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 0, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00004_TestStartProcessingWithFileExistIncreaseRevision()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 4, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00005_TestStartProcessingWithFileExistIncreaseBuildNumber()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 3, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00006_TestStartProcessingWithFileExistIncreaseMinorVersion()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 2, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00007_TestStartProcessingWithFileExistIncreaseMajorVersion()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 1, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00008_TestStartProcessingWithFileExistIncreaseMajorVersion()
        {
            string[] lines = {
                "#if DEBUG"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4-PreRelease\")]"
                , "#else"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4\")]"
                , "#endif"
            };

            string fileName = "WriteLines.txt";
            bool isNSIS = false;
            int incParamNum = 4;
            string versionStr = "";
            int rstParamNum = -1;

            //string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            File.WriteAllLines(path + "\\" + fileName, lines);

            StreamWriter writer = new StreamWriter(fileName + ".out", false);
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
                }
            }
            catch (Exception ex)
            {
                writer.Close();

            }


            //Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 1, "", 0));
        }


        [TestMethod]
        public void ProcessFile_00009_TestStartProcessingWithFileExistIncreaseMajorVersion()
        {
            string[] lines = {
                "#if DEBUG"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4-PreRelease\")]"
                , "#else"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4\")]"
                , "#endif"
            };

            string fileName = "WriteLinesReset4Inc3.txt";
            bool isNSIS = false;
            int incParamNum = 3;
            string versionStr = "";
            int rstParamNum = 4;

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            File.WriteAllLines(path + "\\" + fileName, lines);

            StreamWriter writer = new StreamWriter(fileName + ".out", false);
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
                }
            }
            catch (Exception ex)
            {
                writer.Close();

            }


            //Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.cs", 1, "", 0));
        }

        [TestMethod]
        public void ProcessFile_00010_TestStartProcessingWithFileExistIncreaseMajorVersion()
        {
            string[] lines = {
                "#if DEBUG"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4-PreRelease\")]"
                , "#else"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4\")]"
                , "#endif"
            };

            string fileName = "WriteLinesInc4.txt";
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            File.WriteAllLines(path + "\\" + fileName, lines);

            int incParamNum = 4;
            string versionStr = "";
            int rstParamNum = -1;

            Assert.AreEqual(true, ProcessFile.StartProcessing(path + "\\" + fileName, incParamNum, versionStr, rstParamNum));
        }

        [TestMethod]
        public void ProcessFile_00011_TestStartProcessingWithFileExistIncreaseMajorVersion()
        {
            string[] lines = {
                "#if DEBUG"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4-PreRelease\")]"
                , "#else"
                , "[assembly: AssemblyInformationalVersion(\"1.0.0.4\")]"
                , "#endif"
            };

            string fileName = "WriteLinesInc3Rst4.txt";
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            File.WriteAllLines(path + "\\" + fileName, lines);

            int incParamNum = 3;
            string versionStr = "";
            int rstParamNum = 4;

            Assert.AreEqual(true, ProcessFile.StartProcessing(path + "\\" + fileName, incParamNum, versionStr, rstParamNum));
        }


        #region "Visual Basic"

        ///[TestMethod]
        public void ProcessFile_01001_TestStartProcessingWithVBFileExistIncreaseMajorVersion()
        {
            Assert.AreEqual(true, ProcessFile.StartProcessing(@"AssemblyInfo.vb", 1, "", 0));
        }

        #endregion "Visual Basic"


    }
}

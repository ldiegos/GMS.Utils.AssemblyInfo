using GMS.Utils.AssemblyInfoUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UT_Line
    {

        [TestMethod]
        public void UTLine_TestProcessAssemblyVersionLineWithNoInformation()
        {
            bool isNSIS = false;

            string result = Line.ProcessLine("", isNSIS, 0, "", 0);

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void UTLine_TestProcessAssemblyVersionLineWithNoInformationAndIsVB()
        {
            bool isNSIS = false;

            string result = Line.ProcessLine("", isNSIS, 0, "", 0);

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void UTLine_TestProcessAssemblyVersionLineWithCSharpAssemblyLine()
        {

            string result = Line.ProcessLine("[assembly: AssemblyVersion(\"3.1.7.53\")]", false, 4, "", -1);

            Assert.AreEqual("[assembly: AssemblyVersion(\"3.1.7.54\")]", result);
        }

        [TestMethod]
        public void UTLine_TestProcessAssemblyVersionLineWithCSharpAssemblyLineWihtIsVB()
        {

            string result = Line.ProcessLine("[assembly: AssemblyVersion(\"3.1.7.53\")]", false, 0, "", 0);

            Assert.AreNotEqual("<Assembly: AssemblyVersion(\"3.1.7.54\")>", result);
        }


        [TestMethod]
        public void UTLine_TestProcessAssemblyVersionLineWithVisualBasicLineWithIsVB()
        {

            string result = Line.ProcessLine("<Assembly: AssemblyVersion(\"3.1.7.53\")>", false, 4, "", -1);

            Assert.AreEqual("<Assembly: AssemblyVersion(\"3.1.7.54\")>", result);
        }


        [TestMethod]
        public void UTLine_TestProcessAssemblyFileVersionLineWithCSharpAssemblyLine()
        {

            string result = Line.ProcessLine("[assembly: AssemblyFileVersion(\"3.1.7.53\")]", false, 4, "", -1);

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"3.1.7.54\")]", result);
        }


        [TestMethod]
        public void UTLine_TestProcessAssemblyFileVersionLineWithIncAllAndResetRevision()
        {

            string line = "[assembly: AssemblyFileVersion(\"1.0.65534.3\")]";
            bool isNsis = false;
            int incParam = 0;
            int rstParam = 4;

            string result = Line.ProcessLine(line, isNsis, incParam, "", rstParam);

            Console.WriteLine($"Line to process: {line} #Result: {result}");
            
            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.0.65534.0\")]", result);
        }

        [TestMethod]
        public void UTLine_TestProcessAssemblyFileVersionLineWithIncBuildAndResetRevision()
        {

            string line = "[assembly: AssemblyFileVersion(\"1.0.65534.3\")]";
            bool isNsis = false;
            int incParam = 3;
            int rstParam = 4;

            string result = Line.ProcessLine(line, isNsis, incParam, "", rstParam);

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.1.0.0\")]", result);
        }



        #region NSIS

        [TestMethod]
        public void UTLine_ProcessLine_NSIS_LineWithNoInformationAndIsNSIS()
        {
            bool isNSIS = true;

            string result = Line.ProcessLine("", isNSIS, -1, "", -1);

            Assert.AreEqual("", result);
        }
        
        [TestMethod]
        public void UTLine_ProcessLine_NSIS_ASSEMBLY_VERSIONLineSetVersionIsNSISdefine()
        {
            bool isNSIS = true;

            string line = "!define ASSEMBLY_VERSION \"1.0.0.225\"";

            string result = Line.ProcessLine(line, isNSIS, -1, "3.1.7.54", -1);

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("!define ASSEMBLY_VERSION \"3.1.7.54\"", result);
        }

        [TestMethod]
        public void UTLine_ProcessLine_NSIS_ASSEMBLY_VERSIONLineSetVersionIsNSISStrCopy()
        {
            bool isNSIS = true;
            string line = "StrCpy $ASSEMBLY_VERSION \"1.0.0.270\"";

            string result = Line.ProcessLine(line, isNSIS, -1, "3.1.7.54", -1);

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("StrCpy $ASSEMBLY_VERSION \"3.1.7.54\"", result);
        }

        [TestMethod]
        public void UTLine_ProcessLine_NSIS_ASSEMBLY_FILE_VERSIONLineSetVersionIsNSISdefine()
        {
            bool isNSIS = true;

            string line = "!define ASSEMBLY_FILE_VERSION \"1.0.0.225\"";

            string result = Line.ProcessLine(line, isNSIS, -1, "3.1.7.54", -1);

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("!define ASSEMBLY_FILE_VERSION \"3.1.7.54\"", result);
        }

        [TestMethod]
        public void UTLine_ProcessLine_NSIS_ASSEMBLY_FILE_VERSIONLineSetVersionStrCopy()
        {
            bool isNSIS = true;
            string line = "StrCpy $ASSEMBLY_FILE_VERSION \"1.0.0.270\"";

            string result = Line.ProcessLine(line, isNSIS, -1, "3.1.7.54",-1);

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("StrCpy $ASSEMBLY_FILE_VERSION \"3.1.7.54\"", result);
        }



        #endregion NSIS

    }
}

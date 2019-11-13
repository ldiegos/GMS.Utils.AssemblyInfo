using System;
using GMS.Utils.AssemblyInfoUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UT_ProcessAssembyVersion
    {

        #region VisualBasicLines
        [TestMethod]
        public void UTProcessAssembly_10001_TestAssemblyVersionSetVersionVisualBasic()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("<Assembly: AssemblyVersion(\"1.0.0.0\")>", -1, "3.1.7.53", -1, "AssemblyVersion(\"");
            Assert.AreEqual("<Assembly: AssemblyVersion(\"3.1.7.53\")>", result);
        }

        #endregion VisualBasicLines

        #region NSISLines
        [TestMethod]
        public void UTProcessAssembly_10001_TestAssemblyVersionSetVersioNSISdefine()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("!define ASSEMBLY_VERSION \"1.0.0.225\"", -1, "3.1.7.53", -1, "ASSEMBLY_VERSION \"");
            Assert.AreEqual("!define ASSEMBLY_VERSION \"3.1.7.53\"", result);
        }

        [TestMethod]
        public void UTProcessAssembly_10002_TestAssemblyVersionSetVersioNSISStrCpy()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("StrCpy $ASSEMBLY_VERSION \"1.0.0.270\"", -1, "3.1.7.53", -1, "$ASSEMBLY_VERSION \"");
            Assert.AreEqual("StrCpy $ASSEMBLY_VERSION \"3.1.7.53\"", result);
        }

        


        #endregion NSISNLines


        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpWithNoPatternToSearch()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", -1, "", -1, "");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpWithNoPatternToSearchAndIncreaseRevision()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", 4, "", -1, "");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpWithNoPatternToSearchAndIncreaseRevisionSetAssemblyFileVersion()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.0.0\")]", 2, "3.1.7.53", -1, "");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.0.0.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpIncreaseMinorVersionSetAssemblyFileVersion()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.0.0\")]", 2, "3.1.7.53", -1, "AssemblyFileVersion(\"");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"3.2.7.53\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpWithNoLineToSearch()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("", 0, "", 0, "AssemblyFileVersion(\"");

            Assert.AreNotEqual("[assembly: AssemblyFileVersion(\"1.0.0.0\")]", result);
        }


        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpExampleFromVisualStudioWithLastAsterisk()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", -1, "", -1, "AssemblyFileVersion(\"");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.0.*.*\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionCSharpExampleFromVisualStudioWithThreeAsterisk()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.*.*.*\")]", -1, "", -1, "AssemblyFileVersion(\"");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"1.*.*.*\")]", result);
        }

        
        [TestMethod]
        public void UTProcessAssembly_TestAssemblyFileVersionSetFileVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyFileVersion(\"1.0.0.0\")]", -1, "3.1.7.53", -1, "AssemblyFileVersion(\"");

            Assert.AreEqual("[assembly: AssemblyFileVersion(\"3.1.7.53\")]", result);

        }
        
        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", -1, "3.1.7.53", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"3.1.7.53\")]", result);

        }

        /// <summary>
        /// This test, will ignore the "inc: 5" and increase the Revision as if the parameter were "inc: 4"
        /// </summary>
        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseNotExistantAsemblyVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", 5, "", -1, "AssemblyVersion(\"");

            Assert.AreNotEqual("[assembly: AssemblyVersion(\"1.0.0.1\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseRevisionAsemblyVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", 4, "", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.1\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseBuildNumberAsemblyVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", 3, "", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.0\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMinorVersionAsemblyVersionCSharpToCSharpVariable()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", 2, "", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.0.0\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMajorVersion()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.0.0\")]", 1, "", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"2.0.0.0\")]", result);
        }

        


        //[TestMethod]
        //public void UTProcessAssembly_TestAssemblyVersionIncreaseRevisionAndBuildNumberMinorVersionWithIncAll()
        //{
        //    string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.0.65534.65534\")]", 0, "", -1, "AssemblyVersion(\"");

        //    Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.0.0\")]", result);
        //}

        //[TestMethod]
        //public void UTProcessAssembly_TestAssemblyVersionIncreaseRevisionAndBuildNumberMinorVersionMajorVersionWithIncAll()
        //{
        //    string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"1.65534.65534.65534\")]", 0, "", -1, "AssemblyVersion(\"");

        //    Assert.AreEqual("[assembly: AssemblyVersion(\"2.0.0.0\")]", result);
        //}


        #region WithIncParameters

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseRevisionNormally()
        {
            string line = "[assembly: AssemblyVersion(\"1.0.0.56\")]";
            string setVersion = "";
            int incParam = 4;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");
            
            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.57\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseRevisionTop()
        {
            string line = "[assembly: AssemblyVersion(\"1.0.0.65534\")]";
            string setVersion = "";
            int incParam = 4;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseBuildNormally()
        {
            string line = "[assembly: AssemblyVersion(\"1.0.20.59\")]";
            string setVersion = "";
            int incParam = 3;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.21.59\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseBuildTop()
        {
            string line = "[assembly: AssemblyVersion(\"1.0.65534.45\")]";
            string setVersion = "";
            int incParam = 3;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.0.45\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMinorNormally()
        {
            string line = "[assembly: AssemblyVersion(\"1.56.20.78\")]";
            string setVersion = "";
            int incParam = 2;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.57.20.78\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMinorTop()
        {
            string line = "[assembly: AssemblyVersion(\"1.65534.20.85\")]";
            string setVersion = "";
            int incParam = 2;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"2.0.20.85\")]", result);
        }


        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMajorNormally()
        {
            string line = "[assembly: AssemblyVersion(\"23.56.20.56987\")]";
            string setVersion = "";
            int incParam = 1;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"24.56.20.56987\")]", result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Max life cicle for application")]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseMajorTop()
        {
            string line = "[assembly: AssemblyVersion(\"65534.56.20.56987\")]";
            string setVersion = "";
            int incParam = 1;
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.56.20.56987\")]", result);
        }

        #endregion WithIncParameters


        #region WithSetAndIncParameters

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndIncreaseRevision()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            string setVersion = "1.0.0.56";
            int incParam = 4;
            int rstParam = -1;
            
            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");
            
            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.57\")]", result);
        }


        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndIncreaseBuild()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            string setVersion = "1.0.0.56";
            int incParam = 3;            
            int rstParam = -1;
            
            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.56\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndIncreaseMinor()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]", 2, "1.0.0.56", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.0.56\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndIncreaseMajor()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]", 1, "1.0.0.56", -1, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"2.0.0.56\")]", result);
        }


        //[TestMethod]
        //public void UTProcessAssembly_TestAssemblyVersionSetAndIncreaseLastValueWithIncAll()
        //{
        //    string result = ProcessAssembyVersion.IncreaseVersion("[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]", 4, "1.0.0.65534", -1, "AssemblyVersion(\"");

        //    Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.0\")]", result);
        //}


        #endregion WithSetAndIncParameters
        
        #region WithResetParameter

        [TestMethod]
        public void UTProcessAssembly_00001_TestAssemblyVersionResetRevision()
        {
            string line = "[assembly: AssemblyVersion(\"1.1.1.57\")]";

            string result = ProcessAssembyVersion.IncreaseVersion(line, -1, "", 4, "AssemblyVersion(\"");

            Console.WriteLine($"Line to change: {line}# Result of change: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.1.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_00002_TestAssemblyVersionResetBuild()
        {
            string line = "[assembly: AssemblyVersion(\"1.1.1.57\")]";

            string result = ProcessAssembyVersion.IncreaseVersion(line, -1, "", 3, "AssemblyVersion(\"");

            Console.WriteLine($"Line to change: {line}# Result of change: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.1.0.57\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_00003_TestAssemblyVersionResetMinor()
        {
            string line = "[assembly: AssemblyVersion(\"1.1.1.57\")]";

            string result = ProcessAssembyVersion.IncreaseVersion(line, -1, "", 2, "AssemblyVersion(\"");

            Console.WriteLine($"Line to change: {line}# Result of change: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.57\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestToFailAssemblyVersionResetMajor()
        {
            string line = "[assembly: AssemblyVersion(\"5.1.1.57\")]";

            string result = ProcessAssembyVersion.IncreaseVersion(line, -1, "", 1, "AssemblyVersion(\"");

            Console.WriteLine($"Line to change: {line}# Result of change: {result}");

            Assert.AreNotEqual("[assembly: AssemblyVersion(\"0.1.1.57\")]", result);
        }

        #endregion WithResetParameter


        #region WithSetAndResetParameter

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndResetRevision()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = -1;
            string setVersion = "1.0.0.56";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.0\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndResetBuild()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = -1;
            string setVersion = "1.0.2.56";
            int rstParam = 3;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.56\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndResetMinor()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = -1;
            string setVersion = "1.2.2.56";
            int rstParam = 2;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.2.56\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetAndResetMajorIgnored()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = -1;
            string setVersion = "1.2.2.56";
            int rstParam = 1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.2.2.56\")]", result);

        }

        #endregion WithSetAndResetParameter

        #region WithIncAndResetParameter
        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncRevisionAndResetRevisionNormally()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.5897\")]";
            int incParam = 4;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56987.15479.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncRevisionAndResetRevisionTop()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.65534\")]";
            int incParam = 4;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56987.15480.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncBuildAndResetRevisionNormally()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.5897\")]";
            int incParam = 3;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56987.15480.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncBuildAndResetRevisionTop()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.65534\")]";
            int incParam = 3;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56987.15480.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncMinorAndResetRevisionNormally()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.5897\")]";
            int incParam = 2;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56988.15479.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncMinorAndResetRevisionTop()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.65534\")]";
            int incParam = 2;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"45.56988.15479.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncMajorAndResetRevisionNormally()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.5897\")]";
            int incParam = 1;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");
            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"46.56987.15479.0\")]", result);
        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionIncMajorAndResetRevisionTop()
        {
            string line = "[assembly: AssemblyVersion(\"45.56987.15479.65534\")]";
            int incParam = 1;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");
            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"46.56987.15479.0\")]", result);
        }

        #endregion WithIncAndResetParameter

        #region WithSetIncAndResetParameter

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetIncRevisionAndResetRevision()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = 4;
            string setVersion = "1.0.0.56";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.0\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetIncBuildAndResetRevision()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = 3;
            string setVersion = "1.0.0.56";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.1.0\")]", result);

        }

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionSetIncMinorAndResetRevision()
        {
            string line = "[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]";
            int incParam = 2;
            string setVersion = "1.1.0.56";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.2.0.0\")]", result);

        }

        #endregion WithSetIncAndResetParameter
        [TestMethod]
        public void UTProcessAssembly_TestAssemblyVersionWithPreReleaseIncRevision()
        {
            string line = "[assembly: AssemblyVersion(\"1.0.0.9-PreRelease\")]";
            int incParam = 4;
            string setVersion = "";
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyVersion(\"1.0.0.10-PreRelease\")]", result);

        }

        #region maintainPrerelease

        [TestMethod]
        public void UTProcessAssembly_TestAssemblyInformationalVersionWithPreReleaseIncRevision()
        {
            string line = "[assembly: AssemblyInformationalVersion(\"1.0.0.25-PreRelease\")]";
            int incParam = 4;
            string setVersion = "";
            int rstParam = -1;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyInformationalVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyInformationalVersion(\"1.0.0.26-PreRelease\")]", result);

        }


        [TestMethod]
        public void UTProcessAssembly_TestAssemblyInformationalVersionWithIncBuildResetRevisionWithPreRelease()
        {
            string line = "[assembly: AssemblyInformationalVersion(\"1.0.0.4-PreRelease\")]";
            int incParam = 3;
            string setVersion = "";
            int rstParam = 4;

            string result = ProcessAssembyVersion.IncreaseVersion(line, incParam, setVersion, rstParam, "AssemblyInformationalVersion(\"");

            Console.WriteLine($"Line to process: {line} #Result: {result}");

            Assert.AreEqual("[assembly: AssemblyInformationalVersion(\"1.0.1.0-PreRelease\")]", result);

        }

        

        #endregion maintainPrerelease       


        #region WithForceCommentsChanged

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Max life cicle for application")]
        public void UTProcessAssembly_TestAssemblyVersionIncreaseLastValueOnCommented()
        {
            string result = ProcessAssembyVersion.IncreaseVersion("//[assembly: AssemblyVersion(\"65534.65534.65534.65534\")]", 4, "", -1, "AssemblyVersion(\"",false);

            Assert.AreEqual("//[assembly: AssemblyVersion(\"1.0.0.0\")]", result);
        }

        #endregion WithForceCommentsChanged

    }

}

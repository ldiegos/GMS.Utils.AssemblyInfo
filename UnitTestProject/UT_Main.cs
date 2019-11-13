using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMS.Utils.AssemblyInfoUtil;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UT_Main
    {
        public UT_Main()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Main_00001_LaunchMainWithSet()
        {
            string[] args = { @"AssemblyInfo.cs", "-set:1000.1.7.53" };

            Program.Main(args);
        }

        [TestMethod]
        public void Main_00001_LaunchMainWithInc()
        {
            string[] args = { @"AssemblyInfo.cs", "-inc:4" };

            Program.Main(args);
        }

        [TestMethod]
        public void Main_00001_LaunchMainWithIncBuildAndResetRevision()
        {
            string[] args = { @"AssemblyInfo.cs", "-inc:3", "-rst:4" };
            Program.Main(args);
        }


        //[TestMethod]
        //public void Main_00001_LaunchMainResetValuesINCMax()
        //{
        //    //const int MAX = 65534;
        //    const int MAX = 2;

        //    string[] args = { @"AssemblyInfo.cs", "-set:1.0.0.65533-PreRelease" };
        //    Program.Main(args);

        //    Program.SetVersionStr(null);

        //    string[] args2 = { @"AssemblyInfo.cs", "-inc:4" };
        //    for (int i = 0; i <= MAX; i++)
        //    {
        //        Program.Main(args2);
        //    } 
        //}


        [TestMethod]
        public void Main_00001_LaunchMainWithSetAndInc()
        {
            string[] args = { @"AssemblyInfo.cs", "-set:1.0.0.65533-PreRelease", "-inc:4" };
            Program.Main(args);
        }


       
    }
}


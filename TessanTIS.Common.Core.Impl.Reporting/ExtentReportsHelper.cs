using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;

namespace TessanTIS.Common.Core.Impl.Reporting
{
    public class ExtentReportsHelper : IReportHelper
    {
        private ILoggerHelper logg;
        public static ExtentReportsHelper extentReportHelper { get; set; }
        public ExtentReports Extent { get; set; }
        public ExtentSparkReporter Reporter { get; set; }
        public ExtentTest Test { get; set; }
        public Dictionary<string, ExtentTest> TestsByTestName;

        private ExtentReportsHelper()
        {
            TestsByTestName = new Dictionary<string, ExtentTest>();
            logg = LoggerHelper.GetInstance();
            Extent = new ExtentReports();
            if (
                File.Exists(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "\\index.html"
                    )
                )
            )
            {
                File.Delete(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "\\index.html"
                    )
                );
            }
            if (
                File.Exists(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "\\executionResult.xml"
                    )
                )
            )
            {
                File.Delete(
                    Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "\\executionResult.xml"
                    )
                );
            }
            Reporter = new ExtentSparkReporter(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "ExtentReports.html"
                )
            );
            Reporter.Config.DocumentTitle = "Automation Testing Report";
            Reporter.Config.ReportName = "Regression Testing";
            Reporter.Config.Theme = Theme.Standard;
            Extent.AttachReporter(Reporter);
            Extent.AddSystemInfo(
                "Application Under Test",
                ConfigurationHelper.Config["ApplicationUnderTest"]
            );
            Extent.AddSystemInfo("Enviroment", "QA");
            Extent.AddSystemInfo("Machine", Environment.MachineName);
            Extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        public void AddTestFailureScreenshot(IWebDriver Driver)
        {
            string path = CaptureScreenShot(Driver);
            Test.AddScreenCaptureFromPath(path, "Screenshot on Error: ");
        }

        private string CaptureScreenShot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            string finalpth =
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                        + "\\ErrorScreen"
                ) + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            ts.GetScreenshot().SaveAsFile(localpath);
            return localpath;
        }

        public void Close()
        {
            Extent.Flush();
            XmlReportHelper.SaveReport(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                        + "\\executionResult.xml"
                )
            );
            logg.Info("///////////////////   End Test   //////////////////////");
        }

        public void CreateTest(string testName)
        {
            if (TestsByTestName.ContainsKey(testName))
                Test = TestsByTestName[testName];
            else
            {
                Test = Extent.CreateTest(testName);
                TestsByTestName.Add(testName, Test);
            }
            XmlReportHelper.AddTest(testName);
            logg.Info("///////////////////   Start Test: " + testName + "  //////////////////////");
        }

        public void EndStep(string testName, int stepNumber)
        {
            Test.Log(Status.Info, $"Step {stepNumber}: End");
            XmlReportHelper.EndStep(testName, stepNumber);
            logg.Info("///////////////////   End Step: " + stepNumber + "  //////////////////////");
        }

        public void Error(string message)
        {
            Test.Log(Status.Error, message);
        }

        public void Info(string message)
        {
            Test.Log(Status.Info, message);
        }

        public void SetStepStatusBlocked(string testName, int stepNumber, string stepDescription)
        {
            Test.Log(Status.Warning, stepDescription);
            XmlReportHelper.SetStepStatus(
                testName,
                stepNumber,
                Models.Status.Blocked,
                stepDescription
            );
        }

        public void SetStepStatusFail(string testName, int stepNumber, string stepDescription)
        {
            Test.Log(Status.Fail, stepDescription);
            XmlReportHelper.SetStepStatus(
                testName,
                stepNumber,
                Models.Status.Failed,
                stepDescription
            );
        }

        public void SetStepStatusPass(string testName, int stepNumber, string stepDescription)
        {
            Test.Log(Status.Pass, stepDescription);
            XmlReportHelper.SetStepStatus(
                testName,
                stepNumber,
                Models.Status.Passed,
                stepDescription
            );
        }

        public void SetStepStatusWarning(string testName, int stepNumber, string stepDescription)
        {
            Test.Log(Status.Warning, stepDescription);
            XmlReportHelper.SetStepStatus(
                testName,
                stepNumber,
                Models.Status.Passed,
                stepDescription
            );
        }

        public void SetTestStatusBlocked(string testName)
        {
            Test.Warning("Test Blocked!");
            XmlReportHelper.SetTestStatus(testName, Models.Status.Blocked);
        }

        public void SetTestStatusFail(string testName, string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += message;
            }
            Test.Fail(printMessage);
            XmlReportHelper.SetTestStatus(testName, Models.Status.Failed);
        }

        public void SetTestStatusPass(string testName)
        {
            Test.Pass("Test Executed Succefully!");
            XmlReportHelper.SetTestStatus(testName, Models.Status.Passed);
        }

        public void SetTestStatusSkipped(string testName)
        {
            Test.Skip("Test Skiped!");
            XmlReportHelper.SetTestStatus(testName, Models.Status.NotCompleted);
        }

        public void SetTestStatusWarning(string testName)
        {
            Test.Warning("Warning, Test did not complet!");
            XmlReportHelper.SetTestStatus(testName, Models.Status.Warning);
        }

        public void StartStep(string testName, int stepNumber)
        {
            Test.Log(Status.Info, $"Step {stepNumber}: Start");
            XmlReportHelper.EndStep(testName, stepNumber);
            logg.Info(
                "///////////////////   Start Step: " + stepNumber + "  //////////////////////"
            );
        }

        public static ExtentReportsHelper GetInstance()
        {
            if (extentReportHelper == null)
                extentReportHelper = new ExtentReportsHelper();
            return extentReportHelper;
        }
    }
}

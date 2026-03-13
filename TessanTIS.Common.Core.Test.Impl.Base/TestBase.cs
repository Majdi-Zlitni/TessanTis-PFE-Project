using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data.Modles;
using TessanTIS.Common.Core.Impl.Reporting;
using TessanTIS.Common.Core.Impl.WebDriver;
using Unity;

namespace TessanTIS.Common.Core.Test.Impl.Base
{
    [TestFixture]
    public abstract class TestBase
    {
        //Comment
        // test base class
        protected IBrowserHelper browser;
        protected IReportHelper extent;
        protected ILoggerHelper logging;
        protected UnityContainer unityContainer;
        protected string TestName => TestContext.CurrentContext.Test.Name;
        public IList<TestCaseWorkflow> ExecutionWorkflowList;
        public string BaseUrl;
        public string ApplicationVersion;
        public string DeviceName { get; set; }
        public static string AppV { get; set; }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                string testName = null;
                TestStatus status;
                testName = TestContext.CurrentContext.Test.Name;
                status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessgae = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
                switch (status)
                {
                    case TestStatus.Failed:
                        extent.SetTestStatusFail(
                            testName,
                            $"<br>{errorMessgae}<br>Stack Trace: <br>{stacktrace}<br>"
                        );
                        extent.AddTestFailureScreenshot(browser.Driver);
                        break;
                    case TestStatus.Skipped:
                        extent.SetTestStatusSkipped(testName);
                        extent.AddTestFailureScreenshot(browser.Driver);
                        break;
                    case TestStatus.Inconclusive:
                        extent.SetTestStatusBlocked(testName);
                        extent.AddTestFailureScreenshot(browser.Driver);
                        break;
                    default:
                        extent.SetTestStatusPass(testName);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                browser.Close();
            }
        }

        [OneTimeTearDown]
        public void CloseAll()
        {
            try
            {
                extent.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [OneTimeSetUp]
        public void SetUpReporter()
        {
            extent = ExtentReportsHelper.GetInstance();
            logging = LoggerHelper.GetInstance();
        }

        [SetUp, Order(1)]
        public void StartUpTest()
        {
            extent.CreateTest(TestContext.CurrentContext.Test.Name);
            InitializeParameters();
            if (browser == null)
            {
                if (!ApplicationVersion.Contains("Mobile"))
                {
                    browser = new SeleniumBrowserHelper(extent);
                }
                browser.init();
            }
        }

        private void InitializeParameters()
        {
            extent.Info("Start Initialize Parameters");
            BaseUrl = "http://automationpractice.com/"; // TestContext.Parameters["baseUrl"];
            extent.Info($"BaseUrl={BaseUrl}");
            ApplicationVersion =
                TestContext.Parameters["applicationVersion"] == null
                    ? string.Empty
                    : TestContext.Parameters["applicationVersion"];
            AppV = "V1"; // ApplicationVersion;
            extent.Info($"ApplicationVersion={AppV}");
            DeviceName = TestContext.Parameters["deviceName"];
            extent.Info($"DeviceName={DeviceName}");
            extent.Info("End Initialize Parameters");
            unityContainer = new UnityContainer();
        }

        public abstract void ExecuteAction(int stepNumber, string actionKeyWord);

        public void ExecuteWorkflow()
        {
            foreach (
                int stepNumber in ExecutionWorkflowList
                    .Select(x => x.StepNumber)
                    .Distinct()
                    .OrderBy(y => y)
            )
            {
                extent.StartStep(TestName, stepNumber);
                foreach (
                    TestCaseWorkflow testCaseWorkflow in ExecutionWorkflowList
                        .Where(x => x.StepNumber == stepNumber)
                        .OrderBy(y => y.Order)
                )
                {
                    if (testCaseWorkflow.Execute)
                    {
                        ExecuteAction(stepNumber, testCaseWorkflow.Action);
                    }
                    else
                    {
                        extent.Info(
                            $"{testCaseWorkflow.Action} action : The execution is desactivated"
                        );
                    }
                }
                extent.EndStep(TestName, stepNumber);
            }
        }
    }
}

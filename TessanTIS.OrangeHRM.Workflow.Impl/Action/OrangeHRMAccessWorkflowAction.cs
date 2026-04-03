using System;
using System.Reflection;
using NUnit.Framework;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Pages.Impl;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Base;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Action
{
    public class OrangeHRMAccessWorkflowAction : WorkflowBase, IOrangeHRMAccessWorkflowAction
    {
        private readonly string baseUrl;
        private readonly IHomePage homePage = null;
        private readonly ILoginPage loginPage = null;

        public OrangeHRMAccessWorkflowAction(
            IBrowserHelper browserHelper,
            IReportHelper extent,
            string baseUrl
        )
        {
            this.browser = browserHelper;
            this.extent = extent;
            this.baseUrl = baseUrl;
            if (homePage == null)
                homePage = new HomePage(browser, extent);
            if (loginPage == null)
                loginPage = new LoginPage(browserHelper, extent, null);
        }

        public void OpenAutomationPracticeWebSite(int stepNumber)
        {
            try
            {
                browser.Goto(baseUrl);
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void OpenOrangeHRMWebSite(int stepNumber)
        {
            try
            {
                browser.Goto(baseUrl);
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void AccessToLoginPage(int stepNumber)
        {
            try
            {
                homePage.ClickSignIn(stepNumber);
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void AccessToWomenTShirt(int stepNumber)
        {
            try
            {
                homePage.ClickWomenTShirt(stepNumber, "T-shirts");
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }
    }
}

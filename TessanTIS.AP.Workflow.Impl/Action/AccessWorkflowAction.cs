using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.AP.Pages.Abstraction;
using TessanTIS.AP.Pages.Impl;
using TessanTIS.AP.Workflow.Abstraction;
using TessanTIS.AP.Workflow.Impl.Base;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.AP.Workflow.Impl.Action
{
    public class AccessWorkflowAction : WorkflowBase, IAccessWorkflowAction
    {
        private readonly string baseUrl;
        private readonly IHomePage homePage = null;
        private readonly ILoginPage loginPage = null;

        public AccessWorkflowAction(IBrowserHelper browserHelper, IReportHelper extent, string baseUrl)
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

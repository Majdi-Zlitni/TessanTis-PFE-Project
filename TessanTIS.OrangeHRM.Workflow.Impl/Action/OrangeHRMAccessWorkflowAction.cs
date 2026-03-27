using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Pages.Impl;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Base;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Action
{
    public class OrangeHRMAccessWorkflowAction : WorkflowBase, IOrangeHRMAccessWorkflowAction
    {
        private readonly IHomePage homePage = null;
        private readonly ILoginPage loginPage = null;

        public OrangeHRMAccessWorkflowAction(IBrowserHelper browserHelper, IReportHelper extent)
        {
            this.browser = browserHelper;
            this.extent = extent;
            if (homePage == null)
                homePage = new HomePage(browser, extent);
            if (loginPage == null)
                loginPage = new LoginPage(browserHelper, extent, null);
        }

        public void OpenAutomationPracticeWebSite(int stepNumber)
        {
            try
            {
                browser.Goto("https://opensource-demo.orangehrmlive.com");
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


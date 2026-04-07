using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.OrangeHRM.Data.Impl;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Base;
using Unity;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Verification
{
    public class OrangeHRMWorkflowVerification : WorkflowBase, IOrangeHRMWorkflowVerification
    {
        private readonly IProfilPage profilPage;
        private readonly ILoginPage loginPage;
        private readonly IPimPage pimPage;

        public OrangeHRMWorkflowVerification(
            IBrowserHelper browser,
            IReportHelper extent,
            OrangeHRMData OrangeHRMData,
            ILoggerHelper logg
        )
        {
            this.browser = browser;
            this.extent = extent;
            this.logging = logg;
            SetUpWorkflow(browser, extent, OrangeHRMData, logg);
            profilPage = unityContainer.Resolve<IProfilPage>();
            loginPage = unityContainer.Resolve<ILoginPage>();
            pimPage = unityContainer.Resolve<IPimPage>();
        }

        public void VerifyOpenOrangeHRMWebSiteSuccessfully(int stepNumber)
        {
            try
            {
                bool isLoginButtonDisplayed = loginPage.IsLoginButtonDisplayed(stepNumber);
                Assert.IsTrue(isLoginButtonDisplayed, "Login button is not displayed");
                extent.Info("Verified successfully that OrangeHRM website is opened");
            }
            catch (Exception ex)
            {
                extent.Error("Failed to verify that OrangeHRM website is opened");
                throw;
            }
        }

        public void VerifyUserLoggedInSuccessfully(int stepNumber)
        {
            try
            {
                bool isHeaderDisplayed = profilPage.IsDashboardHeaderDisplayed(stepNumber);
                bool isLogoDisplayed = profilPage.IsBrandLogoDisplayed(stepNumber);
                bool isSidebarDashboardDisplayed = profilPage.IsSidebarDashboardDisplayed(
                    stepNumber
                );
                string pageTitle = profilPage.GetPageTitle(stepNumber);

                if (
                    isHeaderDisplayed
                    && isLogoDisplayed
                    && isSidebarDashboardDisplayed
                    && pageTitle == "OrangeHRM"
                )
                {
                    extent.SetStepStatusPass(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        "Dashboard verified successfully after login"
                    );
                    logging.Info("Dashboard verified successfully after login");
                }
                else
                {
                    extent.SetStepStatusFail(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Dashboard verification failed after login. Header={isHeaderDisplayed}, Logo={isLogoDisplayed}, Sidebar={isSidebarDashboardDisplayed}, Title={pageTitle}"
                    );
                    logging.Error(
                        $"Dashboard verification failed after login. Header={isHeaderDisplayed}, Logo={isLogoDisplayed}, Sidebar={isSidebarDashboardDisplayed}, Title={pageTitle}"
                    );
                }
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

        public void VerifyNavigateToPimSuccessfully(int stepNumber)
        {
            try
            {
                bool isAddEmployeeTabDisplayed = pimPage.IsAddEmployeeTabDisplayed(stepNumber);
                Assert.IsTrue(isAddEmployeeTabDisplayed, "Add Employee tab is not displayed");
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void VerifyEmployeeAddedSuccessfully(int stepNumber)
        {
            try
            {
                bool isSuccessToastDisplayed = pimPage.IsSuccessToastDisplayed(stepNumber);
                Assert.IsTrue(isSuccessToastDisplayed, "Success toast is not displayed");
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }
    }
}

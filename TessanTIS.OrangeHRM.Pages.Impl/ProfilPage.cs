using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Pages.Impl.Base;

namespace TessanTIS.OrangeHRM.Pages.Impl
{
    public class ProfilPage : PageBase, IProfilPage
    {
        private readonly By brandLogo = By.CssSelector("img[alt='client brand banner']");
        private readonly By dashboardHeader = By.XPath("//h6[text()='Dashboard']");
        private readonly By sidebarDashboard = By.XPath("//span[text()='Dashboard']");
        private readonly By userNameText = By.XPath(
            "//*[@id=\"header\"]/div[2]/div/div/nav/div[1]/a"
        );

        public ProfilPage(
            IBrowserHelper browserHelper,
            IReportHelper reportHelper,
            ILoggerHelper logg
        )
        {
            this.browserHelper = browserHelper;
            this.reportHelper = reportHelper;
            this.loggerHelper = logg;
            loggerHelper = LoggerHelper.GetInstance();
        }

        public string GetUserName(int stepNumber)
        {
            try
            {
                return browserHelper.GetText(userNameText);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public bool IsBrandLogoDisplayed(int stepNumber)
        {
            try
            {
                return browserHelper.ElementIsDisplayed(brandLogo);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public bool IsDashboardHeaderDisplayed(int stepNumber)
        {
            try
            {
                return browserHelper.ElementIsDisplayed(dashboardHeader);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public bool IsSidebarDashboardDisplayed(int stepNumber)
        {
            try
            {
                return browserHelper.ElementIsDisplayed(sidebarDashboard);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public string GetPageTitle(int stepNumber)
        {
            try
            {
                return browserHelper.Driver.Title;
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }
    }
}


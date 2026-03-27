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
    }
}


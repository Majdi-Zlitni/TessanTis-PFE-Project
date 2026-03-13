using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TessanTIS.AP.Pages.Abstraction;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Pages.Impl.Base;

namespace TessanTIS.AP.Pages.Impl
{
    public class SearchPage : PageBase, ISearchPage
    {
        private readonly By searchResult = By.XPath(
            "//*[@id=\"center_column\"]/ul/li/div/div[2]/h5/a"
        );

        public SearchPage(
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

        public string GetSearchResultName(int stepNumber)
        {
            try
            {
                return browserHelper.GetText(searchResult);
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

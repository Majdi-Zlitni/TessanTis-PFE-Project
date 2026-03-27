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
    public class HomePage : PageBase, IHomePage
    {
        private readonly By signInButton = By.XPath(
            "//*[@id=\"header\"]/div[2]/div/div/nav/div[1]/a"
        );
        private readonly By womenField = By.LinkText("WOMEN");
        private readonly By tshirtItem = By.XPath(
            "//div[@id='block_top_menu']/ul/li[1]/ul/li[1]/ul//a[@title='T-shirts']"
        );

        public HomePage(IBrowserHelper browser, IReportHelper extent)
        {
            this.browserHelper = browser;
            this.reportHelper = extent;
        }

        public void ClickSignIn(int stepNumber)
        {
            try
            {
                browserHelper.ClickButton(signInButton);
                reportHelper.Info("ClickSignIn OK");
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

        public void ClickWomenTShirt(int stepNumber, string field)
        {
            try
            {
                browserHelper.HoverAndClickElement(womenField, tshirtItem);
                reportHelper.Info("ClickWomenTShirt OK");
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


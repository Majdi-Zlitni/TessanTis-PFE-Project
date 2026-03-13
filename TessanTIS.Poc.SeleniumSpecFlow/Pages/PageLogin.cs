using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Impl.SeleniumHelper;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.Poc.SeleniumSpecFlow.Pages
{
    class PageLogin
    {
        #region SetUp
        readonly private IWebDriver driver = null;
        readonly private BrowseHelpers browser = null;
        public PageLogin(IWebDriver d)
        {
            this.driver = d;
            browser = new BrowseHelpers(d);
        }
        #endregion
        #region Element Definition
        
        readonly string loginByLinkText = "Sign In";
        readonly string emailByID = "emailOrUsername";
        readonly string passwordByID = "password";
        readonly string loginClickByCssLocator = "button[type=submit]";
        readonly string userNameByClass = "username";
        #endregion
        #region Function Definition
        public void GoToURL(string url)=> browser.GoToURL(url);
        public void ConnectToLoginPage() => browser.ClickButtom(Locator.ByHyperLinks, loginByLinkText);
        public void EnterUserName(string input) => browser.SetText(Locator.ById, emailByID, input);
        public void EnterPassword(string input) => browser.SetText(Locator.ById, passwordByID, input);    
        public void ClickLogin()=> browser.ClickButtom(Locator.ByCssLocator, loginClickByCssLocator);      
        public string ActualUser() =>  browser.GetText(Locator.ByClassName, userNameByClass);
     
        #endregion
    }
}
        

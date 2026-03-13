using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.SeleniumHelper;

namespace TessanTIS.Poc.SeleniumSpecFlow.Pages
{
    class PageSearch
    {
        #region SetUp
        readonly private IWebDriver driver = null;
        readonly private BrowseHelpers browser = null;
        public PageSearch(IWebDriver d)
        {
            this.driver = d;
            browser = new BrowseHelpers(d);
        }
        #endregion
        #region Element Definition

        readonly string explorByClassName = "tab-nav-link discover-new-link";
        readonly string inputSearchById = "main-search-search-input";
        readonly string buttomSearchById = "main-search-search-submit";
        readonly string resultSearchByClass="d-none d-md-block";
        #endregion

        #region Function Definition
        public void ExploreTheCourses() => browser.ClickButtom(Locator.ByClassName, explorByClassName);
        public void EnterInputSearch(string input) => browser.SetText(Locator.ById, inputSearchById, input);
        public void ClickSearch() => browser.ClickButtom(Locator.ById, buttomSearchById);
        public string ResultSearch() => browser.GetText(Locator.ByClassName, resultSearchByClass);
      
        #endregion
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Impl.SeleniumHelper;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.Poc.Selenium.Pages
{
   class HomePageWitBrowser
    {
        //########## Setup ############
        readonly private IWebDriver driver = null;
        readonly private BrowseHelpers browser = null;
        public HomePageWitBrowser(IWebDriver d)
        {
            this.driver = d;
            browser = new BrowseHelpers(d);
        }

        //########### Element Definition #############
        readonly string xPathImmo = "//*[@id=\"__next\"]/div/div[2]/div[2]/div[3]/div[1]/a[2]/div/div[2]";
        readonly string idShop = "cta-shops";
        readonly string XpathButtomEnvoyer = "//*[@id=\"__next\"]/div/div[2]/div/form/div[2]/button";
        readonly string idLogin = "login-tayara-phone";
        readonly string idSeConnecter = "cta-signin";
        //######### Function Definition #################
        // Go To Url
        public void GoToURL(string url)
        {
            browser.GoToURL(url);
        }
        //Immobilier

        public void ClickImmo()
        {
            browser.ClickButtom(Locator.ByXPath, xPathImmo);
        }


        //Shop pros

        public void ClickShops()
        {
            browser.ClickButtom(Locator.ById, idShop);
        }
       //Login by phone number
        
        public void LoginByPhoneNumber()
        {
            browser.ClickButtom(Locator.ById, idSeConnecter);

            browser.SetText(Locator.ById, idLogin, "51469141");

            browser.ClickButtom(Locator.ByXPath, XpathButtomEnvoyer);
    
        }


    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Poc.Selenium.Utility;

namespace TessanTIS.Poc.Selenium.Pages
{
    class Home
    {
        //########## Setup ############
        private readonly IWebDriver driver = null;
        private readonly Util util = null;
        public Home(IWebDriver d)
        {
            this.driver = d;
            util = new Util(d);
        }
        //########### Element Definition #############

        private readonly By shopsXpath = By.Id("cta-shops");
        private readonly By seConnecterXpath = By.Id("cta-signin");
        private readonly By loginTayara = By.Id("login-tayara-phone");
        private readonly By envoyerXpath = By.XPath("//*[@id=\"__next\"]/div/div[2]/div/form/div[2]/button");
        private readonly By immoXpath = By.XPath("//*[@id=\"__next\"]/div/div[2]/div[2]/div[3]/div[1]/a[2]/div/div[2]");
        //######### Function Definition #################
     
        public void ClickShops()
        {
             util.ClickElement(shopsXpath);
        }
        public void ClickSeConnecter()
        {
             util.ClickElement(seConnecterXpath);
        }
        public void ClickEnvoyer()
        {
             util.ClickElement(envoyerXpath);
        }
        public void LoginByPhoneNumber()
        {
            ClickSeConnecter();
            util.SendInput("20448112", loginTayara);
            ClickEnvoyer();
           
        }
        public void ClickImmo()
        {
             util.ClickElement(immoXpath);
        }
        public void GoToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.FullScreen();
        }
    }
}

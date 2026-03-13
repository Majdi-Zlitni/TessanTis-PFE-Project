
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TessanTIS.Poc.Selenium.Pages;
using TessanTIS.Common.Core.Impl.SeleniumHelper;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.Poc.Selenium
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            //initialize
            IWebDriver webDriver = new ChromeDriver();
             Home home = new Home(webDriver);
           // HomePageWitBrowser home = new HomePageWitBrowser(webDriver);
            string url = "https://www.tayara.tn/";

            //test Home Page
            home.GoToURL(url);
       

            //// Go To Immo
           home.ClickImmo();
            
            ////Go to Shops
           home.GoToURL(url);
            home.ClickShops();
            ////Go TO Connexion page
            home.GoToURL(url);
   
           home.LoginByPhoneNumber();
          


        }
    }
}

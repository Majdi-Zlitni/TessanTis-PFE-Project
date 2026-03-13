using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using TessanTIS.Poc.SeleniumSpecFlow.Pages;

namespace TessanTIS.Poc.SeleniumSpecFlow.StepsDefinitions
{
    [Binding]
    class LoginSteps
    {
        readonly IWebDriver webDriver = new ChromeDriver();
        readonly PageLogin pageLogin = null;
        
        public LoginSteps() => pageLogin = new PageLogin(webDriver);
      
        readonly string url = "https://www.edx.org/";
        [Given("I am on the Home page")]
        public void GivenIamInHomePage()
        {
            pageLogin.GoToURL(url);
            Thread.Sleep(2000);
        }
        [Given("I click the Sign In option")]
        public void WhenIClickSignIn() => pageLogin.ConnectToLoginPage();
       

        [When("I log in login as '(.*)' and password as '(.*)'")]
        public void WhenIEnterUserNameAndPassword(string userName, string password)
        {
            pageLogin.EnterUserName(userName);        
            pageLogin.EnterPassword(password);
            pageLogin.ClickLogin();

        }
        [Then("I am logged in as '(.*)'")]

        public void ThenILogged(string userName)
        {
            string actualUser = pageLogin.ActualUser();
            Assert.IsTrue(actualUser.Contains(userName));


        }


    }
}

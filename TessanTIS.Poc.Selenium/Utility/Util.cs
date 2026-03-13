using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Poc.Selenium.Utility
{
    class Util
    {
        private readonly IWebDriver driver = null;
        public Util(IWebDriver d)
        {
            driver = d;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

    
        public IWebElement WaitForElementVisible(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }


        public bool ClickElement(By locator)
        {
            bool returnValue = false;
            try
            {
                WaitForElementVisible(locator).Click();
                returnValue = true;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + locator + "not found on page "+e.Message + driver.Title);
                returnValue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown error " + e.Message + " occurred on page " + driver.Title);
                returnValue = false;
            }
            return returnValue;
        }
        public void SendInput(string input, By locator)
        {
            WaitForElementVisible(locator).SendKeys(input);
        }
        public bool IsElementVisible(By locator)
        {
            bool returnValue = false;
            try
            {
                returnValue = WaitForElementVisible(locator).Displayed;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + locator + "not found on page "+ e.Message + driver.Title);
                returnValue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown error " + e.Message + " occurred on page " + driver.Title);
                returnValue = false;
            }
            return returnValue;
        }
    }
}

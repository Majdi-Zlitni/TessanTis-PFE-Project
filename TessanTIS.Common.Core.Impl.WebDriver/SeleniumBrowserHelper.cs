using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Protractor;
using SeleniumExtras.WaitHelpers;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;

namespace TessanTIS.Common.Core.Impl.WebDriver
{
    public class SeleniumBrowserHelper : IBrowserHelper
    {
        private readonly IReportHelper extentReportsHelper;
        private readonly ILoggerHelper logger;

        private readonly string browser;
        private IWebDriver webDriver;
        private WebDriverWait webDriverWait;
        private NgWebDriver ngWebDriver;

        public SeleniumBrowserHelper(IReportHelper reportsHelper)
        {
            browser = ConfigurationHelper.Config["DataConfig:browser"];
            extentReportsHelper = reportsHelper;
            logger = LoggerHelper.GetInstance();
        }

        public IWebDriver Driver
        {
            get { return webDriver; }
        }
        public string Title
        {
            get { return webDriver.Title; }
        }

        public WebDriverWait Wait
        {
            get { return webDriverWait; }
        }

        public bool AngularElementIsDisplayed(By element)
        {
            try
            {
                ngWebDriver.WaitForAngular();
                return ngWebDriver.FindElement(element).Displayed;
            }
            catch (Exception ex)
            {
                logger.Error($"{MethodBase.GetCurrentMethod().Name} crashed ! {ex.Message}");
                return false;
            }
        }

        public void ClearText(By by)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                IWebElement inputWebElement = webDriverWait.Until(
                    ExpectedConditions.ElementIsVisible(by)
                );
#pragma warning restore CS0618 // ExpectedConditions is obsolete
                inputWebElement.Clear();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
        }

        public void ClickButton(By button)
        {
            try
            {
                // ngWebDriver.WaitForAngular();
                IWebElement buttonWebElement = webDriverWait.Until(
                    ExpectedConditions.ElementIsVisible(button)
                );
                buttonWebElement.Click();
                //ngWebDriver.WaitForAngular();
            }
            catch (ElementClickInterceptedException ex)
            {
                logger.Error(
                    "ClickButton crashed when Clicking on button xpath : "
                        + button
                        + "ElementClickInterceptedException message is :  "
                        + ex.Message
                        + "Retreying to ClcikButton an other Time"
                );
                this.ElementIsVisible(button);
                IWebElement buttonWebElement = ngWebDriver.FindElement(button);
                buttonWebElement.Click();
                ngWebDriver.WaitForAngular();
            }
        }

        public void ClickButtonUsingJavaScript(By by)
        {
            try
            {
                ngWebDriver.WaitForAngular();
                IWebElement btnElement = ngWebDriver.FindElement(by);
                string javascript = "arguments[0].click()";
                ngWebDriver.ExecuteScript(javascript, btnElement);
                ngWebDriver.WaitForAngular();
            }
            catch (StaleElementReferenceException ex)
            {
                logger.Error(
                    "ClickButtonUsingJavaScript crashed when Clicking on button xpath : "
                        + by
                        + "StaleElementReferenceException message is :  "
                        + ex.Message
                        + "Retreying to Click another Time"
                );
                this.ElementIsVisible(by);
                ngWebDriver.WaitForAngular();
                IWebElement btnElement = ngWebDriver.FindElement(by);
                string javascript = "arguments[0].click()";
                ngWebDriver.ExecuteScript(javascript, btnElement);
                ngWebDriver.WaitForAngular();
            }
        }

        public void ClickButtonUsingSendKey(By button)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                IWebElement buttonWebElement = webDriverWait.Until(
                    ExpectedConditions.ElementToBeClickable(button)
                );
#pragma warning restore CS0618 // ExpectedConditions is obsolete
                buttonWebElement.SendKeys(Keys.Return);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
        }

        public void ClickFirstFromList(By list, string text)
        {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
            IWebElement webElement = webDriverWait
                .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(list))
                .FirstOrDefault(x => x.Text == text);
#pragma warning restore CS0618 // ExpectedConditions is obsolete
            webElement.Click();
        }

        public void ClickFirstFromListUsingSendKey(By list, string text)
        {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
            IWebElement webElement = webDriverWait
                .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(list))
                .FirstOrDefault(x => x.Text == text);
#pragma warning restore CS0618 // ExpectedConditions is obsolete
            webElement.SendKeys(Keys.Return);
            Thread.Sleep(2000);
        }

        public void ClickLinkText(By linktext)
        {
            try
            {
                ngWebDriver.WaitForAngular();
                ngWebDriver.FindElement(linktext).Click();
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
        }

        public void ClickOnCellValueInTable(By table, By cell)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                IWebElement tableWebElement = webDriverWait.Until(
                    ExpectedConditions.ElementIsVisible(table)
                );
#pragma warning restore CS0618 // ExpectedConditions is obsolete
                IWebElement cellWebElemnt = tableWebElement.FindElement(cell);
                cellWebElemnt.Click();
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
        }

        public void Close()
        {
            webDriverWait = null;
            webDriver.Quit();
            webDriver.Dispose();
            extentReportsHelper.Info($"Browser closed.");
        }

        public void DoubleClickOnButton(By button)
        {
            ngWebDriver.WaitForAngular();
            IWebElement buttonWebElement = ngWebDriver.FindElement(button);
            Actions actions = new Actions(ngWebDriver);
            actions.DoubleClick(buttonWebElement).Perform();
        }

        public bool ElementExist(By element)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                return webDriverWait.Until(ExpectedConditions.ElementExists(element)).Displayed;
#pragma warning restore CS0618 // ExpectedConditions is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool ElementIsDisplayed(By element)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                return webDriverWait.Until(ExpectedConditions.ElementIsVisible(element)).Displayed;
#pragma warning restore CS0618 // ExpectedConditions is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool ElementIsDisplayedAfterAJAX(By element)
        {
            try
            {
                WaitForAjax();
                return webDriver.FindElement(element).Displayed;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool ElementIsEnabled(By element)
        {
            try
            {
#pragma warning disable CS0618 // ExpectedConditions is obsolete
                return webDriverWait.Until(ExpectedConditions.ElementIsVisible(element)).Enabled;
#pragma warning restore CS0618 // ExpectedConditions is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool ElementIsVisible(By element)
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.ElementExists(element));
#pragma warning restore CS0618 // Type or member is obsolete
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );

                return false;
            }
        }

        public void ElementToBeClickable(By element)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            webDriverWait.Until(ExpectedConditions.ElementToBeClickable(element));
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void EnterKey()
        {
            try
            {
                Actions builder = new Actions(Driver);
                builder.SendKeys(Keys.Return).Build().Perform();
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                extentReportsHelper.Info($"{MethodBase.GetCurrentMethod().Name} crashed");
                throw;
            }
        }

        public IWebElement FindElement(By by)
        {
            IWebElement Element = null;
            try
            {
                Element = webDriver.FindElement(by);
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
            return Element;
        }

        public IWebElement FindElements(By by)
        {
            IWebElement tableWebElement = null;
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                tableWebElement = webDriverWait.Until(ExpectedConditions.ElementIsVisible(by));
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
            return tableWebElement;
        }

        public IWebElement FindElementWithExist(By by)
        {
            IWebElement WebElement = null;
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                WebElement = webDriverWait.Until(ExpectedConditions.ElementExists(by));
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
            return WebElement;
        }

        public string GetAlertMessage()
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.AlertIsPresent());
#pragma warning restore CS0618 // Type or member is obsolete
                IAlert alert = Driver.SwitchTo().Alert();
                string msg = alert.Text;
                try
                {
                    alert.Accept();
                }
                catch (Exception ex)
                {
                    extentReportsHelper.Info("GetAlertMessage crached excption : " + ex.Message);
                }
                return msg;
            }
            catch (Exception ex)
            {
                extentReportsHelper.Info(
                    $"{MethodBase.GetCurrentMethod().Name} crashed ! {ex.Message}"
                );
                return "No PopUp was displayed";
            }
        }

        public string GetAttribute(By element, string attribute)
        {
            string attributValue = string.Empty;
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                attributValue = webDriverWait
                    .Until(ExpectedConditions.ElementExists(element))
                    .GetAttribute(attribute);
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
            return attributValue;
        }

        public List<IWebElement> GetListOfElements(By by)
        {
            List<IWebElement> tableWebElement = null;
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                tableWebElement = webDriverWait
                    .Until(ExpectedConditions.ElementIsVisible(by))
                    .FindElements(by)
                    .ToList();
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
            return tableWebElement;
        }

        public List<IWebElement> GetListOfExistingElements(By by)
        {
            List<IWebElement> tableWebElement = null;
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                tableWebElement = webDriverWait
                    .Until(ExpectedConditions.ElementExists(by))
                    .FindElements(by)
                    .ToList();
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
            return tableWebElement;
        }

        public string GetPopUpMessage()
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.AlertIsPresent());
#pragma warning restore CS0618 // Type or member is obsolete
                return Driver.SwitchTo().Alert().Text;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return "No PopUp was displayed";
            }
        }

        public string GetProperty(By element, string property)
        {
            ngWebDriver.WaitForAngular();
#pragma warning disable CS0618 // Type or member is obsolete
            return webDriverWait
                .Until(ExpectedConditions.ElementExists(element))
                .GetDomProperty(property);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public int GetRowsCount(By by)
        {
            ngWebDriver.WaitForAngular();
            return ngWebDriver.FindElements(by).Count;
        }

        public string GetText(By textInput)
        {
            try
            {
                //ngWebDriver.WaitForAngular();
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.ElementExists(textInput));
#pragma warning restore CS0618 // Type or member is obsolete
                return webDriver.FindElement(textInput).Text;
            }
            catch (StaleElementReferenceException ex)
            {
                logger.Error(
                    "GetText crashed when getting text by this xpath : "
                        + textInput
                        + "StaleElementRefereExceptinceon message is :  "
                        + ex.Message
                        + "Retreying to getText an other Time"
                );
                this.ElementIsVisible(textInput);
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.ElementExists(textInput));
#pragma warning restore CS0618 // Type or member is obsolete
                return ngWebDriver.FindElement(textInput).Text;
            }
        }

        public void Goto(string url)
        {
            webDriver.Url = url;
            logger.Info($"Browser navigated to the url [{url}].");
            extentReportsHelper.Info($"Browser navigated to the url [{url}].");
        }

        public bool HyperlinkTobeClickable(By element)
        {
            try
            {
                bool testHyperlink = false;

                var tableWebElement = webDriver.FindElements(element);

                if (tableWebElement != null)
                {
                    testHyperlink = true;
                }
                return testHyperlink;
            }
            catch (NoSuchElementException ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public void init()
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException(
                    "Browser discovery via registry is only supported on Windows."
                );

            List<Browser> browserList = BrowserUtility.GetBrowsers();
            switch (browser)
            {
                case "IE":

                    InternetExplorerOptions options = new InternetExplorerOptions();
                    webDriver = new InternetExplorerDriver(
                        InternetExplorerDriverService.CreateDefaultService(),
                        options,
                        TimeSpan.FromMinutes(3)
                    );
                    webDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));
                    break;

                case "Chrome":
                    ChromeOptions chromeoptions = new ChromeOptions();
                    webDriver = new ChromeDriver(
                        ChromeDriverService.CreateDefaultService(),
                        chromeoptions,
                        TimeSpan.FromMinutes(3)
                    );

                    ngWebDriver = new NgWebDriver(webDriver);
                    break;

                case "Firefox":
                    webDriver = new FirefoxDriver();
                    break;

                default:
                    webDriver = new InternetExplorerDriver();
                    break;
            }
            webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
            extentReportsHelper.Info("Browser started.");
            webDriver.Manage().Window.Maximize();
            extentReportsHelper.Info("Browser maximized.");
        }

        public bool InvisibilityOfElementLocated(By element)
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
#pragma warning restore CS0618 // Type or member is obsolete
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool IsAlertDisappear()
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                return webDriverWait.Until(ExpectedConditions.AlertState(false));
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public bool IsElementClickable(By element)
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                webDriverWait.Until(ExpectedConditions.ElementToBeClickable(element));
#pragma warning restore CS0618 // Type or member is obsolete
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                return false;
            }
        }

        public void MouseMoveUpDown(string direction)
        {
            try
            {
                Actions builder = new Actions(Driver);
                if (direction == "Up")
                    builder.SendKeys(Keys.Up).Build().Perform();
                else
                    builder.SendKeys(Keys.Down).Build().Perform();
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                extentReportsHelper.Info($"{MethodBase.GetCurrentMethod().Name} crashed");
                throw;
            }
        }

        public void PressEnterKey()
        {
            Actions actions = new Actions(webDriver);
            actions.SendKeys(Keys.Enter);
        }

        public void PressESC()
        {
            Actions actions = new Actions(webDriver);
            actions.SendKeys(Keys.Escape);
        }

        public void Refresh(string FrameName)
        {
            webDriver.Navigate().Refresh();
            logger.Info($"Page was Refreshed");
            extentReportsHelper.Info($"Page was Refreshed");
            webDriver.SwitchTo().Frame(FrameName);
        }

        public int ReturnColumnIndex(string column, string xPath)
        {
            int columnIndex = 0;
            int colCount = 0;
            if (xPath.Contains("thead"))
            {
                colCount = GetRowsCount(By.XPath(xPath + "/tr/th"));
                //find date colum index
                for (int i = 1; i <= colCount; i++)
                {
                    string columnName = GetText(By.XPath(xPath + "/tr[1]/th[" + i + "]"));
                    if (string.Equals(columnName, column, StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = i;
                        break;
                    }
                }
            }
            else
            {
                colCount = GetRowsCount(By.XPath(xPath + "/tr/td"));
                //find date colum index
                for (int i = 1; i <= colCount; i++)
                {
                    string columnName = GetText(By.XPath(xPath + "/tr[1]/td[" + i + "]"));
                    if (string.Equals(columnName, column, StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = i;
                        break;
                    }
                }
            }

            return columnIndex;
        }

        public void ScrollDown(By by)
        {
            try
            {
                IWebElement ScrollElement = webDriver.FindElement(by);
                Actions actions = new Actions(webDriver);
                actions.MoveToElement(ScrollElement).Perform();
                actions.Click(ScrollElement).Perform();
                actions.KeyDown(Keys.Shift);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("outside the bounds of the current view port"))
                {
                    logger.Error(
                        $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                    );
                }
                else
                {
                    logger.Error(
                        $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                    );
                    extentReportsHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed");
                    throw;
                }
            }
        }

        public void SelectDropDownElementByTextValue(By drpdownElement, string text)
        {
            var dropdown = webDriver.FindElement(drpdownElement);
            var select = new SelectElement(dropdown);
            select.SelectByText(text);
        }

        public void SelectElementFromListByProperty(string property, string value)
        {
            try
            {
                var ListItems = GetListOfExistingElements(By.XPath("//li")).ToList();
                string itemInnertext = string.Empty;
                foreach (var item in ListItems)
                {
                    itemInnertext = item.GetDomProperty(property);
                    if (itemInnertext.Contains(value))
                    {
                        Actions actions = new Actions(webDriver);
                        actions.Click(item).Perform();
                        logger.Info("Item is selected from the list : " + itemInnertext);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                throw;
            }
        }

        public void SetDatePcker(By by, Tuple<string, string> data)
        {
            throw new NotImplementedException();
        }

        public void SetText(By textInput, string text)
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                IWebElement inputWebElement = webDriverWait.Until(
                    ExpectedConditions.ElementIsVisible(textInput)
                );
#pragma warning restore CS0618 // Type or member is obsolete
                inputWebElement.Clear();
                inputWebElement.SendKeys(text);
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
                extentReportsHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed " + ex.Message
                );
                throw;
            }
        }

        public void SetTextUsingSendKeys(By textInput, string text)
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                IWebElement LocalMatInput = webDriverWait.Until(
                    ExpectedConditions.ElementExists(textInput)
                );
#pragma warning restore CS0618 // Type or member is obsolete
                LocalMatInput.Clear();
                LocalMatInput.SendKeys(text);
            }
            catch (Exception ex)
            {
                logger.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception Message is: {ex.Message} Exception Stacktrace is {ex.StackTrace}"
                );
            }
        }

        public bool TextContainsNumber(string text)
        {
            bool isNumber = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    isNumber = true;
                    break;
                }
            }
            return isNumber;
        }

        public string WaintUntilGetText(By textInput)
        {
            throw new NotImplementedException();
        }

        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)
                    (ngWebDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(100);
            }
        }

        public void WaitForElementToBeVisible(By by)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            webDriverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void ClickRadio(By by)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            IWebElement radio = webDriverWait.Until(ExpectedConditions.ElementExists(by));
#pragma warning restore CS0618 // Type or member is obsolete
            radio.Click();
        }

        public void HoverAndClickElement(By listElement, By listItem)
        {
            Actions actions = new Actions(webDriver);
            IWebElement womenTab = webDriver.FindElement(listElement);
            IWebElement TshirtTab = webDriver.FindElement(listItem);
            actions.MoveToElement(womenTab).MoveToElement(TshirtTab).Click().Perform();
        }
    }
}

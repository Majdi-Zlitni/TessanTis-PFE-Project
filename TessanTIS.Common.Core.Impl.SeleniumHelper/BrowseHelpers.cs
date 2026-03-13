using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.Common.Core.Impl.SeleniumHelper
{
    public class BrowseHelpers : IBrowserHelpers
    {
        public IWebDriver Driver => throw new NotImplementedException();

        public WebDriverWait Wait => throw new NotImplementedException();

        public void AngularElementIsDisplayed(By element)
        {
            throw new NotImplementedException();
        }

        public void ClearText(By by)
        {
            throw new NotImplementedException();
        }

        public void ClickButton(By button)
        {
            throw new NotImplementedException();
        }

        public void ClickButtonUsingJavaScript(By table)
        {
            throw new NotImplementedException();
        }

        public void ClickButtonUsingSendKey(By button)
        {
            throw new NotImplementedException();
        }

        public void ClickFirstFromList(By list, string text)
        {
            throw new NotImplementedException();
        }

        public void ClickFirstFromListUsingSendKey(By list, string text)
        {
            throw new NotImplementedException();
        }

        public void ClickLinkText(By by)
        {
            throw new NotImplementedException();
        }

        public void ClickOnCellValueInTable(By table, By cell)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DoubleClickOnButton(By button)
        {
            throw new NotImplementedException();
        }

        public bool ElementExist(By by)
        {
            throw new NotImplementedException();
        }

        public bool ElementIsDisplayed(By element)
        {
            throw new NotImplementedException();
        }

        public bool ElementIsDisplayedAfterAJAX(By element)
        {
            throw new NotImplementedException();
        }

        public bool ElementIsEnabled(By table, By cell)
        {
            throw new NotImplementedException();
        }

        public bool ElementIsVisible(By element)
        {
            throw new NotImplementedException();
        }

        public void ElementToBeClickable(By element)
        {
            throw new NotImplementedException();
        }

        public void EnterKey()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElementWithExist(By by)
        {
            throw new NotImplementedException();
        }

        public string GetAlertMessage()
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(By pageInfo, string v)
        {
            throw new NotImplementedException();
        }

        public List<IWebElement> GetListOfElements(By by)
        {
            throw new NotImplementedException();
        }

        public List<IWebElement> GetListOfExistingElements(By by)
        {
            throw new NotImplementedException();
        }

        public string GetPopUpMessage()
        {
            throw new NotImplementedException();
        }

        public string GetProperty(By element, string property)
        {
            throw new NotImplementedException();
        }

        public int GetRowsCount(By by)
        {
            throw new NotImplementedException();
        }

        public string GetText(By textInput)
        {
            throw new NotImplementedException();
        }

        public void Goto(string url)
        {
            throw new NotImplementedException();
        }

        public bool HyperlinkTobeClickable(By by)
        {
            throw new NotImplementedException();
        }

        public void init()
        {
            throw new NotImplementedException();
        }

        public bool InvisibilityOfElementLocated(By by)
        {
            throw new NotImplementedException();
        }

        public bool IsAlertDisappear()
        {
            throw new NotImplementedException();
        }

        public bool IsElementClickable(By by)
        {
            throw new NotImplementedException();
        }

        public void MouseMoveUpDown(string v)
        {
            throw new NotImplementedException();
        }

        public void PressEnterKey()
        {
            throw new NotImplementedException();
        }

        public void PressESC()
        {
            throw new NotImplementedException();
        }

        public void Refresh(string FrameName)
        {
            throw new NotImplementedException();
        }

        public int ReturnColumnIndex(string column, string xPath)
        {
            throw new NotImplementedException();
        }

        public void ScrollDown(By by)
        {
            throw new NotImplementedException();
        }

        public void SelectDropDownElementByTextValue(By by, string data)
        {
            throw new NotImplementedException();
        }

        public void SelectElementFromListByProperty(string property, string value)
        {
            throw new NotImplementedException();
        }

        public void SetDatePcker(By by, Tuple<string, string> data)
        {
            throw new NotImplementedException();
        }

        public void SetText(By textInput, string text)
        {
            throw new NotImplementedException();
        }

        public void SetTextUsingSendKeys(By materialFilter, string v)
        {
            throw new NotImplementedException();
        }

        public bool TextContainsNumber(string text)
        {
            throw new NotImplementedException();
        }

        public string WaintUntilGetText(By textInput)
        {
            throw new NotImplementedException();
        }

        public void WaitForAjax()
        {
            throw new NotImplementedException();
        }

        public void WaitForElementToBeVisible(By element)
        {
            throw new NotImplementedException();
        }
    }
}

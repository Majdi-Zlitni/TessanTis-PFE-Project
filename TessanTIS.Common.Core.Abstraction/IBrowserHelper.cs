using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Abstraction
{
    public interface IBrowserHelper
    {
        public IWebDriver Driver { get; }
        public WebDriverWait Wait { get; }
        public void ClickButton(By button);
        public void ClickButtonUsingSendKey(By button);
        public void ClickFirstFromList(By list, string text);
        public void ClickFirstFromListUsingSendKey(By list, string text);
        public void ClickOnCellValueInTable(By table, By cell);
        public void Close();
        public bool ElementIsDisplayed(By element);
        public bool ElementIsEnabled(By element);
        public string GetPopUpMessage();
        public string GetText(By textInput);
        public string WaintUntilGetText(By textInput);
        public void Goto(string url);
        public void init();
        public string GetAlertMessage();
        public string GetAttribute(By pageInfo, string v);
        public void SetText(By textInput, string text);
        public void SetTextUsingSendKeys(By materialFilter, string v);
        public void ClickButtonUsingJavaScript(By table);
        public void ElementToBeClickable(By element);
        public void ClickLinkText(By by);
        public void SelectDropDownElementByTextValue(By by, string data);
        public bool ElementIsVisible(By element);
        public void PressEnterKey();
        public void WaitForElementToBeVisible(By element);
        public bool AngularElementIsDisplayed(By element);
        public int GetRowsCount(By by);
        public bool ElementExist(By by);
        public string GetProperty(By element, string property);
        public bool InvisibilityOfElementLocated(By by);
        public void SetDatePcker(By by, Tuple<string, string> data);
        public void ClearText(By by);
        public void WaitForAjax();
        public int ReturnColumnIndex(string column, string xPath);
        public bool ElementIsDisplayedAfterAJAX(By element);
        public void MouseMoveUpDown(string v);
        public void PressESC();
        public bool TextContainsNumber(string text);
        public void EnterKey();
        public IWebElement FindElements(By by);
        public void Refresh(string FrameName);
        public bool IsElementClickable(By by);
        public bool HyperlinkTobeClickable(By by);
        public void DoubleClickOnButton(By button);
        public List<IWebElement> GetListOfElements(By by);
        public bool IsAlertDisappear();
        public IWebElement FindElement(By by);
        public void SelectElementFromListByProperty(string property, string value);
        public IWebElement FindElementWithExist(By by);
        public List<IWebElement> GetListOfExistingElements(By by);
        public void ScrollDown(By by);
        void ClickRadio(By by);
        void HoverAndClickElement(By listElement, By listItem);
    }
}

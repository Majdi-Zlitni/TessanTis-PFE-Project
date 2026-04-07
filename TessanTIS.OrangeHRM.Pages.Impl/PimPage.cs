using OpenQA.Selenium;
using System;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Pages.Impl.Base;
using TessanTIS.OrangeHRM.Pages.Abstraction;

namespace TessanTIS.OrangeHRM.Pages.Impl
{
    public class PimPage : PageBase, IPimPage
    {
        private readonly By pimMenuLink = By.CssSelector("a[href*='viewPimModule']");
        private readonly By addEmployeeTab = By.XPath("//a[text()='Add Employee']");
        private readonly By firstNameInput = By.Name("firstName");
        private readonly By middleNameInput = By.Name("middleName");
        private readonly By lastNameInput = By.Name("lastName");
        private readonly By employeeIdInput = By.XPath("//label[text()='Employee Id']/parent::div/following-sibling::div/input");
        private readonly By imageUploadInput = By.CssSelector("input[type='file']");
        private readonly By saveButton = By.CssSelector("button[type='submit']");
        private readonly By successToast = By.CssSelector(".oxd-toast--success");

        public PimPage(IBrowserHelper browserHelper, IReportHelper reportHelper, ILoggerHelper loggerHelper) 
        {
            this.browserHelper = browserHelper;
            this.reportHelper = reportHelper;
            this.loggerHelper = loggerHelper;
        }

        public void NavigateToPim(int stepNumber)
        {
            try
            {
                browserHelper.Driver.FindElement(pimMenuLink).Click();
                reportHelper.Info("Navigated to PIM module");
                loggerHelper.Info("Navigated to PIM module");
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"Step {stepNumber}: Failed to navigate to PIM module. Exception: {ex.Message}");
                throw;
            }
        }

        public void ClickAddEmployeeTab(int stepNumber)
        {
            try
            {
                browserHelper.Driver.FindElement(addEmployeeTab).Click();
                reportHelper.Info("Clicked Add Employee Tab");
                loggerHelper.Info("Clicked Add Employee Tab");
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"Step {stepNumber}: Failed to click Add Employee Tab. Exception: {ex.Message}");
                throw;
            }
        }

        public void SetFirstName(string firstName, int stepNumber)
        {
            try
            {
                browserHelper.SetText(firstNameInput, firstName);
                reportHelper.Info($"Set First Name: {firstName}");
            }
            catch (Exception) { throw; }
        }

        public void SetMiddleName(string middleName, int stepNumber)
        {
            try
            {
                browserHelper.SetText(middleNameInput, middleName);
            }
            catch (Exception) { throw; }
        }

        public void SetLastName(string lastName, int stepNumber)
        {
            try
            {
                browserHelper.SetText(lastNameInput, lastName);
            }
            catch (Exception) { throw; }
        }

        public void SetEmployeeId(string employeeId, int stepNumber)
        {
            try
            {
                browserHelper.SetText(employeeIdInput, employeeId);
            }
            catch (Exception) { throw; }
        }

        public void UploadImage(string imagePath, int stepNumber)
        {
            try
            {
                browserHelper.Driver.FindElement(imageUploadInput).SendKeys(imagePath);
            }
            catch (Exception) { throw; }
        }

        public void ClickSaveButton(int stepNumber)
        {
            try
            {
                browserHelper.Driver.FindElement(saveButton).Click();
                reportHelper.Info("Clicked Save Button");
            }
            catch (Exception) { throw; }
        }

        public bool IsAddEmployeeTabDisplayed(int stepNumber)
        {
            try
            {
                return browserHelper.ElementIsVisible(addEmployeeTab);
            }
            catch (Exception) { return false; }
        }

        public bool IsSuccessToastDisplayed(int stepNumber)
        {
            try
            {
                return browserHelper.ElementIsVisible(successToast);
            }
            catch (Exception) { return false; }
        }
    }
}
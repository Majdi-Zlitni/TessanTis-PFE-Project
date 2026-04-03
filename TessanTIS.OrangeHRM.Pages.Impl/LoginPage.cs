using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Pages.Impl.Base;
using TessanTIS.OrangeHRM.Pages.Abstraction;

namespace TessanTIS.OrangeHRM.Pages.Impl
{
    public class LoginPage : PageBase, ILoginPage
    {
        private readonly By txtLogin = By.Id("email");
        private readonly By txtPassword = By.Id("passwd");
        private readonly By submitLogin = By.Id("SubmitLogin");
        private readonly By txtcreateEmail = By.Id("email_create");
        private readonly By submitcreate = By.Id("SubmitCreate");
        private readonly By txtCustomerFirstName = By.Id("customer_firstname");
        private readonly By txtFirstName = By.Id("firstname");
        private readonly By txtCustomerLastName = By.Id("customer_lastname");
        private readonly By txtLastName = By.Id("lastname");
        private readonly By txtNewPassword = By.Id("passwd");
        private readonly By txtCompany = By.Id("company");
        private readonly By txtCity = By.Id("city");
        private readonly By txtPostcode = By.Id("postcode");
        private readonly By txtPhone_mobile = By.Id("phone_mobile");
        private readonly By txtAlias = By.Id("alias");
        private readonly By drpState = By.XPath("//*[@id=\"id_state\"]");
        private readonly By drpCountry = By.XPath("//*[@id=\"id_country\"]");
        private readonly By submitAccount = By.Id("submitAccount");
        private readonly By txtAddress = By.Id("address1");
        private readonly By errorMessage = By.XPath("//*[@id=\"create_account_error\"]/ol/li");
        private readonly By emptyFieldsErrorMessage = By.XPath("//*[@id=\"center_column\"]/div/p");

        private readonly By usernameInputXpath = By.XPath(
            "//*[@id=\"app\"]/div[1]/div/div[1]/div/div[2]/div[2]/form/div[1]/div/div[2]/input"
        );
        private readonly By passwordInputXpath = By.XPath(
            "//*[@id=\"app\"]/div[1]/div/div[1]/div/div[2]/div[2]/form/div[2]/div/div[2]/input"
        );
        private readonly By loginbutton = By.XPath(
            "//*[@id=\"app\"]/div[1]/div/div[1]/div/div[2]/div[2]/form/div[3]/button"
        );

        public LoginPage(
            IBrowserHelper browserHelper,
            IReportHelper reportHelper,
            ILoggerHelper logg
        )
        {
            this.browserHelper = browserHelper;
            this.reportHelper = reportHelper;
            this.loggerHelper = logg;
            loggerHelper = LoggerHelper.GetInstance();
        }

        #region login
        public void setLogin(string login, int stepNumber)
        {
            try
            {
                browserHelper.SetText(usernameInputXpath, login);
                reportHelper.Info("SetLogin OK");
                loggerHelper.Info("SetLogin OK");
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void setPassword(string password, int stepNumber)
        {
            try
            {
                browserHelper.SetText(passwordInputXpath, password);
                reportHelper.Info("setPassword OK");
                loggerHelper.Info("setPassword OK");
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void ClickLogin(int stepNumber)
        {
            try
            {
                browserHelper.ClickButton(loginbutton);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void ClickSubmitLogin(int stepNumber)
        {
            try
            {
                browserHelper.ClickButton(submitLogin);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }
        #endregion


        #region signup

        public void SetCreateEmail_EmailAddress(int stepNumber, string createEmail)
        {
            try
            {
                browserHelper.SetText(txtcreateEmail, createEmail);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void ClickSubmitCreate(int stepNumber)
        {
            try
            {
                browserHelper.ClickButton(submitcreate);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        #region personal information
        public void SetTitle(int stepNumber, string genderId)
        {
            try
            {
                browserHelper.ClickRadio(By.XPath("//input[@id=\"" + genderId + "\"]"));
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetCustomerFirstName(int stepNumber, string customerFirstName)
        {
            try
            {
                browserHelper.SetText(txtCustomerFirstName, customerFirstName);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetCustomerLastName(int stepNumber, string customerLastName)
        {
            try
            {
                browserHelper.SetText(txtCustomerLastName, customerLastName);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetCustomerNewPassword(int stepNumber, string newPassword)
        {
            try
            {
                browserHelper.SetText(txtNewPassword, newPassword);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }
        #endregion



        #region comapny
        public void SetFirstName(int stepNumber, string firstName)
        {
            try
            {
                browserHelper.SetText(txtFirstName, firstName);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetLastName(int stepNumber, string lastName)
        {
            try
            {
                browserHelper.SetText(txtLastName, lastName);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetCompany(int stepNumber, string company)
        {
            try
            {
                browserHelper.SetText(txtCompany, company);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetCity(int stepNumber, string city)
        {
            try
            {
                browserHelper.SetText(txtCity, city);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetPostCode(int stepNumber, string PostCode)
        {
            try
            {
                browserHelper.SetText(txtPostcode, PostCode);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetPhoneNumber(int stepNumber, string phoneNumber)
        {
            try
            {
                browserHelper.SetText(txtPhone_mobile, phoneNumber);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetAlias(int stepNumber, string alias)
        {
            try
            {
                browserHelper.SetText(txtAlias, alias);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SelectState(int stepNumber, string state)
        {
            try
            {
                browserHelper.SelectDropDownElementByTextValue(drpState, state);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SelectCountry(int stepNumber, string country)
        {
            try
            {
                browserHelper.SelectDropDownElementByTextValue(drpCountry, country);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void ClickSubmitCreateAccount(int stepNumber)
        {
            try
            {
                browserHelper.ClickButton(submitAccount);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public void SetAddress(int stepNumber, string adress)
        {
            try
            {
                browserHelper.SetText(txtAddress, adress);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }
        #endregion
        #endregion

        public string GetErrorMessage(int stepNumber)
        {
            try
            {
                return browserHelper.GetText(errorMessage);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }

        public string GetEmptyFieldsErrorMessage(int stepNumber)
        {
            try
            {
                return browserHelper.GetText(emptyFieldsErrorMessage);
            }
            catch (Exception ex)
            {
                loggerHelper.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                reportHelper.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crasherd: Exception: {ex.Message}"
                );
                throw;
            }
        }
    }
}

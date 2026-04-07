using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.OrangeHRM.Pages.Abstraction
{
    public interface ILoginPage
    {
        void ClickLogin(int stepNumber);
        void ClickSubmitCreate(int stepNumber);
        void ClickSubmitCreateAccount(int stepNumber);
        void ClickSubmitLogin(int stepNumber);
        string GetEmptyFieldsErrorMessage(int stepNumber);
        string GetErrorMessage(int stepNumber);
        void SelectCountry(int stepNumber, string country);
        void SelectState(int stepNumber, string state);
        void SetAddress(int stepNumber, string adress);
        void SetAlias(int stepNumber, string alias);
        void SetCity(int stepNumber, string city);
        void SetCompany(int stepNumber, string company);
        void SetCreateEmail_EmailAddress(int stepNumber, string createEmail);
        void SetCustomerFirstName(int stepNumber, string customerFirstName);
        void SetCustomerLastName(int stepNumber, string customerLastName);
        void SetCustomerNewPassword(int stepNumber, string newPassword);
        void SetFirstName(int stepNumber, string firstName);
        void SetLastName(int stepNumber, string lastName);
        void setLogin(string login, int stepNumber);
        void setPassword(string password, int stepNumber);
        void SetPhoneNumber(int stepNumber, string phoneNumber);
        void SetPostCode(int stepNumber, string PostCode);
        void SetTitle(int stepNumber, string genderId);
        bool IsLoginButtonDisplayed(int stepNumber);
    }
}

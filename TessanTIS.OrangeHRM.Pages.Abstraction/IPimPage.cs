using System;

namespace TessanTIS.OrangeHRM.Pages.Abstraction
{
    public interface IPimPage
    {
        void NavigateToPim(int stepNumber);
        void ClickAddEmployeeTab(int stepNumber);
        void SetFirstName(string firstName, int stepNumber);
        void SetMiddleName(string middleName, int stepNumber);
        void SetLastName(string lastName, int stepNumber);
        void SetEmployeeId(string employeeId, int stepNumber);
        void UploadImage(string imagePath, int stepNumber);
        void ClickSaveButton(int stepNumber);
        bool IsAddEmployeeTabDisplayed(int stepNumber);
        bool IsSuccessToastDisplayed(int stepNumber);
    }
}
using System;
using System.Collections.Generic;
using TessanTIS.OrangeHRM.Data.Abstraction;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Abstraction;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Action
{
    public class OrangeHRMPimWorkflowAction : IOrangeHRMPimWorkflowAction
    {
        private readonly IPimPage _pimPage;
        private readonly IOrangeHRMPimData _pimData;
        private readonly IDictionary<int, IDictionary<string, string>> _datas = null;

        public OrangeHRMPimWorkflowAction(IPimPage pimPage, IOrangeHRMPimData pimData)
        {
            _pimPage = pimPage;
            _pimData = pimData;
            if (_pimData != null && _pimData.helper != null)
            {
                _datas = _pimData.helper.Datas;
            }
        }

        public void NavigateToPim(int stepNumber)
        {
            _pimPage.NavigateToPim(stepNumber);
        }

        public void AddEmployee(int stepNumber)
        {
            string employeeId = Guid.NewGuid().ToString("N").Substring(0, 6);

            var stepData =
                _datas != null && _datas.ContainsKey(stepNumber) ? _datas[stepNumber] : null;

            string firstName =
                stepData != null
                && stepData.ContainsKey(_pimData.FirstName)
                && stepData[_pimData.FirstName].ToString().Length != 0
                    ? stepData[_pimData.FirstName]
                    : "John";
            string middleName =
                stepData != null
                && stepData.ContainsKey(_pimData.MiddleName)
                && stepData[_pimData.MiddleName].ToString().Length != 0
                    ? stepData[_pimData.MiddleName]
                    : "Quincy";
            string lastName =
                stepData != null
                && stepData.ContainsKey(_pimData.LastName)
                && stepData[_pimData.LastName].ToString().Length != 0
                    ? stepData[_pimData.LastName]
                    : "Doe";
            string imagePath =
                stepData != null && stepData.ContainsKey(_pimData.ImagePath)
                    ? stepData[_pimData.ImagePath]
                    : "";

            _pimPage.ClickAddEmployeeTab(stepNumber);
            _pimPage.SetFirstName(firstName, stepNumber);
            _pimPage.SetMiddleName(middleName, stepNumber);
            _pimPage.SetLastName(lastName, stepNumber);
            _pimPage.SetEmployeeId(employeeId, stepNumber);

            if (!string.IsNullOrEmpty(imagePath))
            {
                _pimPage.UploadImage(imagePath, stepNumber);
            }

            _pimPage.ClickSaveButton(stepNumber);
        }
    }
}

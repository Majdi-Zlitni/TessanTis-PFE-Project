using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.OrangeHRM.Data.Impl;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Base;
using TessanTIS.Common.Core.Abstraction;
using Unity;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Verification
{
    public class OrangeHRMWorkflowVerification : WorkflowBase, IOrangeHRMWorkflowVerification
    {
        private OrangeHRMData OrangeHRMData = null;
        private IDictionary<string, string> orangeHRMDataMap = null;
        private IDictionary<int, IDictionary<string, string>> orangeHRMDataRows = null;
        private IProfilPage profilPage = null;
        private ILoginPage loginPage = null;
        private ISearchPage searchPage = null;

        public OrangeHRMWorkflowVerification(
            IBrowserHelper browser,
            IReportHelper extent,
            OrangeHRMData OrangeHRMData,
            ILoggerHelper logg
        )
        {
            this.browser = browser;
            this.extent = extent;
            this.logging = logg;
            this.OrangeHRMData = OrangeHRMData;

            orangeHRMDataMap = this.OrangeHRMData.helper.Data;
            orangeHRMDataRows = this.OrangeHRMData.helper.Datas;
            SetUpWorkflow(browser, extent, OrangeHRMData, logg);
            profilPage = unityContainer.Resolve<IProfilPage>();
            loginPage = unityContainer.Resolve<ILoginPage>();
            searchPage = unityContainer.Resolve<ISearchPage>();
        }

        public void VerifyUserAccountCreatedSuccefuly(int stepNumber)
        {
            try
            {
                string userName = profilPage.GetUserName(stepNumber);
                if (userName.Contains(orangeHRMDataRows[stepNumber][OrangeHRMData.CustomerFirstName]))
                {
                    extent.SetStepStatusPass(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"User {userName} is created succefuly"
                    );
                    logging.Info($"User {userName} is created succefuly");
                }
                else
                {
                    extent.SetStepStatusFail(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"User {userName} not created succefuly"
                    );
                    logging.Error($"User {userName} not created succefuly");
                }
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void VerifyErrorMessageForSignUpFirstStep(int stepNumber)
        {
            try
            {
                string errorMessage = loginPage.GetErrorMessage(stepNumber);
                if (errorMessage.Contains(orangeHRMDataRows[stepNumber][OrangeHRMData.ErrorMessage]))
                {
                    extent.SetStepStatusPass(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Error Message \"{errorMessage}\" displayed succefuly"
                    );
                    logging.Info($"Error Message \"{errorMessage}\" displayed succefuly");
                }
                else
                {
                    extent.SetStepStatusFail(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Error Message \"{errorMessage}\" not displayed succefuly"
                    );
                    logging.Error($"Error Message \"{errorMessage}\" not displayed succefuly");
                }
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void VerifyErrorMessageForEmptyFields(int stepNumber)
        {
            try
            {
                string errorMessage = loginPage.GetEmptyFieldsErrorMessage(stepNumber);
                if (errorMessage.Contains(orangeHRMDataRows[stepNumber][OrangeHRMData.EmptyFieldsErrorMessage]))
                {
                    extent.SetStepStatusPass(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Error Message \"{errorMessage}\" displayed succefuly"
                    );
                    logging.Info($"Error Message \"{errorMessage}\" displayed succefuly");
                }
                else
                {
                    extent.SetStepStatusFail(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Error Message \"{errorMessage}\" not displayed succefuly"
                    );
                    logging.Error($"Error Message \"{errorMessage}\" not displayed succefuly");
                }
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }

        public void VerifyProductDisplayedSuccessfuly(int stepNumber)
        {
            try
            {
                string productName = searchPage.GetSearchResultName(stepNumber);
                if (productName.Contains(orangeHRMDataMap[OrangeHRMData.ProductName]))
                {
                    extent.SetStepStatusPass(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Product \"{productName}\" displayed succefuly"
                    );
                    logging.Info($"Product \"{productName}\" displayed succefuly");
                }
                else
                {
                    extent.SetStepStatusFail(
                        TestContext.CurrentContext.Test.Name,
                        stepNumber,
                        $"Product \"{productName}\" not displayed succefuly"
                    );
                    logging.Error($"Product \"{productName}\" not displayed succefuly");
                }
            }
            catch (Exception ex)
            {
                logging.Error(
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                extent.SetStepStatusFail(
                    TestContext.CurrentContext.Test.Name,
                    stepNumber,
                    $"{MethodBase.GetCurrentMethod().Name} crashed : Exception : {ex.Message}"
                );
                throw;
            }
        }
    }
}


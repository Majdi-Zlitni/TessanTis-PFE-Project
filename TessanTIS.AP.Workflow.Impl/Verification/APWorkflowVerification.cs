using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.AP.Data.Impl;
using TessanTIS.AP.Pages.Abstraction;
using TessanTIS.AP.Workflow.Abstraction;
using TessanTIS.AP.Workflow.Impl.Base;
using TessanTIS.Common.Core.Abstraction;
using Unity;

namespace TessanTIS.AP.Workflow.Impl.Verification
{
    public class APWorkflowVerification : WorkflowBase, IAPWorkflowVerification
    {
        private APData apData = null;
        private IDictionary<string, string> ap_Data = null;
        private IDictionary<int, IDictionary<string, string>> ap_Datas = null;
        private IProfilPage profilPage = null;
        private ILoginPage loginPage = null;
        private ISearchPage searchPage = null;

        public APWorkflowVerification(
            IBrowserHelper browser,
            IReportHelper extent,
            APData apData,
            ILoggerHelper logg
        )
        {
            this.browser = browser;
            this.extent = extent;
            this.logging = logg;
            this.apData = apData;

            ap_Data = this.apData.helper.Data;
            ap_Datas = this.apData.helper.Datas;
            SetUpWorkflow(browser, extent, apData, logg);
            profilPage = unityContainer.Resolve<IProfilPage>();
            loginPage = unityContainer.Resolve<ILoginPage>();
            searchPage = unityContainer.Resolve<ISearchPage>();
        }

        public void VerifyUserAccountCreatedSuccefuly(int stepNumber)
        {
            try
            {
                string userName = profilPage.GetUserName(stepNumber);
                if (userName.Contains(ap_Datas[stepNumber][apData.CustomerFirstName]))
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
                if (errorMessage.Contains(ap_Datas[stepNumber][apData.ErrorMessage]))
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
                if (errorMessage.Contains(ap_Datas[stepNumber][apData.EmptyFieldsErrorMessage]))
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
                if (productName.Contains(ap_Data[apData.ProductName]))
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

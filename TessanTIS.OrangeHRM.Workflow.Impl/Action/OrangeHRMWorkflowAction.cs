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
using TessanTIS.Common.Core.Impl.Configuration;
using Unity;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Action
{
    public class OrangeHRMWorkflowAction : WorkflowBase, IOrangeHRMWorkflowAction
    {
        private readonly ILoginPage loginPage = null;
        private readonly IProfilPage profilPage = null;
        private readonly IWomenTShirtPage womenTShirtPage = null;
        private readonly ISearchPage searchPage = null;
        private readonly OrangeHRMData OrangeHRMData = null;
        private readonly IDictionary<string, string> orangeHRMDataMap = null;
        private readonly IDictionary<int, IDictionary<string, string>> orangeHRMDataRows = null;

        public OrangeHRMWorkflowAction(
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
            logging = LoggerHelper.GetInstance();
            SetUpWorkflow(browser, extent, OrangeHRMData, logg);

            this.loginPage = unityContainer.Resolve<ILoginPage>();
            this.profilPage = unityContainer.Resolve<IProfilPage>();
            this.womenTShirtPage = unityContainer.Resolve<IWomenTShirtPage>();
            this.searchPage = unityContainer.Resolve<ISearchPage>();
        }

        public void CreateAccountFirstStep(int stepNumber)
        {
            try
            {
                loginPage.SetCreateEmail_EmailAddress(
                    stepNumber,
                    orangeHRMDataRows[stepNumber][OrangeHRMData.CreateAccountEmail]
                );
                loginPage.ClickSubmitCreate(stepNumber);
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

        public void CreateAccountSecondStep(int stepNumber)
        {
            try
            {
                //Personal Information
                string genderId = ConfigurationHelper.Config[
                    $"GenderIds:{orangeHRMDataRows[stepNumber][OrangeHRMData.Gender]}"
                ];
                loginPage.SetTitle(stepNumber, genderId);
                loginPage.SetCustomerFirstName(
                    stepNumber,
                    orangeHRMDataRows[stepNumber][OrangeHRMData.CustomerFirstName]
                );
                loginPage.SetCustomerLastName(
                    stepNumber,
                    orangeHRMDataRows[stepNumber][OrangeHRMData.CustomerLastName]
                );
                loginPage.SetCustomerNewPassword(
                    stepNumber,
                    orangeHRMDataRows[stepNumber][OrangeHRMData.NewPassword]
                );
                logging.Info($"Personal Information Done");

                //YOUR ADDRESS

                loginPage.SetFirstName(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.CustomerFirstName]);
                loginPage.SetLastName(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.CustomerLastName]);
                loginPage.SetCompany(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.Company]);
                loginPage.SetCity(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.City]);
                loginPage.SetPostCode(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.PostCode]);
                loginPage.SetPhoneNumber(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.PhoneNumber]);
                loginPage.SetAlias(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.Alias]);
                loginPage.SelectState(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.State]);
                loginPage.SelectCountry(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.Country]);
                loginPage.SetAddress(stepNumber, orangeHRMDataRows[stepNumber][OrangeHRMData.Adress]);
                loginPage.ClickSubmitCreateAccount(stepNumber);
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

        public void CreateAccountSecondStepWithEmptyFields(int stepNumber)
        {
            try
            {
                loginPage.ClickSubmitCreateAccount(stepNumber);
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

        public void SaveFirstProductName(int stepNumber)
        {
            try
            {
                var firstProductName = womenTShirtPage.GetFirstProductName(stepNumber);
                orangeHRMDataMap[OrangeHRMData.ProductName] = firstProductName;
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

        public void SearchForProduct(int stepNumber)
        {
            try
            {
                womenTShirtPage.Search(stepNumber, orangeHRMDataMap[OrangeHRMData.ProductName]);
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


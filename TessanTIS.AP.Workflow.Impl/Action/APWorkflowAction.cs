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
using TessanTIS.Common.Core.Impl.Configuration;
using Unity;

namespace TessanTIS.AP.Workflow.Impl.Action
{
    public class APWorkflowAction : WorkflowBase, IAPWorkflowAction
    {
        private readonly ILoginPage loginPage = null;
        private readonly IProfilPage profilPage = null;
        private readonly IWomenTShirtPage womenTShirtPage = null;
        private readonly ISearchPage searchPage = null;
        private readonly APData apData = null;
        private readonly IDictionary<string, string> ap_Data = null;
        private readonly IDictionary<int, IDictionary<string, string>> ap_Datas = null;

        public APWorkflowAction(
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
            logging = LoggerHelper.GetInstance();
            SetUpWorkflow(browser, extent, apData, logg);

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
                    ap_Datas[stepNumber][apData.CreateAccountEmail]
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
                    $"GenderIds:{ap_Datas[stepNumber][apData.Gender]}"
                ];
                loginPage.SetTitle(stepNumber, genderId);
                loginPage.SetCustomerFirstName(
                    stepNumber,
                    ap_Datas[stepNumber][apData.CustomerFirstName]
                );
                loginPage.SetCustomerLastName(
                    stepNumber,
                    ap_Datas[stepNumber][apData.CustomerLastName]
                );
                loginPage.SetCustomerNewPassword(
                    stepNumber,
                    ap_Datas[stepNumber][apData.NewPassword]
                );
                logging.Info($"Personal Information Done");

                //YOUR ADDRESS

                loginPage.SetFirstName(stepNumber, ap_Datas[stepNumber][apData.CustomerFirstName]);
                loginPage.SetLastName(stepNumber, ap_Datas[stepNumber][apData.CustomerLastName]);
                loginPage.SetCompany(stepNumber, ap_Datas[stepNumber][apData.Company]);
                loginPage.SetCity(stepNumber, ap_Datas[stepNumber][apData.City]);
                loginPage.SetPostCode(stepNumber, ap_Datas[stepNumber][apData.PostCode]);
                loginPage.SetPhoneNumber(stepNumber, ap_Datas[stepNumber][apData.PhoneNumber]);
                loginPage.SetAlias(stepNumber, ap_Datas[stepNumber][apData.Alias]);
                loginPage.SelectState(stepNumber, ap_Datas[stepNumber][apData.State]);
                loginPage.SelectCountry(stepNumber, ap_Datas[stepNumber][apData.Country]);
                loginPage.SetAddress(stepNumber, ap_Datas[stepNumber][apData.Adress]);
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
                ap_Data[apData.ProductName] = firstProductName;
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
                womenTShirtPage.Search(stepNumber, ap_Data[apData.ProductName]);
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

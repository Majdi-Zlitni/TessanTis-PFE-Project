using System;
using NUnit.Framework;
using TessanTIS.Common.Core.Impl.Data;
using TessanTIS.Common.Core.Test.Impl.Base;
using TessanTIS.OrangeHRM.Data.Abstraction;
using TessanTIS.OrangeHRM.Data.Impl;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Action;
using TessanTIS.OrangeHRM.Workflow.Impl.Verification;
using Unity;
using Unity.Injection;

namespace TessanTIS.OrangeHRM.Test.Impl.Base
{
    public class OrangeHRMTestBase : TestBase
    {
        private TestCaseWorkflowExecution testCaseWorkflowExecution = null;
        private OrangeHRMData orangeHRMData = null;
        private OrangeHRMConfigurationData orangeHRMConfigurationData = null;
        private IOrangeHRMLoginData loginData = null;

        private IOrangeHRMLoginWorkflowAction loginWorkflowAction = null;
        private IOrangeHRMAccessWorkflowAction accessWorkflowAction = null;
        private IOrangeHRMWorkflowAction orangeHRMWorkflowAction = null;
        private IOrangeHRMWorkflowVerification orangeHRMWorkflowVerification = null;

        [Dependency]
        public string Type { get; set; }

        public void PrepareSetup()
        {
            unityContainer.RegisterType<TestCaseWorkflowExecution>(
                new InjectionConstructor(
                    TestContext.CurrentContext.Test.Name,
                    "OrangeHRM",
                    Type,
                    ApplicationVersion,
                    extent,
                    logging
                )
            );
            testCaseWorkflowExecution = unityContainer.Resolve<TestCaseWorkflowExecution>();

            unityContainer.RegisterType<OrangeHRMData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "OrangeHRM", Type)
            );
            orangeHRMData = unityContainer.Resolve<OrangeHRMData>();

            unityContainer.RegisterType<OrangeHRMConfigurationData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "OrangeHRM", Type)
            );
            orangeHRMConfigurationData = unityContainer.Resolve<OrangeHRMConfigurationData>();

            unityContainer.RegisterType<IOrangeHRMLoginData, OrangeHRMLoginData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "OrangeHRM", Type)
            );
            loginData = unityContainer.Resolve<IOrangeHRMLoginData>();

            loginData.helper.Data[loginData.Url] = BaseUrl;
            orangeHRMData.helper.Data[loginData.Url] = BaseUrl;
            orangeHRMConfigurationData.helper.Data[loginData.Url] = BaseUrl;

            ExecutionWorkflowList = testCaseWorkflowExecution.ExecuteWorkflowList;

            unityContainer.RegisterType<
                IOrangeHRMLoginWorkflowAction,
                OrangeHRMLoginWorkflowAction
            >(new InjectionConstructor(browser, extent, loginData, logging));
            loginWorkflowAction = unityContainer.Resolve<IOrangeHRMLoginWorkflowAction>();

            unityContainer.RegisterType<
                IOrangeHRMAccessWorkflowAction,
                OrangeHRMAccessWorkflowAction
            >(new InjectionConstructor(browser, extent, BaseUrl));
            accessWorkflowAction = unityContainer.Resolve<IOrangeHRMAccessWorkflowAction>();

            unityContainer.RegisterType<IOrangeHRMWorkflowAction, OrangeHRMWorkflowAction>(
                new InjectionConstructor(browser, extent, orangeHRMData, logging)
            );
            orangeHRMWorkflowAction = unityContainer.Resolve<IOrangeHRMWorkflowAction>();

            unityContainer.RegisterType<
                IOrangeHRMWorkflowVerification,
                OrangeHRMWorkflowVerification
            >(new InjectionConstructor(browser, extent, orangeHRMData, logging));
            orangeHRMWorkflowVerification =
                unityContainer.Resolve<IOrangeHRMWorkflowVerification>();
        }

        public override void ExecuteAction(int stepNumber, string actionKeyWord)
        {
            switch (actionKeyWord)
            {
                case "OpenOrangeHRMWebSite":
                    accessWorkflowAction.OpenOrangeHRMWebSite(stepNumber);
                    break;
                case "LoginWithCorrectCredential":
                    loginWorkflowAction.LoginWithCorrectCredential(stepNumber);
                    break;
                case "VerifyUserLoggedInSuccessfully":
                    orangeHRMWorkflowVerification.VerifyUserLoggedInSuccessfully(stepNumber);
                    break;

                case "OpenAutomationPracticeWebSite":
                    accessWorkflowAction.OpenAutomationPracticeWebSite(stepNumber);
                    break;

                case "AccessToLoginPage":
                    accessWorkflowAction.AccessToLoginPage(stepNumber);
                    break;
                case "CreateAccountFirstStep":
                    orangeHRMWorkflowAction.CreateAccountFirstStep(stepNumber);
                    break;
                case "CreateAccountSecondStep":
                    orangeHRMWorkflowAction.CreateAccountSecondStep(stepNumber);
                    break;
                case "CreateAccountSecondStepWithEmptyFields":
                    orangeHRMWorkflowAction.CreateAccountSecondStepWithEmptyFields(stepNumber);
                    break;
                case "VerifyUserAccountCreatedSuccefuly":
                    orangeHRMWorkflowVerification.VerifyUserAccountCreatedSuccefuly(stepNumber);
                    break;
                case "VerifyErrorMessageForSignUpFirstStep":
                    orangeHRMWorkflowVerification.VerifyErrorMessageForSignUpFirstStep(stepNumber);
                    break;
                case "VerifyErrorMessageForEmptyFields":
                    orangeHRMWorkflowVerification.VerifyErrorMessageForEmptyFields(stepNumber);
                    break;
                case "AccessToWomenTShirt":
                    accessWorkflowAction.AccessToWomenTShirt(stepNumber);
                    break;
                case "SaveFirstProductName":
                    orangeHRMWorkflowAction.SaveFirstProductName(stepNumber);
                    break;
                case "SearchForProduct":
                    orangeHRMWorkflowAction.SearchForProduct(stepNumber);
                    break;
                case "VerifyProductDisplayedSuccessfuly":
                    orangeHRMWorkflowVerification.VerifyProductDisplayedSuccessfuly(stepNumber);
                    break;
                default:
                    throw new Exception("given action not found!");
            }
        }
    }
}

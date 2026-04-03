using System;
using NUnit.Framework;
using TessanTIS.AP.Data.Abstraction;
using TessanTIS.AP.Data.Impl;
using TessanTIS.AP.Workflow.Abstraction;
using TessanTIS.AP.Workflow.Impl.Action;
using TessanTIS.AP.Workflow.Impl.Verification;
using TessanTIS.Common.Core.Impl.Data;
using TessanTIS.Common.Core.Test.Impl.Base;
using Unity;
using Unity.Injection;

namespace TessanTIS.AP.Test.Impl.Base
{
    public class APTestBase : TestBase
    {
        private TestCaseWorkflowExecution testCaseWorkflowExecution = null;
        private APData apData = null;
        private APConfigurationData apConfigurationData = null;
        private ILoginData loginData = null;

        private ILoginWorkflowAction loginWorkflowAction = null;

        //private ILoginWorkflowVerification loginWorkflowVerification = null;
        private IAccessWorkflowAction accessWorkflowAction = null;

        //private IAccessWorkflowVerification accessWorkflowVerification = null;
        private IAPWorkflowAction aPWorkflowAction = null;
        private IAPWorkflowVerification aPWorkflowVerification = null;

        [Dependency]
        public string Type { get; set; }

        public void PrepareSetup()
        {
            unityContainer.RegisterType<TestCaseWorkflowExecution>(
                new InjectionConstructor(
                    TestContext.CurrentContext.Test.Name,
                    "AP",
                    Type,
                    ApplicationVersion,
                    extent,
                    logging
                )
            );
            testCaseWorkflowExecution = unityContainer.Resolve<TestCaseWorkflowExecution>();

            unityContainer.RegisterType<APData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "AP", Type)
            );
            apData = unityContainer.Resolve<APData>();

            unityContainer.RegisterType<APConfigurationData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "AP", Type)
            );
            apConfigurationData = unityContainer.Resolve<APConfigurationData>();

            unityContainer.RegisterType<ILoginData, LoginData>(
                new InjectionConstructor(TestContext.CurrentContext.Test.Name, "AP", Type)
            );
            loginData = unityContainer.Resolve<ILoginData>();

            loginData.helper.Data[loginData.Url] = BaseUrl;
            apData.helper.Data[loginData.Url] = BaseUrl;
            apConfigurationData.helper.Data[loginData.Url] = BaseUrl;

            ExecutionWorkflowList = testCaseWorkflowExecution.ExecuteWorkflowList;

            unityContainer.RegisterType<ILoginWorkflowAction, LoginWorkflowAction>(
                new InjectionConstructor(browser, extent, loginData, logging)
            );
            loginWorkflowAction = unityContainer.Resolve<ILoginWorkflowAction>();

            /* unityContainer.RegisterType<ILoginWorkflowVerification, LoginWorkflowVerification>(new InjectionConstructor(browser, extent));
             loginWorkflowVerification = unityContainer.Resolve<ILoginWorkflowVerification>();*/

            unityContainer.RegisterType<IAccessWorkflowAction, AccessWorkflowAction>(
                new InjectionConstructor(browser, extent, BaseUrl)
            );
            accessWorkflowAction = unityContainer.Resolve<IAccessWorkflowAction>();

            /*   unityContainer.RegisterType<IAccessWorkflowVerification, AccessWorkflowVerification>(new InjectionConstructor(browser, extent,null));
               accessWorkflowVerification = unityContainer.Resolve<IAccessWorkflowVerification>();*/

            unityContainer.RegisterType<IAPWorkflowAction, APWorkflowAction>(
                new InjectionConstructor(browser, extent, apData, logging)
            );
            aPWorkflowAction = unityContainer.Resolve<IAPWorkflowAction>();

            unityContainer.RegisterType<IAPWorkflowVerification, APWorkflowVerification>(
                new InjectionConstructor(browser, extent, apData, logging)
            );
            aPWorkflowVerification = unityContainer.Resolve<IAPWorkflowVerification>();
        }

        public override void ExecuteAction(int stepNumber, string actionKeyWord)
        {
            switch (actionKeyWord)
            {
                case "OpenAutomationPracticeWebSite":
                    accessWorkflowAction.OpenAutomationPracticeWebSite(stepNumber);
                    break;
                case "LoginWithCorrectCredential":
                    loginWorkflowAction.LoginWithCorrectCredential(stepNumber);
                    break;
                case "AccessToLoginPage":
                    accessWorkflowAction.AccessToLoginPage(stepNumber);
                    break;
                case "CreateAccountFirstStep":
                    aPWorkflowAction.CreateAccountFirstStep(stepNumber);
                    break;
                case "CreateAccountSecondStep":
                    aPWorkflowAction.CreateAccountSecondStep(stepNumber);
                    break;
                case "CreateAccountSecondStepWithEmptyFields":
                    aPWorkflowAction.CreateAccountSecondStepWithEmptyFields(stepNumber);
                    break;
                case "VerifyUserAccountCreatedSuccefuly":
                    aPWorkflowVerification.VerifyUserAccountCreatedSuccefuly(stepNumber);
                    break;
                case "VerifyErrorMessageForSignUpFirstStep":
                    aPWorkflowVerification.VerifyErrorMessageForSignUpFirstStep(stepNumber);
                    break;
                case "VerifyErrorMessageForEmptyFields":
                    aPWorkflowVerification.VerifyErrorMessageForEmptyFields(stepNumber);
                    break;
                case "AccessToWomenTShirt":
                    accessWorkflowAction.AccessToWomenTShirt(stepNumber);
                    break;
                case "SaveFirstProductName":
                    aPWorkflowAction.SaveFirstProductName(stepNumber);
                    break;
                case "SearchForProduct":
                    aPWorkflowAction.SearchForProduct(stepNumber);
                    break;
                case "VerifyProductDisplayedSuccessfuly":
                    aPWorkflowVerification.VerifyProductDisplayedSuccessfuly(stepNumber);
                    break;
                default:
                    throw new Exception("given action not found!");
            }
        }
    }
}

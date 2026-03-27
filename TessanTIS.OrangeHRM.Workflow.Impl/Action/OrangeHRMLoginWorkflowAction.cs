using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TessanTIS.OrangeHRM.Data.Abstraction;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Pages.Impl;
using TessanTIS.OrangeHRM.Workflow.Abstraction;
using TessanTIS.OrangeHRM.Workflow.Impl.Base;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Action
{
    public class OrangeHRMLoginWorkflowAction : WorkflowBase, IOrangeHRMLoginWorkflowAction
    {
        private readonly ILoginPage loginPage = null;
        private readonly IOrangeHRMLoginData loginData = null;
        private readonly IDictionary<string, string> data = null;
        private readonly IDictionary<int, IDictionary<string, string>> Datas = null;

        public OrangeHRMLoginWorkflowAction(
            IBrowserHelper browserHelper,
            IReportHelper extent,
            IOrangeHRMLoginData loginData,
            ILoggerHelper logg
        )
        {
            this.browser = browserHelper;
            this.extent = extent;
            this.loginPage = new LoginPage(browser, extent, logg);
            if (loginData != null)
            {
                this.loginData = loginData;
                data = this.loginData.helper.Data;
                Datas = this.loginData.helper.Datas;
            }
            this.logging = logg;
            logging = LoggerHelper.GetInstance();
        }

        public void LoginWithCorrectCredential(int stepNumber)
        {
            try
            {
                loginPage.setLogin(data[loginData.ValidUserName], stepNumber);
                loginPage.setPassword(data[loginData.ValidPassword], stepNumber);
                loginPage.ClickSubmitLogin(stepNumber);
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


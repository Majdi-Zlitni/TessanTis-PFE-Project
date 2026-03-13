using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.AP.Data.Impl;
using TessanTIS.AP.Pages.Abstraction;
using TessanTIS.AP.Pages.Impl;
using TessanTIS.Common.Core.Abstraction;
using Unity;
using Unity.Injection;

namespace TessanTIS.AP.Workflow.Impl.Base
{
    public class WorkflowBase
    {
        protected IBrowserHelper browser;
        protected IReportHelper extent;
        protected ILoggerHelper logging;
        protected UnityContainer unityContainer = new UnityContainer();

        public void FailTest(string message)
        {
            try
            {
                Assert.Fail(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetUpWorkflow(IBrowserHelper browserHelper,IReportHelper reportHelper,APData apData,ILoggerHelper logger)
        {
            unityContainer.RegisterType<ILoginPage, LoginPage>(new InjectionConstructor(browserHelper, reportHelper, logger));
            unityContainer.RegisterType<IProfilPage, ProfilPage>(new InjectionConstructor(browserHelper, reportHelper, logger));
            unityContainer.RegisterType<IWomenTShirtPage,WomenTShirtPage>(new InjectionConstructor(browserHelper, reportHelper, logger));
            unityContainer.RegisterType<ISearchPage,SearchPage>(new InjectionConstructor(browserHelper, reportHelper, logger));
        }
    }
}

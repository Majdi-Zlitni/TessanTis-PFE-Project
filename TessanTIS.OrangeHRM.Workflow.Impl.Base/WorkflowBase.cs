using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.OrangeHRM.Data.Impl;
using TessanTIS.OrangeHRM.Pages.Abstraction;
using TessanTIS.OrangeHRM.Pages.Impl;
using Unity;
using Unity.Injection;

namespace TessanTIS.OrangeHRM.Workflow.Impl.Base
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

        public void SetUpWorkflow(
            IBrowserHelper browserHelper,
            IReportHelper reportHelper,
            OrangeHRMData orangeHRMData,
            ILoggerHelper logger
        )
        {
            unityContainer.RegisterType<ILoginPage, LoginPage>(
                new InjectionConstructor(browserHelper, reportHelper, logger)
            );
            unityContainer.RegisterType<IProfilPage, ProfilPage>(
                new InjectionConstructor(browserHelper, reportHelper, logger)
            );
            unityContainer.RegisterType<IWomenTShirtPage, WomenTShirtPage>(
                new InjectionConstructor(browserHelper, reportHelper, logger)
            );
            unityContainer.RegisterType<ISearchPage, SearchPage>(
                new InjectionConstructor(browserHelper, reportHelper, logger)
            );
            unityContainer.RegisterType<IPimPage, PimPage>(
                new InjectionConstructor(browserHelper, reportHelper, logger)
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;
using Unity;

namespace TessanTIS.Common.Core.Pages.Impl.Base
{
    public abstract class PageBase
    {
        protected IBrowserHelper browserHelper;
        protected IReportHelper reportHelper;
        protected ILoggerHelper loggerHelper;
        protected UnityContainer unityContainer = new UnityContainer();
    }
}

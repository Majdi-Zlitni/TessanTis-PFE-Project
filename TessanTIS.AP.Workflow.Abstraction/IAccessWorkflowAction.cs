using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Workflow.Abstraction
{
    public interface IAccessWorkflowAction
    {
        void AccessToLoginPage(int stepNumber);
        void AccessToWomenTShirt(int stepNumber);
        void OpenAutomationPracticeWebSite(int stepNumber);
    }
}

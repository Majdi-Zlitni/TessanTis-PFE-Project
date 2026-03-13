using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Workflow.Abstraction
{
    public interface ILoginWorkflowAction
    {
        void LoginWithCorrectCredential(int stepNumber);
    }
}

using System;

namespace TessanTIS.OrangeHRM.Workflow.Abstraction
{
    public interface IOrangeHRMPimWorkflowAction
    {
        void NavigateToPim(int stepNumber);
        void AddEmployee(int stepNumber);
    }
}
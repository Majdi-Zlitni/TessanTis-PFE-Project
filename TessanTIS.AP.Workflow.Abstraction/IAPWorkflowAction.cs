using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Workflow.Abstraction
{
    public interface IAPWorkflowAction
    {
        void CreateAccountFirstStep(int stepNumber);
        void CreateAccountSecondStep(int stepNumber);
        void CreateAccountSecondStepWithEmptyFields(int stepNumber);
        void SaveFirstProductName(int stepNumber);
        void SearchForProduct(int stepNumber);
    }
}

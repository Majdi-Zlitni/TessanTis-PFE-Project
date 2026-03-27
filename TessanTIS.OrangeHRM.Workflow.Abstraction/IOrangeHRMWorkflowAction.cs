namespace TessanTIS.OrangeHRM.Workflow.Abstraction
{
    public interface IOrangeHRMWorkflowAction
    {
        void CreateAccountFirstStep(int stepNumber);
        void CreateAccountSecondStep(int stepNumber);
        void CreateAccountSecondStepWithEmptyFields(int stepNumber);
        void SaveFirstProductName(int stepNumber);
        void SearchForProduct(int stepNumber);
    }
}

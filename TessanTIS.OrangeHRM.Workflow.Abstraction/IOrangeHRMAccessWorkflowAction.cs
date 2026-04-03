namespace TessanTIS.OrangeHRM.Workflow.Abstraction
{
    public interface IOrangeHRMAccessWorkflowAction
    {
        void AccessToLoginPage(int stepNumber);
        void AccessToWomenTShirt(int stepNumber);
        void OpenAutomationPracticeWebSite(int stepNumber);
        void OpenOrangeHRMWebSite(int stepNumber);
    }
}

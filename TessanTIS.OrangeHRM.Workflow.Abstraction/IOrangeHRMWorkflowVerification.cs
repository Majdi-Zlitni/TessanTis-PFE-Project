namespace TessanTIS.OrangeHRM.Workflow.Abstraction
{
    public interface IOrangeHRMWorkflowVerification
    {
        void VerifyOpenOrangeHRMWebSiteSuccessfully(int stepNumber);
        void VerifyUserLoggedInSuccessfully(int stepNumber);
        void VerifyNavigateToPimSuccessfully(int stepNumber);
        void VerifyEmployeeAddedSuccessfully(int stepNumber);
    }
}

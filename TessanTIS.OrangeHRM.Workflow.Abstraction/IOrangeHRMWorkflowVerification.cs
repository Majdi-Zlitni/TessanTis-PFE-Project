namespace TessanTIS.OrangeHRM.Workflow.Abstraction
{
    public interface IOrangeHRMWorkflowVerification
    {
        void VerifyErrorMessageForEmptyFields(int stepNumber);
        void VerifyErrorMessageForSignUpFirstStep(int stepNumber);
        void VerifyProductDisplayedSuccessfuly(int stepNumber);
        void VerifyUserAccountCreatedSuccefuly(int stepNumber);
        void VerifyUserLoggedInSuccessfully(int stepNumber);
    }
}

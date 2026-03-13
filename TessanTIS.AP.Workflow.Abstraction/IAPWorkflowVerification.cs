using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Workflow.Abstraction
{
    public interface IAPWorkflowVerification
    {
        void VerifyErrorMessageForEmptyFields(int stepNumber);
        void VerifyErrorMessageForSignUpFirstStep(int stepNumber);
        void VerifyProductDisplayedSuccessfuly(int stepNumber);
        void VerifyUserAccountCreatedSuccefuly(int stepNumber);
    }
}

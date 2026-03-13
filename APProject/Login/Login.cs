using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TessanTIS.AP.Test.Impl.Base;

namespace APProject.Login
{
    public class Login : APTestBase
    {
        [SetUp, Order(2)]
        public void Setup()
        {
            Type = "Login";
            PrepareSetup();
        }

        [Test, Order(1)]
        public void TC001_Automate_User_Registration_process_of_e_commerce_website()
        {
            ExecuteWorkflow();
        }

        [Test, Order(2)]
        public void TC002_Verify_invalid_email_address_error()
        {
            ExecuteWorkflow();
        }

        [Test, Order(3)]
        public void TC003_Verify_error_messages_for_mandatory_fields()
        {
            ExecuteWorkflow();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.OrangeHRM.Test.Impl.Base;

namespace OrangeHRM.Login
{
    internal class Login : OrangeHRMTestBase
    {
        [SetUp, Order(1)]
        public void Setup()
        {
            Type = "Login";
            PrepareSetup();
        }

        [Test, Order(2)]
        public void TC001_Automate_User_Registration_process_of_e_commerce_website()
        {
            ExecuteWorkflow();
        }
    }
}

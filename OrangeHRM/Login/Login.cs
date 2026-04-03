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
        public void TC001_Automate_User_Login()
        {
            ExecuteWorkflow();
        }

        [Test, Order(2)]
        public void TC001_Automate_login()
        {
            ExecuteWorkflow();
        }
    }
}

using NUnit.Framework;
using TessanTIS.OrangeHRM.Test.Impl.Base;

namespace OrangeHRM.Pim
{
    internal class AddEmployee : OrangeHRMTestBase
    {
        [SetUp, Order(1)]
        public void Setup()
        {
            Type = "AddEmployee";
            PrepareSetup();
        }

        [Test, Order(2)]
        public void TC002_Automate_Add_Employee()
        {
            ExecuteWorkflow();
        }
    }
}
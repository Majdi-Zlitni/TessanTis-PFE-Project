using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.AP.Test.Impl.Base;

namespace APProject.Login
{
    class Search : APTestBase
    {
        [SetUp, Order(2)]
        public void Setup()
        {
            Type = "Search";
            PrepareSetup();
        }
        [Test,Order(1)]
        public void TC001_Automate_Search_Product_feature_of_e_commerce_website_with_Selenium()
        {
            ExecuteWorkflow();
        }
    }
}

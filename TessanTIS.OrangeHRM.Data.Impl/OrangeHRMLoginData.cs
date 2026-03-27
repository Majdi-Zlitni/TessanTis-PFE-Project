using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data;

namespace TessanTIS.OrangeHRM.Data.Impl
{
    public class OrangeHRMLoginData : TessanTIS.OrangeHRM.Data.Abstraction.IOrangeHRMLoginData
    {
        public OrangeHRMLoginData(string testCaseName, string module, string subModule)
        {
            helper = new ExcelHelper();
            string spreadsheet = $"{module}_{subModule}_{testCaseName.Split('_')[0]}";
            helper.LoadData(
                $"{ConfigurationHelper.Config["DataConfig:ExcelFolderPath"]}\\{module}_{subModule}.xlsx",
                spreadsheet
            );
        }
        
        public IDataHelper helper { get; set; }
        public string InValidPassword { get; } = "InValidPassword";
        public string InValidUserName { get; } = "InValidUserName";
        public string Url { get; } = "Url";
        public string ValidPassword { get; } = "Password";
        public string ValidUserName { get; } = "User";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data;

namespace TessanTIS.OrangeHRM.Data.Impl
{
    public class OrangeHRMPimData : TessanTIS.OrangeHRM.Data.Abstraction.IOrangeHRMPimData
    {
        public OrangeHRMPimData(string testCaseName, string module, string subModule)
        {
            helper = new ExcelHelper();
            string spreadsheet = $"{module}_{subModule}_{testCaseName.Split('_')[0]}";
            helper.LoadData(
                $"{ConfigurationHelper.Config["DataConfig:ExcelFolderPath"]}\\{module}_{subModule}.xlsx",
                spreadsheet
            );
        }
        
        public IDataHelper helper { get; set; }
        public string FirstName { get; } = "FirstName";
        public string MiddleName { get; } = "MiddleName";
        public string LastName { get; } = "LastName";
        public string ImagePath { get; } = "ImagePath";
    }
}
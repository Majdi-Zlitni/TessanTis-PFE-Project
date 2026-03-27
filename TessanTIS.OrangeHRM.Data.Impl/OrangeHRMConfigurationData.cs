using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data;
using TessanTIS.OrangeHRM.Data.Abstraction;

namespace TessanTIS.OrangeHRM.Data.Impl
{
    public class OrangeHRMConfigurationData : IOrangeHRMConfigurationData
    {
        public ExcelHelper helper = null;
        
        public OrangeHRMConfigurationData(string testCaseName, string prefix, string orangeHRMType)
        {
            helper = new ExcelHelper();
            string spreadsheet = $"{prefix}_{orangeHRMType}_{testCaseName.Split('_')[0]}";
            helper.LoadData(
                $"{ConfigurationHelper.Config["DataConfig:ExcelFolderPath"]}\\{prefix}_{orangeHRMType}.xlsx",
                spreadsheet
            );
        }
    }
}

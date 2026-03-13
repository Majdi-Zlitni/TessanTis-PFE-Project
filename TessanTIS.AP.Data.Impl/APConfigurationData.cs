using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.AP.Data.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data;

namespace TessanTIS.AP.Data.Impl
{
    public class APConfigurationData:IAPConfigurationData
    {
        public ExcelHelper helper = null;
        public APConfigurationData(string testCaseName,string prefix,string APType)
        {
            helper = new ExcelHelper();
            string spreadsheet = $"{prefix}_{APType}_{testCaseName.Split('_')[0]}";
            helper.LoadData($"{ConfigurationHelper.Config["DataConfig:ExcelFolderPath"]}\\{prefix}_{APType}.xlsx", spreadsheet);
        }
    }
}

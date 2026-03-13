using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TessanTIS.Common.Core.Abstraction
{
    public interface IDataHelper
    {
        public IDictionary<string,string> Data { get; set; }
        public IDictionary<int, IDictionary<string, string>> Datas { get; set; }
        public void LoadData(string filePath, string spreadsheet);
        public void GenerateExcel(DataSet dataTable, string path);
    }
}

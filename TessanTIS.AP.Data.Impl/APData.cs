using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using TessanTIS.AP.Data.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data;

namespace TessanTIS.AP.Data.Impl
{
    public class APData : IAPData
    {
        public ExcelHelper helper = null;

        public APData(string testCaseName, string prefix, string APType)
        {
            helper = new ExcelHelper();
            string spreadsheet = $"{prefix}_{APType}_{testCaseName.Split('_')[0]}";
            helper.LoadData(
                $"{ConfigurationHelper.Config["DataConfig:ExcelFolderPath"]}\\{prefix}_{APType}.xlsx",
                spreadsheet
            );
        }

        public static List<T> ReadFromJSONt<T>(string fileName)
        {
            string folderPath = ConfigurationHelper.Config["DataConfig:JsonFolderPath"];
            string filePath = folderPath + fileName;
            List<T> list = new List<T>();
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            if (!File.Exists(filePath))
                throw new FileNotFoundException(
                    "Cannot locate the file:\n \""
                        + filePath
                        + "\"\nPlease check the path and try again."
                );
            try
            {
                list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath), settings);
            }
            catch (Exception ex)
            {
                throw new FileLoadException(
                    "Unable to load. \nThe file \"" + filePath + "\" does not load correctly.",
                    ex
                );
            }
            return list;
        }

        public string User { get; set; } = "User";
        public string Password { get; set; } = "Password";

        public string CreateAccountEmail { get; set; } = "CreateAccountEmail";
        public string Gender { get; set; } = "Gender";
        public string CustomerFirstName { get; set; } = "CustomerFirstName";
        public string CustomerLastName { get; set; } = "CustomerLastName";
        public string NewPassword { get; set; } = "NewPassword";
        public string Company { get; set; } = "Company";
        public string City { get; set; } = "City";
        public string PostCode { get; set; } = "PostCode";
        public string PhoneNumber { get; set; } = "PhoneNumber";
        public string Alias { get; set; } = "Alias";
        public string State { get; set; } = "State";
        public string Country { get; set; } = "Country";
        public string Adress { get; set; } = "Adress";
        public string ErrorMessage { get; set; } = "ErrorMessage";
        public string EmptyFieldsErrorMessage { get; set; } = "EmptyFieldsErrorMessage";
        public string ProductName { get; set; } = "ProductName";
    }
}

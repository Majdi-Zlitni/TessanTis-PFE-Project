using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Impl.Configuration
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot Config { get; } = InitConfig();
        public static IConfigurationRoot Column { get; } = ColumnTable();

        private static IConfigurationRoot ColumnTable()
        {
            var builder = new ConfigurationBuilder()
                     .AddJsonFile($"TableColumnName.json", true, true)
                     .AddEnvironmentVariables();
            return builder.Build();
        }

        private static IConfigurationRoot InitConfig()
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile($"appsettings.json", true, true)
           .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}

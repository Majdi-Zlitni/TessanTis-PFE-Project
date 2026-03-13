using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;
using TessanTIS.Common.Core.Impl.Data.Modles;

namespace TessanTIS.Common.Core.Impl.Data
{
    public class TestCaseWorkflowExecution
    {
        public ExcelHelper helper = null;
        public IList<TestCaseWorkflow> ExecuteWorkflowList { get; set; }

        private readonly IReportHelper report;
        private readonly ILoggerHelper logger;

        public TestCaseWorkflowExecution(
            string testCaseName,
            string prefix,
            string type,
            string applicationVersion,
            IReportHelper reportHelper,
            ILoggerHelper loggerHelper
        )
        {
            logger = loggerHelper;
            report = reportHelper;
            string spreadsheet = $"{prefix}_{type}_{testCaseName.Split('_')[0]}";
            string filePath =
                $"{ConfigurationHelper.Config["DataConfig:ExcelWorkflowFolderPath"]}\\{prefix}_{type}Workflow.xlsx";
            LoadExecuteWorkflow(filePath, spreadsheet, applicationVersion);
        }

        private void LoadExecuteWorkflow(
            string filePath,
            string spreadsheet,
            string applicationVersion
        )
        {
            try
            {
                ExecuteWorkflowList = new List<TestCaseWorkflow>();
                helper = new ExcelHelper();
                helper.LoadData(filePath, spreadsheet);
                string usedVersion = applicationVersion;
                bool versionIsConfigured = helper.Datas.Any(x =>
                    x.Value.Any(y => y.Key == applicationVersion)
                );
                string lastConfiguredVersion = string.Empty;
                if (helper.Datas.Any())
                {
                    lastConfiguredVersion = helper
                        .Datas.First()
                        .Value.Keys.Where(x => x.StartsWith("V"))
                        .OrderBy(y => y)
                        .Last();
                    if (!versionIsConfigured)
                    {
                        usedVersion = lastConfiguredVersion;
                        report.Info(
                            $"The version {applicationVersion} is not configured in the workflow execution!"
                        );
                        report.Info(
                            $"The last workflow execution confiugured for the version {lastConfiguredVersion} will be used. The test could be failed due a chanfe in the actual version"
                        );
                    }
                }
                foreach (
                    KeyValuePair<int, IDictionary<string, string>> keyValuePair in helper.Datas
                )
                {
                    TestCaseWorkflow testCaseWorkflow = new TestCaseWorkflow()
                    {
                        Action = keyValuePair.Value.First(x => x.Key == "Action").Value,
                        StepNumber = Int32.Parse(
                            keyValuePair.Value.First(x => x.Key == "StepNumber").Value
                        ),
                        Order = Int32.Parse(keyValuePair.Value.First(x => x.Key == "Order").Value),
                        Enabled = bool.Parse(
                            keyValuePair.Value.First(x => x.Key == usedVersion).Value
                        ),
                        Execute = bool.Parse(
                            keyValuePair.Value.First(x => x.Key == "Execute").Value
                        )
                    };
                    if (testCaseWorkflow.Enabled)
                    {
                        ExecuteWorkflowList.Add(testCaseWorkflow);
                    }
                }
            }
            catch (Exception ex)
            {
                report.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw;
            }
        }
    }
}

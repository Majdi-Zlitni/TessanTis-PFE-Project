using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using TessanTIS.Common.Core.Impl.Reporting.Models;

namespace TessanTIS.Common.Core.Impl.Reporting
{
    public static class XmlReportHelper
    {
        private static readonly IExtendedXmlSerializer serialize = new ConfigurationContainer().UseAutoFormatting()
                                                                    .UseOptimizedNamespaces()
                                                                    .EnableImplicitTyping(typeof(ExecutionReportResult))
                                                                    .Ignore(typeof(List<>).GetProperty(nameof(List<object>.Capacity)))
                                                                    .Create();
        public static ExecutionReportResult ExecutionResult { get; set; } = new ExecutionReportResult();

        public static void AddTest(string testName)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult == null)
            {
                testResult = new TestResult { Name = testName, StartTime = DateTime.Now };
                ExecutionResult.Results.Add(testResult);
            }
        }

        public static void EndStep(string testName, int stepNumber)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult == null)
            {
                testResult = new TestResult { Name = testName, StartTime = DateTime.Now };
                ExecutionResult.Results.Add(testResult);
                testResult.Steps.Add(new StepResult { Number = stepNumber, EndTime = DateTime.Now });
            }
            else
            {
                StepResult step = testResult.Steps.FirstOrDefault(x => x.Number == stepNumber);
                if (step == null)
                {
                    testResult.Steps.Add(new StepResult { Number = stepNumber, EndTime = DateTime.Now });
                }
                else
                {
                    step.EndTime = DateTime.Now;
                }
            }
        }

        public static void SaveReport(string path)
        {
            var document = serialize.Serialize(new XmlWriterSettings { Indent = true }, ExecutionResult);
            File.WriteAllText(path, document);
        }

        public static void SetStepStatus(string testName, int stepNumber, Status stepStatus, string stepDescription)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult != null)
            {
                StepResult step = testResult.Steps.FirstOrDefault(x => x.Number == stepNumber);
                if (step != null)
                {
                    if (step.Result != Status.Failed)
                        step.Result = stepStatus;
                    step.Actual = stepDescription;
                    step.EndTime = DateTime.Now;
                }
            }
        }

        public static void SetTestStaus(string testName, Status status)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult != null)
            {
                testResult.Result = status;
                testResult.EndTime = DateTime.Now;
            }
        }
        public static void StartStep(string testName, int stepNumber)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult == null)
            {
                testResult = new TestResult { Name = testName, StartTime = DateTime.Now, EndTime = DateTime.Now };
                ExecutionResult.Results.Add(testResult);
            }
            testResult.Steps.Add(new StepResult { Number = stepNumber, StartTime = DateTime.Now, Actual = string.Empty });
        }

        public static void SetTestStatus(string testName,Status status)
        {
            TestResult testResult = ExecutionResult.Results.FirstOrDefault(x => x.Name == testName);
            if (testResult != null)
            {
                testResult.Result = status;
                testResult.EndTime = DateTime.Now;
            }
        }
    }
}

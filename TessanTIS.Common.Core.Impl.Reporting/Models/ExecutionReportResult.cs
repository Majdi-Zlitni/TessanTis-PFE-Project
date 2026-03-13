using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Impl.Reporting.Models
{
    public enum Status
    {
        Passed,
        Failed,
        NotCompleted,
        Warning,
        Blocked
    }
    public class ExecutionReportResult
    {
        public List<TestResult> Results { get; set; } = new List<TestResult>();
    }
}

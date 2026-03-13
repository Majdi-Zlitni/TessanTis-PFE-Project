using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Impl.Reporting.Models
{
    public class TestResult
    {
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public Status Result { get; set; }
        public DateTime StartTime { get; set; }
        public List<StepResult> Steps { get; set; } = new List<StepResult>();
    }
}

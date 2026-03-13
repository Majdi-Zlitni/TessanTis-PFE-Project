using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Impl.Reporting.Models
{
    public class StepResult
    {
        public string Actual { get; set; }
        public DateTime EndTime { get; set; }
        public string ExecDate { get { return EndTime.Date.ToString("MM/dd/yyyy"); } set { } }
        public string ExecTime { get { return EndTime.Date.ToString(); } set { } }
        public string Name { get; set; }
        public int Number { get; set; }
        public Status Result { get; set; }
        public DateTime StartTime { get; set; }
    }
}

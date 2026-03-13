using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Impl.Data.Modles
{
    public class TestCaseWorkflow
    {
        public int StepNumber { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }
        public bool Enabled { get; set; }
        public bool Execute { get; set; }
    }
}

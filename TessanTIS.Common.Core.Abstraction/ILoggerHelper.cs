using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Abstraction
{
    public interface ILoggerHelper
    {
        public void Debug(string message, string arg = null);
        public void Info(string message, string arg = null);
        public void Error(string message, string arg = null);
        public void Warning(string message,string arg=null);
    }
}

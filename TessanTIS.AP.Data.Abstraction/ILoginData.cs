using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.AP.Data.Abstraction
{
    public interface ILoginData
    {
        public IDataHelper helper { get; set; }
        public string InValidPassword { get;}
        public string InValidUserName { get; }
        public string Url { get; }
        public string ValidPassword { get;  }
        public string ValidUserName { get; }
    }
}

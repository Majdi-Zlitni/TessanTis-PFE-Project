using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.OrangeHRM.Data.Abstraction
{
    public interface IOrangeHRMLoginData
    {
        IDataHelper helper { get; set; }
        string InValidPassword { get; }
        string InValidUserName { get; }
        string Url { get; }
        string ValidPassword { get; }
        string ValidUserName { get; }
    }
}

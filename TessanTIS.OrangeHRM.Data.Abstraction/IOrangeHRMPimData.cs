using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.OrangeHRM.Data.Abstraction
{
    public interface IOrangeHRMPimData
    {
        IDataHelper helper { get; set; }
        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        string ImagePath { get; }
    }
}
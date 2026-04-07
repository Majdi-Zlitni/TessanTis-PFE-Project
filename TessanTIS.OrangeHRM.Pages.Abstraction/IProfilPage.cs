using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.OrangeHRM.Pages.Abstraction
{
    public interface IProfilPage
    {
        bool IsBrandLogoDisplayed(int stepNumber);
        bool IsDashboardHeaderDisplayed(int stepNumber);
        bool IsSidebarDashboardDisplayed(int stepNumber);
        string GetPageTitle(int stepNumber);
        string GetUserName(int stepNumber);
    }
}


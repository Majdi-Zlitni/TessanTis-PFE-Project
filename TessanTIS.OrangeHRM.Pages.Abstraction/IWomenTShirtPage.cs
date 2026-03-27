using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.OrangeHRM.Pages.Abstraction
{
    public interface IWomenTShirtPage
    {
        string GetFirstProductName(int stepNumber);
        void Search(int stepNumber, string productName);
    }
}


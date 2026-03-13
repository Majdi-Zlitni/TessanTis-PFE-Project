using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Pages.Abstraction
{
    public interface IWomenTShirtPage
    {
        string GetFirstProductName(int stepNumber);
        void Search(int stepNumber, string productName);
    }
}

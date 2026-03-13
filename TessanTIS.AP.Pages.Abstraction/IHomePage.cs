using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.AP.Pages.Abstraction
{
    public interface IHomePage
    {
        void ClickSignIn(int stepNumber);
        void ClickWomenTShirt(int stepNumber, string field);
    }
}

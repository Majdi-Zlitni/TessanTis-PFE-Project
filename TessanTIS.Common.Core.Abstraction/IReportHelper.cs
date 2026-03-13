using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TessanTIS.Common.Core.Abstraction
{
    public interface IReportHelper
    {
        public void AddTestFailureScreenshot(IWebDriver Driver);
        public void Close();
        public void CreateTest(string testName);
        public void EndStep(string testName, int stepNumber);
        public void Error(string message);
        public void Info(string message);
        public void SetStepStatusFail(string testName, int stepNumber, string stepDescription);
        public void SetStepStatusPass(string testName, int stepNumber, string stepDescription);
        public void SetStepStatusWarning(string testName, int stepNumber, string stepDescription);
        public void SetTestStatusFail(string testName, string message = null);
        public void SetTestStatusPass(string testName);
        public void SetTestStatusWarning(string testName);
        public void SetTestStatusSkipped(string testName);
        public void StartStep(string testName, int stepNumber);
        public void SetTestStatusBlocked(string testName);
        public void SetStepStatusBlocked(string testName, int stepNumber, string stepDescription);
    }
}

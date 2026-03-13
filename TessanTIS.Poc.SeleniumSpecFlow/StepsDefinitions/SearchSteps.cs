using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TessanTIS.Poc.SeleniumSpecFlow.Pages;

namespace TessanTIS.Poc.SeleniumSpecFlow.StepsDefinitions
{
    [Binding]
    class SearchSteps
    {
        readonly IWebDriver webDriver = new ChromeDriver();
        readonly PageSearch pageSearch = null;

        public SearchSteps() => pageSearch = new PageSearch(webDriver);

        [Given("Explore the courses")]
        public void ExploreTheCoursesInEdx() => pageSearch.ExploreTheCourses();

        [Given("I enter '(.*)' in input serach")]
        public void EnterSearchTerm(string inputTerm) => pageSearch.EnterInputSearch(inputTerm);


        [When("I click to search buttom")]
        public void WhenIClickToSearchButtom() => pageSearch.ClickSearch();


        [Then("Search results should contains '(.*)'")]

        public void ThenSearchResultShouldContains(string result)
        {
            string actualResult = pageSearch.ResultSearch();
            Assert.IsTrue(actualResult.Contains(result));
        }





    }
}

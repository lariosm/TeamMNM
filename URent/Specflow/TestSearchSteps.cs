using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace Specflow
{
    [Binding]
    public class TestSearchSteps : IDisposable
    {
        private string searchKeyword;
        private ChromeDriver driver;

        public TestSearchSteps()
        {
            driver = new ChromeDriver();
        }

        [Given(@"I have navigated to the URent website")]
        public void GivenIHaveNavigatedToTheURentWebsite()
        {
            driver.Navigate().GoToUrl("https://urent.azurewebsites.net");
            Assert.IsTrue(driver.Title.ToLower().Contains("home"));
        }
        
        [Given(@"I have typed a search word into the search bar")]
        public void GivenIHaveTypedASearchWordIntoTheSearchBar(string search)
        {
            this.searchKeyword = search.ToLower();
            var searchInputBox = driver.FindElementById("searchinput");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("searchbar")));
            searchInputBox.SendKeys(searchKeyword);
        }
        
        [When(@"I press the search button")]
        public void WhenIPressTheSearchButton()
        {
            var searchButton = driver.FindElementById("searchbutton");
            searchButton.Click();
        }
        
        [Then(@"I should be taken to the search result page")]
        public void ThenIShouldBeTakenToTheSearchResultPage()
        {
            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(driver.Url.ToLower().Contains(searchKeyword));
            Assert.IsTrue(driver.Title.ToLower().Contains("search"));
        }

        public void Dispose()
        {
            if(driver != null)
            {
                driver.Dispose();
                driver = null;
            }
        }
    }
}

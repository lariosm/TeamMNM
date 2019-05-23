using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace Specflow
{
    [TestClass]
    public class TestLogging
    {
        //    private string searchKeyword;
        //    private ChromeDriver driver;

        //    public void LoginSteps()
        //    {
        //        driver = new ChromeDriver();
        //    }


        //    [Given(@"I have registered as a user")]
        //    public void GivenIHaveRegisteredAsAUser()
        //    {
        //        driver.Navigate().GoToUrl("Https://urent.azurewebsites.net/Account/Login/");
        //        Assert.IsTrue(driver.Title.ToLower().Contains("Log in"));
        //    }

        //    [Given(@"I can input my email and password")]
        //    public void GivenICanInputMyEmailAndPassword()
        //    {
        //        driver.FindElementById("email").SendKeys("ml@me.com");
        //        driver.FindElement(By.Id("password")).SendKeys("Solutions1!");
        //    }

        //    [When(@"I press login")]
        //    public void WhenIPressLogin()
        //    {
        //        var submitButton = driver.FindElementById("submit");
        //        submitButton.Click();
        //    }

        //    [Then(@"I am logged into my account and redirected to the home page\.")]
        //    public void ThenIAmLoggedIntoMyAccountAndRedirectedToTheHomePage_()
        //    {
        //        System.Threading.Thread.Sleep(2000);
        //        Assert.IsTrue(driver.Title.ToLower().Contains("Home"));
        //    }

        //    public void Dispose()
        //    {
        //        if (driver != null)
        //        {
        //            driver.Dispose();
        //            driver = null;
        //        }
        //    }

        [TestMethod]
        public void UserLogin_Successful()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://urent.azurewebsites.net/Account/Login/");

            //Assert.IsTrue(driver.Title.ToLower().Contains("Log in"));

            driver.FindElement(By.Id("Email")).SendKeys("mlarios@me.com");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("Password")).SendKeys("Solutions1!");
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

            //var submitButton = driver.FindElement(By.Id("submit"));
            //submitButton.Click();

            //driver.FindElement(By.Id("Submit")).Click();

            System.Threading.Thread.Sleep(2000);

            driver.Close();
            driver.Quit();
        }

    }

    
}

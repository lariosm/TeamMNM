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
    public class TestUserRatingSteps
    {
        [Given(@"I have navigated to an item listing as a logged in user")]
        public void GivenIHaveNavigatedToAnItemListingAsALoggedInUser()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"click to view the user profile of the owner renting the item")]
        public void GivenClickToViewTheUserProfileOfTheOwnerRentingTheItem()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on a star and leave a review")]
        public void WhenIClickOnAStarAndLeaveAReview()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should be able to see my review on the owner on their user profile page")]
        public void ThenIShouldBeAbleToSeeMyReviewOnTheOwnerOnTheirUserProfilePage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

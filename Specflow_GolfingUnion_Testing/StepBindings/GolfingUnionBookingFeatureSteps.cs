using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;

namespace Specflow_GolfingUnion_Testing.StepBindings
{
    [Binding]
    public class GolfingUnionBookingFeatureSteps : HelperMethods
    {
        public static NameValueCollection appSettings = ConfigurationManager.AppSettings;

        IWebDriver driver = new ChromeDriver();
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        [Given(@"I have navigated to the GolfingUnion site")]
        public void GivenIHaveNavigatedToTheGolfingUnionSite()
        {
            driver.Navigate().GoToUrl(appSettings["Website_URL"]);
            Assert.IsTrue(IsElementPresent(driver, By.XPath("//img[contains(@alt, 'BRS Golf logo')]")));
        }

        [Given(@"I have entered a Username \+ Password")]
        public void GivenIHaveEnteredAUsernamePassword()
        {
            driver.FindElement(By.Id("login_form_username")).SendKeys(appSettings["UserName"]);
            driver.FindElement(By.Id("login_form_password")).SendKeys(appSettings["Password"]);
            driver.FindElement(By.Id("login_form_login")).Click();
            Assert.AreEqual(driver.Url, appSettings["Website_URL"] + "MembershipNumber");
        }

        [Given(@"I select the desired Date \+ Time")]
        public void GivenISelectTheDesiredDateTime()
        {
            driver.Navigate().GoToUrl(appSettings["Website_URL"]);
            driver.FindElement(By.XPath(appSettings["Xpath_Day_And_Date"])).Click();
            ReadOnlyCollection<IWebElement> searchForEmptyPlayerSpace = driver.FindElements(By.XPath("//span[contains(.,'Start typing to find player...')]"));
            searchForEmptyPlayerSpace[0].Click();
        }

        [When(@"I press book-now")]
        public void WhenIPressBook_Now()
        {
            driver.FindElement(By.XPath("//input[contains(@type, 'search')]")).SendKeys(appSettings["MemberName"]);
            driver.FindElement(By.XPath("//input[contains(@type, 'search')]")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("member_booking_form_confirm_booking")).Click();
            driver.FindElement(By.XPath("//button[contains(., 'OK')]")).Click();
            Assert.IsTrue(IsElementPresent(driver, By.XPath("//a[contains(text(),'Booking Confirmed')]")));
        }

        [Then(@"the user should be booked in for the selected time and date")]
        public void ThenTheUserShouldBeBookedInForTheSelectedTimeAndDate()
        {
            IWebElement bookingsMade = driver.FindElement(By.Id("booking-value"));
            Assert.Greater(bookingsMade.GetAttribute("value").Length, 1);
        }
    }
}

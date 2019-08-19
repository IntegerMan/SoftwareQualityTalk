using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Xunit;
using Shouldly;

namespace WebTesting
{
    public class FridayTests
    {
        private readonly ChromeDriver _driver;

        public FridayTests()
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void ItShouldNeverBeFriday() // I've run this test many times and it always passes
        {
            // Arrange
            string url = "http://www.isitfriday.org";

            // Act
            _driver.Navigate().GoToUrl(url);
            IWebElement span = _driver.FindElementById("isitfriday");

            // Assert
            span.Text.ShouldBe("No");
        }
    }
}


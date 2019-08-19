using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Shouldly;

namespace WebTesting
{
    public class FridayPageModelTests
    {
        private readonly ChromeDriver _driver;

        public FridayPageModelTests()
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void ItShouldNeverBeFriday() // I've run this test many times and it always passes
        {
            // Arrange
            string url = "http://www.isitfriday.org";
            var model = new FridayPage(_driver);

            // Act
            _driver.Navigate().GoToUrl(url);

            // Assert
            model.Answer.ShouldBe("No");
        }
    }
}


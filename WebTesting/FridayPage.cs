using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebTesting
{
    public class FridayPage
    {
        public IWebDriver Driver { get; }

        public FridayPage(IWebDriver driver)
        {
            Driver = driver;
            // Should work, but not found in .NET Core... missing something PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "isitfriday")]
        private IWebElement AnswerSpan { get; set; }

        public string Answer => AnswerSpan.Text;
    }
}
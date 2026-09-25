using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private const string Url = "https://www.saucedemo.com/";

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("[data-test='error']"));

        public void GoTo()
        {
            _driver.Navigate().GoToUrl(Url);
        }

        public void Login(string username, string password)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                return _wait.Until(d => ErrorMessage).Text;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

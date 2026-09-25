using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests.Pages
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private By InventoryContainer => By.Id("inventory_container");

        public bool IsLoaded()
        {
            try
            {
                _wait.Until(d => d.FindElement(InventoryContainer).Displayed);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

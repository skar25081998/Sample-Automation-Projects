using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTests.Utilities
{
    public class TestBase
    {
        protected IWebDriver Driver { get; private set; }

        [SetUp]
        public void SetUp()
        {
            // Ensure the correct ChromeDriver is downloaded for the installed Chrome version
            // Try to detect installed Chrome binary and request a matching chromedriver major version
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string[] possiblePaths = new string[] {
                System.IO.Path.Combine(programFilesX86, "Google", "Chrome", "Application", "chrome.exe"),
                System.IO.Path.Combine(programFiles, "Google", "Chrome", "Application", "chrome.exe")
            };

            string chromePath = null;
            foreach (var p in possiblePaths)
            {
                if (System.IO.File.Exists(p))
                {
                    chromePath = p;
                    break;
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(chromePath))
                {
                    var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(chromePath).FileVersion;
                    if (!string.IsNullOrEmpty(version))
                    {
                        // Request chromedriver matching the full browser version
                        new DriverManager().SetUpDriver(new ChromeConfig(), version);
                    }
                    else
                    {
                        new DriverManager().SetUpDriver(new ChromeConfig());
                    }
                }
                else
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                }
            }
            catch
            {
                // If WebDriverManager fails, fall back to default behavior and let the test fail with clear error
                new DriverManager().SetUpDriver(new ChromeConfig());
            }

            var options = new ChromeOptions();
            // Uncomment the following line to run headless
            // options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch
            {
                // ignore errors on shutdown
            }
        }
    }
}

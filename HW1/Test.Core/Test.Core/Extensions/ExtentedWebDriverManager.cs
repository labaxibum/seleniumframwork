using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Extensions
{
    public static class ExtentedWebDriverManager
    {
        public static IWebDriver CreateRemoteDriver(string browserName, string platformName, string remoteUrl)
        {
            IWebDriver driver = null;

            if (browserName.ToLower() == "chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.PlatformName = platformName;
                driver = new RemoteWebDriver(new Uri(remoteUrl), options.ToCapabilities(), TimeSpan.FromMinutes(1));
            }

            else if (browserName.ToLower() == "firefox")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.PlatformName = platformName;
                driver = new RemoteWebDriver(new Uri(remoteUrl), new FirefoxOptions().ToCapabilities(), TimeSpan.FromMinutes(1));
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;

        }
        public static IWebElement WaitForElementToBeVisible(this IWebDriver driver, By locator, float timeout)
        {
            if (timeout > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(locator));

            }
            return driver.FindElement(locator);
        }
        public static IWebElement WaitForElementToBeClickable(this IWebDriver driver, By locator, float timeout)
        {
            if (timeout > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(locator));

            }
            return driver.FindElement(locator);
        }
        public static void Click(this IWebDriver driver, By locator, float timeout = 30)
        {
            Actions action = new Actions(driver);
            IWebElement element = driver.WaitForElementToBeClickable(locator, timeout);
            action.MoveToElement(element).Click().Build().Perform();
        }
    /*    public static void Submit(this IWebDriver driver, By locator, float timeout = 30)
        {
            Actions action = new Actions(driver);
            IWebElement element = driver.WaitForElementToBeClickable(locator, timeout);
            element.Submit();
        }*/

        public static void SendKeys(this IWebDriver driver, string key, By locator, float timeout = 30)
        {
            Actions action = new Actions(driver);
            IWebElement element = driver.WaitForElementToBeVisible(locator, timeout);
            action.MoveToElement(element).SendKeys(key).Build().Perform();
        }
        public static string GetAttribute(this IWebDriver driver, string attributeName)
        {
            return driver.GetAttribute(attributeName);
        }

    }

}

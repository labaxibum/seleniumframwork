using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Core.Drivers
{
    public class DriverManager
    {
        private static AsyncLocal<WebDriver> driver = new AsyncLocal<WebDriver>();
        public static WebDriver GetCurrentDriver()
        {
            return driver.Value;
        }
       
    }
}

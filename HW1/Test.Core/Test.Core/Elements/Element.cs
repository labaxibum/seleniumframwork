using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Drivers;
using Test.Core.Extensions;

namespace Test.Core.Elements
{
    public class Element
    {
        protected By locator;
        protected float timeout = 30;

        public Element(By locator)
        {
            this.locator = locator;
        }

        public Element(By locator, float timeOutInSecond)
        {
            this.locator = locator;
            this.timeout = timeOutInSecond;
        }

        public Element SetTimeout(float timeOutInSecond)
        {
            this.timeout = timeOutInSecond;
            return this;
        }
        public By GetLocator()
        {
            return this.locator;
        }

        public IWebElement WaitForVisibility()
        {
            return DriverManager.GetCurrentDriver().WaitForElementToBeVisible(this.locator,this.timeout);
        }
        public void Click() => DriverManager.GetCurrentDriver().Click(this.locator);

        public void Submit() => WaitForVisibility().Submit();

        public void SendKeys(string key) => DriverManager.GetCurrentDriver().SendKeys(key,this.locator);

        public string GetAttribute(string attributeName) => WaitForVisibility().GetAttribute(attributeName);

        public string GetText() => WaitForVisibility().Text;

        public bool IsDisplayed() => WaitForVisibility().Displayed;

        public bool IsSelected() => WaitForVisibility().Selected;
        
        public bool IsEnabled() => WaitForVisibility().Enabled;

        public void Clear() => WaitForVisibility().Clear();

        public IWebElement FindElement(By element)
        {
            WaitForVisibility();
            return DriverManager.GetCurrentDriver().FindElement(element);   
        }
        
        public IList<IWebElement> FindElements(By element)
        {
            WaitForVisibility();
            return DriverManager.GetCurrentDriver().FindElements(element) ;

        }
    }
}

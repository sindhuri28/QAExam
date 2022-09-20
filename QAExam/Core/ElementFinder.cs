using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace QAExam.Core
{
    public static class ElementFinder
    {
        public static IWebElement FindControl(this IWebDriver driver, By findBy, bool ensureVisibility = false,
            bool isClickable = false, bool moveElement = false)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                var element = wait.Until(drv => drv.FindElement(findBy));

                if (ensureVisibility)
                {
                    wait.Until(drv => element.Displayed);
                    wait.Until(drv => element.Enabled);
                }

                if (isClickable)
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                }

                if (moveElement)
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

                    jse.ExecuteScript("scroll(0, 250)");
                }

                return element;
            }
            catch (Exception)
            {
                Console.WriteLine($"INFO: Element not found.\n{findBy}");
                throw;
            }
        }

        public static bool IsElementDisplayed(this IWebDriver driver, By element)
        {
            if (driver.FindElements(element).Count > 0)
            {
                if (driver.FindElement(element).Displayed)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By element, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + element + "' was not found in current context page.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementVisible(this IWebDriver driver, By element, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + element + "' was not found.");
                throw;
            }
        }

        public static bool WaitUntilElementIsNotVisible(this IWebDriver driver, By element, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + element + "' was not found.");
                throw;
            }
        }
    }
}

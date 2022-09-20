using QAExam.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace QAExam.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver = BaseStepDefinition.driver;

        public BasePage(string pageUrl = "")
        {

            PageFactory.InitElements(Driver, this);

            new WebDriverWait(Driver, TimeSpan.FromSeconds(60))
                .Until(drv =>
                    ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            if (!string.IsNullOrWhiteSpace(pageUrl))
                new WebDriverWait(Driver, TimeSpan.FromSeconds(60))
                    .Until(drv => drv.Url.Contains(pageUrl));
        }
    }
}

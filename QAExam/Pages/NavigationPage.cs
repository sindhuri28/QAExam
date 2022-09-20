using QAExam.Core;
using OpenQA.Selenium;
using System.Configuration;

namespace QAExam.Pages
{
    public class NavigationPage : BasePage
    {
        public void NavigateToConfluenceCloud()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
        }

        public void NavigateToPage(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.WaitUntilElementVisible(By.Id("title-text"));
        }
    }
}

using QAExam.Core;
using OpenQA.Selenium;

namespace QAExam.Pages
{
    public class LoginPage : BasePage
    {
        private IWebElement emailId => Driver.FindControl(By.Id("username"));

        private IWebElement submit => Driver.FindControl(By.Id("login-submit"));

        private IWebElement password => Driver.FindControl(By.Id("password"));

        public void Login(string email, string pass)
        {
            emailId.SendKeys(email);
            submit.Click();
            Driver.WaitUntilElementVisible(By.Id("password"));
            password.Clear();
            password.SendKeys(pass);
            submit.Click();
            Driver.WaitUntilElementVisible(By.XPath("//span[text()='Home']"));
        }
    }
}

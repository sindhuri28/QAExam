using System.Configuration;
using OpenQA.Selenium;
using NUnit.Framework;
using QAExam.Core;

namespace QAExam.Pages
{
    public class RestrictionsPage : BasePage
    {
        private LoginPage LoginPage = new LoginPage();
        private IWebElement editRestrictionButton => Driver.FindElement(By.Id("system-content-items-extracted"));

        private IWebElement restrictionsDropdown => Driver.FindElement(By.CssSelector(".css-hkzqy0-singleValue"));

        private IWebElement apply => Driver.FindElement(By.XPath("//span[text()='Apply']"));

        private IWebElement logOut => Driver.FindElement(By.XPath("//span[text()='Log Out']"));

        private IWebElement logOutSubmitButton => Driver.FindElement(By.Id("logout-submit"));

        private IWebElement userProfile => Driver.FindElement(By.CssSelector("span[class='css-4gwzgw']"));

        private IWebElement permission(string permissionType) => Driver.FindElement(By.XPath($"//span[text()='{permissionType}']"));


        public void EditRestriction(string permissionType)
        {

            editRestrictionButton.Click();

            //Wait for restrictions drop down to be available
            Driver.WaitUntilElementVisible(By.CssSelector("div[data-test-id='restrictions-value-container']"));

            restrictionsDropdown.Click();
            permission(permissionType).Click();
            apply.Click();

            //Wait for modal to disappear
            Driver.WaitUntilElementIsNotVisible(By.CssSelector(".css-1b8uygf"));
        }

        public void validatePermission(string expectedresult)
        {
            //logout as Owner
            userProfile.Click();
            Driver.WaitUntilElementVisible(By.XPath("//span[text()='Log Out']"));
            logOut.Click();
            logOutSubmitButton.Click();

            //Login as member
            LoginPage.Login(ConfigurationManager.AppSettings["MemeberEmail"], ConfigurationManager.AppSettings["MemberPassword"]);
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["RestrictionPageURL"]);

            //validate the permission
            if (expectedresult == "Anyone can view and edit")
            {
                Assert.IsTrue(Driver.FindElement(By.Id("editPageLink")).Displayed);
            }
            else if (expectedresult == "Anyone can view and only users with permission can edit")
            {
                Assert.IsFalse(Driver.IsElementDisplayed(By.Id("editPageLink")));
            }
            else if (expectedresult == "Only users with permission to view can view and users with permission to edit can edit")
            {
                Assert.IsTrue(Driver.FindElement(By.XPath("//span[contains(text(),'restricted content')]")).Displayed);
            }
        }
    }
}

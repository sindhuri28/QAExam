using TechTalk.SpecFlow;
using QAExam.Pages;
using System.Configuration;

namespace QAExam.Steps
{
    [Binding]
    public class UserRestrictionStep : BaseStepDefinition
    {
        private NavigationPage NavigationPage = new NavigationPage();

        private LoginPage LoginPage = new LoginPage();

        private RestrictionsPage restrictionsPage = new RestrictionsPage();

        [Given(@"I login to confluence cloud")]
        public void GivenILoginToConfluenceCloud()
        {
            NavigationPage.NavigateToConfluenceCloud();
            LoginPage.Login(ConfigurationManager.AppSettings["OwnerEmail"], ConfigurationManager.AppSettings["OwnerPassword"]);
        }

        [Given(@"I navigate to the page to configure permissions")]
        public void GivenINavigateToThePageToConfigurePermissions()
        {
            NavigationPage.NavigateToPage(ConfigurationManager.AppSettings["RestrictionPageURL"]);

        }

        [When(@"I apply ""(.*)""")]
        public void WhenIApply(string permission)
        {
            restrictionsPage.EditRestriction(permission);
        }

        [Then(@"I see ""(.*)""")]
        public void ThenISee(string expectedResult)
        {
            restrictionsPage.validatePermission(expectedResult);
        }


    }
}

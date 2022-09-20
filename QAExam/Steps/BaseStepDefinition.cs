using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using QAExam.Core;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Net;
using TechTalk.SpecFlow;
using System.Reflection;

namespace QAExam.Steps
{
    [Binding]
    public class BaseStepDefinition
    {
        public static IWebDriver driver = null;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        [BeforeTestRun]
        private static void WarmUpIis()
        {
            var webClient = new WebClient { Proxy = null };
            var requestUrl = ConfigurationManager.AppSettings["URL"];
            webClient.DownloadString(requestUrl);
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter(@"C:\Users\mukesh\Downloads\Bizcover_QA_PracticalExam\TestReport.html");
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {

            extent.Flush();
        }

        [BeforeFeature]
        [Obsolete]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        [Obsolete]
        public static void Initialize()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [Before]
        private void BeforeScenario()
        {
            driver = WebDriverFactory.CreateDriver((DriverType)Enum.Parse(typeof(DriverType), ConfigurationManager.AppSettings["DriverType"]));
        }

        [After]
        private void Teardown()
        {

            driver.Quit();

        }

        //[AfterStep]
        //[Obsolete]
        //public static void InsertReportingSteps()
        //{
        //    var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

        //    if (stepType == "Given")
        //        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
        //    else if (stepType == "When")
        //        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
        //    else if (stepType == "Then")
        //        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
        //    else if (stepType == "And")
        //        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
        //}

        [AfterStep]
        [Obsolete]
        private void AfterStep()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                return;
            }

            Console.WriteLine("INFO: Failed at URL:" + driver.Url);

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
/*
  nuget Selenium.WebDriver v3.141
  NuGet OpenQA.Selenium.Support.UI v3.141
*/
namespace UITestCarsAsyncREST
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webDrivers";
        private static IWebDriver _driver;

        // https://www.automatetheplanet.com/mstest-cheat-sheet/
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // _driver = new ChromeDriver(DriverDirectory); // fast
            // if your Chrome browser was updated, you must update the driver as well ...
            //    https://chromedriver.chromium.org/downloads
            _driver = new FirefoxDriver(DriverDirectory);  // slow
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string url = "http://localhost:3000/";
            _driver.Navigate().GoToUrl("file:///C:/andersb/javascript/carsVue3/index.htm");

            string title = _driver.Title;
            Assert.AreEqual("Car Shop", title);

            IWebElement buttonElement = _driver.FindElement(By.Id("getAllButton"));
            buttonElement.Click();

            //IWebElement carList = _driver.FindElement(By.Id("carlist")); // No such element

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement carList = wait.Until(d => d.FindElement(By.Id("carlist")));
            Assert.IsTrue(carList.Text.Contains("Volvo"));
        }
    }
}

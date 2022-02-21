using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace SeleniumPOM
{

    [TestClass]
    public class PlayGridTests
    {

        public IWebDriver InitDriver()
        {
            var dir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            return new ChromeDriver(dir);
        }

        [TestMethod]
        public void CheckPageHeader()
        {
            // This is setup block. Normally driver setup steps would be moved elsewhere
            IWebDriver driver = InitDriver();
            PageObject pageObject = new PageObject(driver);
            PageNavigator navigator = new PageNavigator(driver);

            // This is test itself
            navigator.GoToHomepage();
            Assert.AreEqual("Play it is fun !!!", pageObject.GetHeaderText());

            // This is cleanup block. Same for disposing driver, should be moved elsewhere
            driver.Dispose();
        }

        [TestMethod]
        public void CheckIconIsEnabledWhenClicked()
        {
            IWebDriver driver = InitDriver();
            PageObject pageObject = new PageObject(driver);
            PageNavigator navigator = new PageNavigator(driver);

            // Navigating to 3x3 grid using query strings
            navigator.BuildGridAndNavigate(3, 3);
            navigator.ClickIconByGridPosition(1, 1);
            Assert.AreEqual("true", pageObject.GetIconByGridPosition(1,1).GetAttribute("active"));

            driver.Dispose();
        }

        [TestMethod]
        public void Check3on3GridDialogBoxOpenedSuccess()
        {
            IWebDriver driver = InitDriver();
            PageNavigator navigator = new PageNavigator(driver);

            // Navigating to 3x3 grid using query strings
            navigator.BuildGridAndNavigate(3, 3);
            navigator.ClickIconByGridPosition(1, 1);
            navigator.ClickIconByGridPosition(1, 2);
            navigator.ClickIconByGridPosition(1, 3);
            navigator.ClickIconByGridPosition(2, 1);
            navigator.ClickIconByGridPosition(2, 3);
            navigator.ClickIconByGridPosition(3, 1);
            navigator.ClickIconByGridPosition(3, 2);
            navigator.ClickIconByGridPosition(3, 3);

            // theoretically this is against coding practice, as it should be moved to separate bool method like isAlertPresent
            try
            {
                driver.SwitchTo().Alert();
                Assert.IsTrue(true);
            }
            catch (NoAlertPresentException)
            {
                Assert.IsTrue(false,"Dialog box was not opened ");
            }

            driver.Dispose();
        }

        [TestMethod]
        public void CheckGridUpdatesAfterEnteringNewValues()
        {
            IWebDriver driver = InitDriver();
            PageObject pageObject = new PageObject(driver);
            PageNavigator navigator = new PageNavigator(driver);

            navigator.GoToHomepage();
            navigator.ForceAlertOnDefaultGrid();
            driver.SwitchTo().Alert().SendKeys("3");
            driver.SwitchTo().Alert().Accept();

            Assert.AreEqual("3x3", pageObject.GetGridParameters());

            driver.Dispose();
        }

        [TestMethod]
        public void Check3on3GridDialogDoesNotOpenIfInnerIconsAreClicked()
        {
            IWebDriver driver = InitDriver();
            PageNavigator navigator = new PageNavigator(driver);

            navigator.GoToHomepage();
            navigator.BuildGridAndNavigate(3, 3);
            navigator.ClickIconByGridPosition(1, 1);
            navigator.ClickIconByGridPosition(1, 2);
            navigator.ClickIconByGridPosition(1, 3);
            navigator.ClickIconByGridPosition(2, 1);
            navigator.ClickIconByGridPosition(2, 2);
            navigator.ClickIconByGridPosition(2, 3);
            navigator.ClickIconByGridPosition(3, 1);
            navigator.ClickIconByGridPosition(3, 2);
            navigator.ClickIconByGridPosition(3, 3);

            // again against practice, and should be moved to bool method
            try
            {
                driver.SwitchTo().Alert();
                Assert.IsTrue(false, "Dialog box was opened");
            }
            catch (NoAlertPresentException)
            {
                Assert.IsTrue(true);
            }

            driver.Dispose();
        }
    }
}

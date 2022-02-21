using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SeleniumPOM
{
    class PageNavigator
    {
        private readonly IWebDriver driver;
        // This should be moved to config file, but for now change this variable to match your doc path
        readonly string Url = "file:///C:/Users/Ram%C5%ABnasAndrijauskas/Downloads/QA%20Task.html";
        readonly SelectorFactory factory = new SelectorFactory();

        public PageNavigator(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToHomepage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void BuildGridAndNavigate(int height, int width)
        {
            string queryStrings = $"?height={height}&width={width}";
            string newUrl = Url + queryStrings;
            driver.Navigate().GoToUrl(newUrl);

        }

        public void ForceAlertOnDefaultGrid()
        {
            ClickIconByGridPosition(1, 1);
            ClickIconByGridPosition(1, 2);
            ClickIconByGridPosition(1, 3);
            ClickIconByGridPosition(1, 4);
            ClickIconByGridPosition(2, 1);
            ClickIconByGridPosition(2, 4);
            ClickIconByGridPosition(3, 1);
            ClickIconByGridPosition(3, 4);
            ClickIconByGridPosition(4, 1);
            ClickIconByGridPosition(4, 2);
            ClickIconByGridPosition(4, 3);
            ClickIconByGridPosition(4, 4);
        }

        public void ClickIconByGridPosition(int row, int column)
        {
            driver.FindElement(factory.GridCssSelectorByPosition(row, column)).Click();
        }

    }
}

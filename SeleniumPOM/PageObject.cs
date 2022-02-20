using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using static SeleniumPOM.PageNavigator;

namespace SeleniumPOM
{
    public class PageObject
    {
        private readonly IWebDriver driver;
        SelectorFactory factory = new SelectorFactory();

        public PageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetHeaderText ()
        {
            return driver.FindElement(factory.PageHeaderByCssSelector()).Text;
        }

        public IWebElement GetIconByGridPosition(int row, int column)
        {
            return driver.FindElement(factory.GridCssSelectorByPosition(row, column));
        }

        public string GetGridParameters()
        {
            int rows = driver.FindElement(factory.MainGridByCssSelector()).FindElements(By.ClassName("row")).Count;
            int columns = driver.FindElement(factory.FirstRowByCssSelector()).FindElements(By.ClassName("icon")).Count;
                
            return $"{rows}x{columns}";
        }
    }
}

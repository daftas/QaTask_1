using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPOM
{
    class SelectorFactory
    {

        public By PageHeaderByCssSelector()
        {
            return By.CssSelector("body > div > h1");
        }
        
        public By MainGridByCssSelector()
        {
            return By.CssSelector("body > div > div.mainGrid");
        }

        public By FirstRowByCssSelector()
        {
            return By.CssSelector("body > div > div.mainGrid > div:nth-child(1)");

        }

        public By GridCssSelectorByPosition(int row, int column)
        {
            string selector = $"body > div > div.mainGrid > div:nth-child({row}) > div:nth-child({column}";
            return By.CssSelector(selector);
        }
    }
}

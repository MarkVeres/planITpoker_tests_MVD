using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _01_planITpoker_clas_library_tests.Tests
{
    public class ScrollIntoView
    {
        IWebDriver driver;
        public ScrollIntoView(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void ScrollDown()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("scroll(0,450);");
        }
        public void ScrollUp()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("scroll(0,-450);");
        }
    }
}

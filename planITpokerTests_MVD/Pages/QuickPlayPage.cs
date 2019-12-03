using _01_planITpoker_clas_library_tests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using planITpokerTests_MVD.Pages;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_planITpoker_clas_library_tests
{
    public class QuickPlayPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By userName = By.CssSelector(".form-control");
        By enterButton = By.CssSelector("button.btn");
        By roomName = By.CssSelector(".page-header");

        public QuickPlayPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public QuickPlayPage(IWebDriver driver, string website)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = website;
        }

        public RoomsPage QuickPlayLogin(string inputName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userName)).SendKeys(inputName);
            driver.FindElement(enterButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-cancel"))).Click();
            RoomsPage room = new RoomsPage(driver, wait);
            return room;
        }
        public MultipleUserGamePage JoinQuickPlay(string inputName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userName)).SendKeys(inputName);
            driver.FindElement(enterButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(roomName));
            MultipleUserGamePage game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public RoomsPage QuickPlay(string inputName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userName)).SendKeys(inputName);
            driver.FindElement(enterButton).Click();
            RoomsPage room = new RoomsPage(driver, wait);
            return room;
        }
        public RoomsPage LeaveSiteRejoinQuickPlay()
        {
            driver.Url = "https://www.google.com/";
            driver.Url = "https://www.planitpoker.com/";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".btn-six"))).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-cancel"))).Click();
            var room = new RoomsPage(driver, wait);
            return room;
        }
    }
}
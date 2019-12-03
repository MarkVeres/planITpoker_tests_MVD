using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_planITpoker_clas_library_tests
{
    public class LoginPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By email = By.CssSelector(".ng-valid-email");
        By password = By.Name("inputPassword");
        By loginButton = By.CssSelector(".btn-default");

        public string Error  //used for Invalid Login Test Assert
        {
            get
            {
                return driver.FindElement(By.CssSelector(".ng-binding")).Text;
            }
        }

        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public RoomsPage LoginValid(string inputEmail, string inputPassword)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(email)).SendKeys(inputEmail);
            driver.FindElement(password).SendKeys(inputPassword);
            driver.FindElement(loginButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-cancel"))).Click();
            var room = new RoomsPage(driver, wait);
            return room;
        }
        public LoginPage LoginInvalid(string inputEmail, string inputPassword)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(email)).SendKeys(inputEmail);
            driver.FindElement(password).SendKeys(inputPassword);
            driver.FindElement(loginButton).Click();
            var login = new LoginPage(driver, wait);
            return login;
        }
    }
}

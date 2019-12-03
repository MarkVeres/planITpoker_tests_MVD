using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_planITpoker_clas_library_tests
{
    public class SignUpPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By userName = By.Name("inputName");
        By email = By.CssSelector(".ng-valid-email");
        By password = By.CssSelector("div.form-group:nth-child(3) > div:nth-child(1) > input:nth-child(2)");
        By signUpButton = By.CssSelector(".btn");

        public SignUpPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public string Error
        {
            get
            {
                return driver.FindElement(By.CssSelector(".ng-binding")).Text;
            }
        }

        public RoomsPage SignUpValid(string inputName, string inputEmail, string inputPassword)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userName)).SendKeys(inputName);
            driver.FindElement(email).SendKeys(inputEmail);
            driver.FindElement(password).SendKeys(inputPassword);
            driver.FindElement(signUpButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-cancel"))).Click();
            var room = new RoomsPage(driver, wait);
            return room;
        }

        public SignUpPage SignUpInvalid(string inputName, string inputEmail, string inputPassword)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userName)).SendKeys(inputName);
            driver.FindElement(email).SendKeys(inputEmail);
            driver.FindElement(password).SendKeys(inputPassword);
            driver.FindElement(signUpButton).Click();
            var signUp = new SignUpPage(driver, wait);
            return signUp;
        }
    }
}

using _01_planITpoker_clas_library_tests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using planITpokerTests_MVD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_planITpoker_clas_library_tests
{
    public class HomePage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By QuickplayButton = By.CssSelector(".btn-six");
        By SignUpButton = By.CssSelector(".btn-one");
        By LoginButton = By.CssSelector(".btn-primary");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = "https://www.planitpoker.com/";
        }
        public QuickPlayPage QuickPlay()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(QuickplayButton)).Click();
            var quick = new QuickPlayPage(driver, wait);
            return quick;
        }
        public SignUpPage SignUp()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SignUpButton)).Click();
            var signUp = new SignUpPage(driver, wait);
            return signUp;
        }
        public LoginPage Login()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LoginButton)).Click();
            var login = new LoginPage(driver, wait);
            return login;
        }

        public GamePage QuickPlayGame(string userName, string roomName, string story, string story2)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(QuickplayButton)).Click();
            var quick = new QuickPlayPage(driver, wait);
            var room = quick.QuickPlay(userName);
            var game = room.CreateRoom(roomName);
            var created = game.CreateStory(story, story2);
            return created;
        }
        public MultipleUserGamePage MultipleUserQuickPlayGame(string userName, string roomName, string story, string story2)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(QuickplayButton)).Click();
            var quick = new QuickPlayPage(driver, wait);
            var room = quick.QuickPlay(userName);
            var game = room.MultipleUserCreateRoom(roomName);
            var created = game.CreateStory(story, story2);
            return created;
        }
    }
}
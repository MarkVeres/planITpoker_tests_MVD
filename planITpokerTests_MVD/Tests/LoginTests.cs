using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace _01_planITpoker_clas_library_tests
{
    public class LoginTests : IDisposable
    {
        IWebDriver driver;

        public LoginTests()
        {
            this.driver = new FirefoxDriver();
        }

        [Fact]
        public void QuickplayLogin()
        {
            var home = new HomePage(driver);
            var quickPlay = home.QuickPlay();
            var room = quickPlay.QuickPlayLogin("Jack");
            Assert.Equal("Recent Rooms", room.Title);
        }
        [Fact]
        public void RememberQuickPlayUser()
        {
            var home = new HomePage(driver);
            var quickPlay = home.QuickPlay();
            quickPlay.QuickPlayLogin("Jack");
            var room = quickPlay.LeaveSiteRejoinQuickPlay();
            Assert.Equal("Recent Rooms", room.Title);
        }
        [Fact]
        public void SignUpValidCredentials()
        {
            var home = new HomePage(driver);
            var signUp = home.SignUp();
            var room = signUp.SignUpValid("Jack", "automationcounts@gmail.com", "123password098");
            Assert.Equal("Recent Rooms", room.Title);
        }
        [Fact]
        public void LoginValidCredentials()
        {
            var home = new HomePage(driver);
            var login = home.Login();
            var room = login.LoginValid("automationcounts@gmail.com", "123password098");
            Assert.Equal("Recent Rooms", room.Title);
        }
        [Fact]
        public void SignUpInvalidCredentials()
        {
            var home = new HomePage(driver);
            var signUp = home.SignUp();
            var room = signUp.SignUpInvalid("Jack", "asdf", "123password098");
            Assert.Equal("Invalid email address.", signUp.Error);
        }
        [Fact]
        public void LoginInvalidCredentials()
        {
            var home = new HomePage(driver);
            var login = home.Login();
            var room = login.LoginInvalid("asdf", "123password098");
            Assert.Equal("Invalid email address.", login.Error);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
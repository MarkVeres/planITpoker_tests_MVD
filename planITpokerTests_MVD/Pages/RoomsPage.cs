using _01_planITpoker_clas_library_tests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using planITpokerTests_MVD.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_planITpoker_clas_library_tests
{
    public class RoomsPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By createRoomName = By.Id("createRoomNameInput");
        By createButton = By.ClassName("btn-ok");
        By createNewRoomButton = By.CssSelector(".btn-create-room");
        By roomName = By.CssSelector(".name-td");
        By resetGameRoom = By.CssSelector(".fa-undo");
        By editGameRoom = By.CssSelector(".edit-icon");
        By deleteGameRoom = By.CssSelector(".delete-icon");
        By doYouWantStories = By.CssSelector("form.ng-valid > div:nth-child(3) > div:nth-child(1) > label:nth-child(1)");
        By autoRevealVotes = By.CssSelector("form.ng-valid > div:nth-child(6) > div:nth-child(1) > label:nth-child(1) > span:nth-child(1)");
        By allowPlayersChangeVote = By.CssSelector("form.ng-valid > div:nth-child(7) > div:nth-child(1) > label:nth-child(1) > span:nth-child(1)");
        By countdownTimer = By.CssSelector("form.ng-valid > div:nth-child(8) > div:nth-child(1) > label:nth-child(1) > span:nth-child(1) > input:nth-child(1)");

        public RoomsPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public string Title   //used for Several LoginTests Test Asserts
        {
            get
            {
                return driver.FindElement(By.CssSelector(".col-sm-9 > div:nth-child(1)")).Text;
            }
        }
        public IEnumerable RoomList  //used for DeleteGameRoom Test Assert
        {
            get
            {
                return driver.FindElements(By.ClassName(".clickable"));
            }
        }
        public string TotalTime  //used for ResetGameRoom Test Assert
        {
            get
            {
                return driver.FindElement(By.CssSelector("td.hidden-xs:nth-child(3) > div:nth-child(2)")).Text;
            }
        }
        public GamePage CreateRoom(string inputroomName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createRoomName)).SendKeys(inputroomName);
            driver.FindElement(createButton).Click();
            GamePage game = new GamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage MultipleUserCreateRoom(string inputroomName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createRoomName)).SendKeys(inputroomName);
            driver.FindElement(createButton).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public RoomsPage ResetGameRoom()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(resetGameRoom)).Click();
            driver.SwitchTo().Alert().Accept();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(roomName));
            RoomsPage room = new RoomsPage(driver, wait);
            return room;
        }
        public GamePage NoStories(string inputRoomName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createRoomName)).SendKeys(inputRoomName);
            driver.FindElement(doYouWantStories).Click();
            driver.FindElement(createButton).Click();
            GamePage game = new GamePage(driver, wait);
            return game;
        }
        public RoomsPage DeleteGameRoom()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(deleteGameRoom)).Click();
            driver.SwitchTo().Alert().Accept();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(createNewRoomButton));
            var room = new RoomsPage(driver, wait);
            return room;
        }
        public GamePage AllowPlayersChangeVote(string inputRoomName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createRoomName)).SendKeys(inputRoomName);
            driver.FindElement(allowPlayersChangeVote).Click();
            driver.FindElement(createButton).Click();
            GamePage game = new GamePage(driver, wait);
            return game;
        }
        public GamePage AddCountdownTimer(string inputRoomName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createRoomName)).SendKeys(inputRoomName);
            driver.FindElement(countdownTimer).Click();
            driver.FindElement(createButton).Click();
            GamePage game = new GamePage(driver, wait);
            return game;
        }

        //not using this At the moment, might use later
        //public GamePage AutoRevealVotes()
        //{
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(editGameRoom)).Click();
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(autoRevealVotes)).Click();
        //    var game = new GamePage(driver, wait);
        //    return game;
        //}
    }
}
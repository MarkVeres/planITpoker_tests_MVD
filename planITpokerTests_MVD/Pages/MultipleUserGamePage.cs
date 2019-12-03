using _01_planITpoker_clas_library_tests;
using _01_planITpoker_clas_library_tests.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace planITpokerTests_MVD.Pages
{
    public class MultipleUserGamePage
    {
        IWebDriver driver;
        WebDriverWait wait;
        By startButton = By.CssSelector("#btn-start");
        By voteValueOne = By.CssSelector("span.ng-scope:nth-child(3) > span:nth-child(1)");
        By voteValueTwo = By.CssSelector("div.player:nth-child(2) > div:nth-child(3)");
        By timer = By.CssSelector(".timer > div:nth-child(1) > span:nth-child(2)");
        By playerOneAvatar = By.CssSelector("div.player:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1)");
        By playerTwoAvatar = By.CssSelector("div.player:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1)");
        By toastError = By.ClassName("toast-message");
        By moderatorRole = By.CssSelector(".open > ul:nth-child(2) > li:nth-child(1) > a:nth-child(1)");
        By observerRole = By.CssSelector(".open > ul:nth-child(2) > li:nth-child(2) > a:nth-child(1)");
        By createStory = By.CssSelector(".create-story-textarea > div:nth-child(1) > div:nth-child(1) > textarea:nth-child(1)");
        By saveAndAddNewStory = By.CssSelector("div.margin-bottom:nth-child(1) > button:nth-child(1)");
        By saveAndCloseButton = By.CssSelector("div.margin-bottom:nth-child(2) > button:nth-child(1)");
        By endTour = By.CssSelector("button.btn:nth-child(3)");
        By finishVoting = By.CssSelector(".control1 > div:nth-child(1) > div:nth-child(2) > button:nth-child(1)");
        By removeUserButton = By.CssSelector(".open > ul:nth-child(2) > li:nth-child(4) > a:nth-child(1)");
        By estimates = By.Id("finalEstimate");
        By account = By.CssSelector("a.dropdown-toggle");
        By accountRooms = By.CssSelector("li.collapsed:nth-child(4) > a:nth-child(1)");
        By clearVotes = By.CssSelector(".controls > div:nth-child(1) > div:nth-child(3) > button:nth-child(1)");
        By skipStory = By.CssSelector(".controls > div:nth-child(1) > div:nth-child(4) > button:nth-child(1)");

        public MultipleUserGamePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public string InviteLink  //used for all MultipleUserTests to join game
        {
            get
            {
                return driver.FindElement(By.CssSelector("#invite-link")).GetAttribute("value").ToString();
            }
        }
        public string RoomName   //used for MultipleUsers Test Assert
        {
            get
            {
                return driver.FindElement(By.CssSelector(".page-header")).Text;
            }
        }
        public string Timer  //used for ObserverSeesPlayersVotingInRealTime Test Assert
        {
            get
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(timer)).Text;
            }
        }
        public string PlayerTwoName  //used for DeAssignRoleOfModerator Test Assert
        {
            get
            {
                return driver.FindElement(By.CssSelector("div.player:nth-child(2) > div:nth-child(2) > div:nth-child(1) > span:nth-child(1)")).Text;
            }
        }
        public string VoteValueOne  //used for ObserverSeesPlayersVoteingInRealTime Test Assert
        {
            get
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(voteValueOne)).Text;
            }
        }
        public string VoteValueTwo  //used for NewUserJoinAndVoteAfterVotingStarts Test Assert
        {
            get
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(voteValueTwo)).Text;
            }
        }
        public IWebElement ClearVotesButton   //used for CanPlayerClearVotes test Assert
        {
            get
            {
                return driver.FindElement(clearVotes);
            }
        }
        public IWebElement SkipStoryButton   //used for CanPlayerSkipStories test Assert
        {
            get
            {
                return driver.FindElement(skipStory);
            }
        }
        public IWebElement ResetTimerButton
        {
            get
            {
                return driver.FindElement(By.CssSelector(".controls > div:nth-child(1) > div:nth-child(1) > button:nth-child(1)"));
            }
        }
        public IWebElement FinishVotingButton  //used for ModeratorCanFinishVotingOnlyAfterAllUsersVoted Test Assert
        {
            get
            {
                return driver.FindElement(finishVoting);
            }
        }
        public bool PlayerList  //used for ModeratorCanRemovePlayer Test Assert
        {
            get
            {
                bool exist = driver.FindElements(By.CssSelector(".players-list")).Count == 1;
                return exist;
            }
        }
        public string ToastMessage    //used for VoteLeaveSiteAndVoteAgain Test Assert
        {
            get
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(toastError)).Text;
            }
        }
        public MultipleUserGamePage CreateStory(string inputStory, string inputStory2)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(createStory)).SendKeys(inputStory);
            driver.FindElement(saveAndAddNewStory).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(createStory)).SendKeys(inputStory2);
            driver.FindElement(saveAndCloseButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(endTour)).Click();
            driver.FindElement(toastError).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage Start()
        {
            driver.FindElement(startButton).Click();
            driver.FindElement(toastError).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage Vote(int num)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(playerOneAvatar));
            string card = Convert.ToString(num);
            var cardsList = driver.FindElements(By.CssSelector(".cards li button"));
            var selectedCard = cardsList.First(e => e.FindElement(By.TagName("div")).Text == card);
            selectedCard.Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage SendEstimate(int num)
        {
            string numS = Convert.ToString(num);
            var estimate = driver.FindElement(estimates);
            var selectElement = new SelectElement(estimate);
            selectElement.SelectByText(numS);
            driver.FindElement(toastError).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage ClearVotes()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(clearVotes)).Click();
            driver.FindElement(toastError).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage FinishVoting()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(finishVoting)).Click();
            driver.FindElement(toastError).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage ClickPlayerOneAvatar()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(playerOneAvatar)).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage ClickPlayerTwoAvatar()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(playerTwoAvatar)).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage ClickModeratorRole()
        {
            driver.FindElement(moderatorRole).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage ClickObserverRole()
        {
            driver.FindElement(observerRole).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public MultipleUserGamePage RemoveUser()
        {
            driver.FindElement(removeUserButton).Click();
            var game = new MultipleUserGamePage(driver, wait);
            return game;
        }
        public RoomsPage GoToRoomsPage()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(account)).Click();
            driver.FindElement(accountRooms).Click();
            var room = new RoomsPage(driver, wait);
            return room;
        }
    }
}

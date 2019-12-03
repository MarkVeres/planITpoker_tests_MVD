using _01_planITpoker_clas_library_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using planITpokerTests_MVD.Pages;
using System;
using System.Linq;
using Xunit;

namespace planITpokerTests_MVD.Tests
{
    public class MultipleUsersTests : IDisposable
    {
        IWebDriver driver;
        IWebDriver driver2;
        public MultipleUsersTests()
        {
            this.driver = new FirefoxDriver();
        }
        [Fact]
        public void UserInvitedByInviteLink()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            string website = game.InviteLink;
            driver.Quit();
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            Assert.Equal("Test Room", uGame.RoomName);
        }
        [Fact]
        public void UserCanSeeOthersVotesOnlyAfterVoting()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(2);
            //asserts that after the second user has voted, he can see the first user's vote
            Assert.Equal("1", game.VoteValueOne);            
        }
        [Fact]
        public void NewUserJoinAndVoteAfterVotingStarts()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(2);
            Assert.Equal("2", game.VoteValueTwo);
        }        
        [Fact]
        public void ModeratorCanPressFinishVotingOnlyAfterAllUsersVoted()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(2);
            Assert.NotNull(game.FinishVotingButton);
        }
        [Fact]
        public void VoteLeaveSiteAndVoteAgain()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(1);
            driver2.Navigate().GoToUrl("https://www.google.com/");
            driver2.Navigate().GoToUrl(website);
            uGame.Vote(1);
            //Asserts that the second user (John) has voted once again after rejoining
            //since the moderator did not press "Finish Voting" before John left the website
            //being able to vote once again proves that the web app did not remember the initial vote input
            Assert.Equal("John voted.", uGame.ToastMessage);
        }
        [Fact]
        public void ModeratorCanRemovePlayer()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            game.ClickPlayerTwoAvatar();
            game.RemoveUser();
            Assert.True(game.PlayerList);
        }
        [Fact]
        public void DeAssignRoleOfModerator()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            game.ClickPlayerTwoAvatar();
            game.ClickModeratorRole();    //assign player two as moderator
            game.ClickPlayerOneAvatar();
            game.ClickModeratorRole();    //de-assign player one as moderator
            //first player is Jack who makes the room and is moderator (and is the first in the player name list)
            //Jack makes John the moderator and then de-assigns himself as moderator
            //now Jack is second in the player name list
            Assert.Equal("Jack", game.PlayerTwoName);
        }
        [Fact]
        public void ObserverSeesPlayersVotingInRealTime()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.ClickPlayerTwoAvatar();
            uGame.ClickObserverRole();
            Assert.NotEqual("00:00:00", uGame.Timer);
            Assert.Equal("1", game.VoteValueOne);
        }
        [Fact]
        public void DeleteGameRoomWithUserInside()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            var room = game.GoToRoomsPage();
            room.DeleteGameRoom();
            uGame.Vote(1);
            Assert.Equal("Waiting for moderator", uGame.ToastMessage);
        }
        [Fact]
        public void CanPlayerUserClearVotes()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(2);
            Assert.False(uGame.ClearVotesButton.Displayed);
        }
        [Fact]
        public void CanPlayerUserSkipStories()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            Assert.False(uGame.SkipStoryButton.Displayed);
        }
        [Fact]
        public void CanPlayerUserFinishVoting()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            uGame.Vote(2);
            Assert.False(uGame.FinishVotingButton.Displayed);
        }
        [Fact]
        public void CanPlayerUserResetTimer()
        {
            var home = new HomePage(driver);
            var game = home.MultipleUserQuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            string website = game.InviteLink;
            driver2 = new FirefoxDriver();
            var uHome = new QuickPlayPage(driver2, website);
            var uGame = uHome.JoinQuickPlay("John");
            Assert.False(uGame.ResetTimerButton.Displayed);
        }
        public void Dispose()
        {
            driver.Quit();
            driver2.Quit();
        }
    }
}

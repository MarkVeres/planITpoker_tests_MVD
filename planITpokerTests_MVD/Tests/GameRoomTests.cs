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
    public class GameRoomTests : IDisposable
    {
        IWebDriver driver;
        public GameRoomTests()
        {
            this.driver = new FirefoxDriver();
        }
        [Fact]
        public void CreatingStory()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.FinishVoting();
            //asserts that story exists in story list
            Assert.Equal("Test Story", game.StoryName);
        }
        [Fact]
        public void VotingInGameRoom()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            Assert.Equal("1", game.Votes);
        }
        [Fact]
        public void ReVotingAfterClearingVotes()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.ClearVotes();
            game.Vote(1);
            Assert.Equal("1", game.Votes);
        }
        [Fact]
        public void VoteBeforeGameStarts()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Vote(1);
            Assert.Equal("Click Start button to begin voting", game.ToastError);
        }
        [Fact]
        public void VotingTwiceOnSameStory()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.Vote(2);
            Assert.Equal("Vote cannot be changed once voting is completed", game.ToastError);
        }
        [Fact]
        public void ObserverCannotVote()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test story", "Test Story 2");
            game.ChangeRoleObserver();
            game.Start();
            game.Vote(1);
            Assert.Equal("Observers cannot vote", game.ToastError);
        }
        [Fact]
        public void SkipStory()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test story", "Test Story 2");
            game.Start();
            game.SkipStory();
            //asserts Next button that appears after skipping a story
            Assert.Equal("Next Story", game.NextStoryButton);
        }
        [Fact]
        public void TimeTheVoteProcess()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            Assert.Equal("00:00:00", game.Timer);
        }
        [Fact]
        public void UseCountdownTimer()
        {
            var home = new HomePage(driver);
            var quick = home.QuickPlay();
            var room = quick.QuickPlay("Jack");
            var game = room.AddCountdownTimer("Test Room");
            game.CreateStory("Test story", "Test story 2");
            game.Start();
            Assert.Equal("00:00:30", game.Timer);
        }
        [Fact]
        public void VotingAfterCountdownEnds()
        {
            var home = new HomePage(driver);
            var quick = home.QuickPlay();
            var room = quick.QuickPlay("Jack");
            var game = room.AddCountdownTimer("Test Room");
            game.CreateStory("Test story", "Test story 2");
            game.Start();
            game.Vote1AfterCountdownEnds();
            // wait for the 30 second countdown to end and then votes card 1
            Assert.Equal("1", game.Votes);
        }
        [Fact]
        public void SendingEstimates()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", " Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.SendEstimate(40);
            game.FinishVoting();
            game.ScrollDownSeeStoryDetails();
            Assert.Equal("40", game.FinalEstimate);
        }
        [Fact]
        public void DisallowStoryCreation()
        {
            var home = new HomePage(driver);
            var quick = home.QuickPlay();
            var room = quick.QuickPlay("Jack");
            var game = room.NoStories("Test Room");
            Assert.Equal("End tour", game.EndTour);
        }
        [Fact]
        public void EditCreatedStories()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.EditStories("Edited Test Story");
            Assert.Equal("Edited Test Story", game.StoryListName);
        }
        [Fact]
        public void SortingStories()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.SendEstimate(2);
            game.FinishVoting();
            game.NextStory();
            game.Vote(2);
            game.SendEstimate(40);
            game.FinishVoting();
            game.ScrollDownSeeStoryDetails();
            game.SortStoriesByFinalEstimate();
            Assert.Equal("40", game.FinalEstimateSortBy);
        }
        [Fact]
        public void DeleteCreatedStories()
        {
            //deletes the first of the 2 stories (deletes "Test Story")
            //only "Test Story 2" remains and thus it can be asserted
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.DeleteStory();
            Assert.Equal("Test Story 2", game.StoryListName2);
        }
        [Fact]
        public void ExportGameReport()
        {
            //Asserts the existence of the "Export Stories" button
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.GetReport();
            Assert.Equal("Export Stories", game.Report);
        }
        [Fact]
        public void AllowChangingVoteOption()
        {
            var home = new HomePage(driver);
            var quick = home.QuickPlay();
            var room = quick.QuickPlay("Jack");
            var game = room.AllowPlayersChangeVote("Test Room");
            game.CreateStory("Test story", "Test story 2");
            game.Start();
            game.Vote(2);
            game.Vote(1);
            Assert.Equal("1", game.Votes);
        }
        [Fact]
        public void RoomCreatorShouldBeModerator()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.ClickAvatar();
            Assert.NotNull(game.ModeratorRole);
        }
        public void Dispose()
        {
            driver.Quit();
        }
    }
}

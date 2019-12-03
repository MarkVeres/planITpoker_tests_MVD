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
    public class RoomCreationTests : IDisposable
    {
        IWebDriver driver;

        public RoomCreationTests()
        {
            this.driver = new FirefoxDriver();
        }
        [Fact]
        public void CreateRoom()
        {
            var home = new HomePage(driver);
            var gamePage = home.QuickPlayGame("Jack", "Test Room", "Test story", "Test Story 2");
            Assert.Equal("Players:", gamePage.Players);
        }

        [Fact]
        public void ResetGameRoom()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            game.Start();
            game.Vote(1);
            game.FinishVoting();
            var room = game.GoToRoomsPage();
            room.ResetGameRoom();
            Assert.Equal("Total: 00:00:00", room.TotalTime);
        }
        [Fact]
        public void DeleteGameRoom()
        {
            var home = new HomePage(driver);
            var game = home.QuickPlayGame("Jack", "Test Room", "Test Story", "Test Story 2");
            var room = game.GoToRoomsPage();
            room.DeleteGameRoom();
            Assert.Empty(room.RoomList);
        }
        public void Dispose()
        {
            driver.Quit();
        }
    }
}
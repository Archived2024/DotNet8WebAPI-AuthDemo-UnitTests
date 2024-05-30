using DotNet8WebAPI.Entity;
using DotNet8WebAPI.Model;
using DotNet8WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace xUnitTestProject
{
    public class OurHeroUnitTests
    {
        private readonly OurHeroDbContext _dbContext;
        private readonly OurHeroService _service;

        public OurHeroUnitTests()
        {
            var options = new DbContextOptionsBuilder<OurHeroDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())//Vi använder ett slumpat nytt Guid som id för annars får tester problem med att dbkontext har samma id.
                .Options;

            _dbContext = new OurHeroDbContext(options);
            _service = new OurHeroService(_dbContext);

            // Seed the in-memory database with initial data
            _dbContext.OurHeros.Add(new OurHero { Id = 1, FirstName = "Hero1", LastName = "Last1", isActive = true });
            _dbContext.OurHeros.Add(new OurHero { Id = 2, FirstName = "Hero2", LastName = "Last2", isActive = false });
            _dbContext.SaveChanges();
        }

        [Fact]
        public void GetAllHeros_ReturnsAllHeros()
        {
            // Arrange
            //Skapa något som ska testas (men behövs inte här, eftersom objekt skapas ovan

            // Act
            var result = _service.GetAllHeros(null).Result;

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetHerosByID_ReturnsHero_WhenHeroExists()
        {
            //Arrange

            // Act
            var result = _service.GetHerosByID(1).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetHerosByID_ReturnsNull_WhenHeroDoesNotExist()
        {


            // Act
            var result = _service.GetHerosByID(999).Result;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateOurHero_UpdatesExistingHero()
        {
            // Arrange
            var updateHero = new AddUpdateOurHero { FirstName = "UpdatedHero", LastName = "UpdatedLast", isActive = false };

            // Act
            var result = _service.UpdateOurHero(1, updateHero).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateHero.FirstName, result.FirstName);
        }

        [Fact]
        public void UpdateOurHero_ReturnsNull_WhenHeroDoesNotExist()
        {
            // Arrange
            var updateHero = new AddUpdateOurHero { FirstName = "UpdatedHero", LastName = "UpdatedLast", isActive = false };

            // Act
            var result = _service.UpdateOurHero(999, updateHero).Result;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteHerosByID_DeletesHero()
        {
            // Act
            var result = _service.DeleteHerosByID(1).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteHerosByID_ReturnsFalse_WhenHeroDoesNotExist()
        {
            // Act
            var result = _service.DeleteHerosByID(999).Result;

            // Assert
            Assert.False(result);
        }
    }
}

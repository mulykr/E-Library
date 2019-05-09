using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LiBook.Data;
using LiBook.Data.Entities;
using LiBook.Data.Interfaces;
using LiBook.Data.Repositories;
using LiBook.Services;
using LiBook.Services.DTO;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LiBook.Tests.Serviсes
{
    public class UserServiceTests
    {
        private readonly Mock<IAppConfiguration> _config;

        public UserServiceTests()
        {
            _config = new Mock<IAppConfiguration>();
            _config.Setup(c => c.WebRootPath).Returns(string.Empty);
        }
        [Fact]
        public void GetUserProfilesTest()
        {
            // Arrange
            var list = GetTestCollection();
            var svc = SetUpService();

            // Act
            IEnumerable<UserProfile> actual = svc.GetUserProfiles();

            // Assert
            Assert.Equal(actual.Count(), list.Count());
        }

        [Fact]
        public void GetUserProfileTest()
        {
            // Arrange
            var claims = new Mock<ClaimsPrincipal>();
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.IsAuthenticated)
                .Returns(true);
            claims.Setup(i => i.Identity)
                .Returns(identity.Object);
            claims.Setup(i => i.FindFirst(It.IsAny<string>()))
                .Returns(new Claim(ClaimTypes.NameIdentifier, "1"));
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<UserProfile>>();
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.GetEnumerator()).Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.UserProfiles).Returns(mockSet.Object);

            var repo = new Mock<IRepository<UserProfile>>();
            repo.Setup(r => r.Get(It.IsAny<string>()))
                .Returns(list.First(i => i.Id == "1"));
            var svc = new UserService(repo.Object);

            // Act
            var user = svc.GetUserProfile(claims.Object);

            // Assert
            Assert.Equal("Vova", user.FirstName);
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var expected = new UserProfile
            {
                FirstName = "Vova",
                LastName = "Vermii"
            };
            var repository = new Mock<IRepository<UserProfile>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new UserProfile
            {
                FirstName = "Vova",
                LastName = "Vermii"
            });
            var svc = new UserService(repository.Object);


            // Act
            svc.Update(expected);

            // Assert
            repository.Verify(r => r.Update(It.IsAny<UserProfile>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var expected = new UserProfile()
            {
                Id="1",
                FirstName = "Vova",
                LastName = "Vermii"
            };
            var repository = new Mock<IRepository<UserProfile>>();
            repository.Setup(r => r.Get(expected.Id)).Returns(new UserProfile
            {
                Id="1",
                FirstName = "Vova",
                LastName = "Vermii"
            });
            var svc = new UserService(repository.Object);

            // Act
            svc.Delete(expected.Id);

            // Assert
            repository.Verify(r => r.Delete(It.IsAny<string>()), Times.Once());
            repository.Verify(r => r.Save(), Times.Once());
        }

        private IEnumerable<UserProfile> GetTestCollection()
        {
            return new[]
            {
                new UserProfile
                {
                    Id="1",
                    FirstName="Vova",
                    LastName="Vermii"
                },
                new UserProfile
                {
                    Id="2",
                    FirstName="Tania",
                    LastName="Hutiy"
                },
                new UserProfile
                {
                    Id="3",
                    FirstName="Roman",
                    LastName="Mulyk"
                }
            };
        }

        private UserService SetUpService()
        {
            var list = GetTestCollection().AsQueryable();
            var mockSet = new Mock<DbSet<UserProfile>>();
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<UserProfile>>().Setup(p => p.GetEnumerator()).Returns(list.GetEnumerator);

            var mockCtx = new Mock<ApplicationDbContext>();
            mockCtx.Setup(p => p.UserProfiles).Returns(mockSet.Object);

            var repository = new UserRepository(mockCtx.Object);

            var svc = new UserService(repository);
            return svc;
        }
    }
}

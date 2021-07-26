using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SafeHouseAMS.DataLayer;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer
{
    public class DataLayerInjectorTests
    {
        [Fact, UnitTest]
        public void ConnectToDatabase_WhenServiceCollectionIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => DataLayerInjector.ConnectToDatabase(null!, Mock.Of<IConfiguration>()));
        
        [Fact, UnitTest]
        public void ConnectToDatabase_WhenConfigurationIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new ServiceCollection().ConnectToDatabase(null!));

        [Fact, UnitTest]
        public void ConnectToDatabase_Always_ReturnsOriginalServiceCollection()
        {
            //arrange
            var services = new ServiceCollection();
            
            //act
            var result = services.ConnectToDatabase(Mock.Of<IConfiguration>());
            
            //assert
            result.Should().BeSameAs(services);
        }
        
        [Fact, UnitTest]
        public void ConnectToDatabase_WhenCalled_RegistersAutomapper()
        {
            //arrange
            var services = new ServiceCollection();
            
            //act
            var result = services.ConnectToDatabase(Mock.Of<IConfiguration>());
            
            //assert
            result.Should().Contain(x => x.ServiceType == typeof(IMapper));
        }
        
        [Fact, UnitTest]
        public void ConnectToDatabase_WhenCalled_RegistersDataContext()
        {
            //arrange
            var services = new ServiceCollection();
            var connectionStringConfigSection = new Mock<IConfigurationSection>();
            connectionStringConfigSection.SetupGet(x => x["inMemory"]).Returns("mock connstring");
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetSection("ConnectionStrings")).Returns(connectionStringConfigSection.Object);
            
            //act
            var result = services.ConnectToDatabase(configMock.Object);
            
            //assert
            result.Should().Contain(x => x.ServiceType == typeof(DataContext));
            result.Should().Contain(x => x.ServiceType == typeof(IDatabaseMigrator));
        }
        
        [Fact, IntegrationTest]
        public void ConnectToDatabase_WhenCalled_CanResolveDataContext()
        {
            //arrange
            var services = new ServiceCollection();
            var connectionStringConfigSection = new Mock<IConfigurationSection>();
            connectionStringConfigSection.SetupGet(x => x["inMemory"]).Returns("mock connstring");
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetSection("ConnectionStrings")).Returns(connectionStringConfigSection.Object);
            
            //act
            var sp = services.ConnectToDatabase(configMock.Object).BuildServiceProvider();
            var datacontext = sp.GetService<DataContext>();
            var databaseMigrator = sp.GetService<IDatabaseMigrator>();

            //assert
            datacontext.Should().NotBeNull();
            databaseMigrator.Should().NotBeNull();
        }
    }
}
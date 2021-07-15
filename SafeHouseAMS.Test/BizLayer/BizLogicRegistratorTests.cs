using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.BizLayer.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer
{
    public class BizLogicRegistratorTests
    {
        [Fact, UnitTest]
        public void AddBizLogic_WhenServicesNull_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => BizLogicRegistrator.AddBizLogic(null!, Mock.Of<IConfiguration>()));
        
        [Fact, UnitTest]
        public void AddBizLogic_WhenConfigurationIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => Mock.Of<IServiceCollection>().AddBizLogic(null!));

        [Fact, UnitTest]
        public void AddBizLogic_Always_ReturnsSameCollection()
        {
            //arrange
            var services = new ServiceCollection();
            
            //act
            var result = services.AddBizLogic(Mock.Of<IConfiguration>());
            
            //assert
            result.Should().BeSameAs(services);
        }
        
        [Fact, UnitTest]
        public void AddBizLogic_Always_RegistersSurvivorsCatalogue()
        {
            //arrange
            var services = new ServiceCollection();
            
            //act
            var result = services.AddBizLogic(Mock.Of<IConfiguration>());
            
            //assert
            result.Should().Contain(x => x.ServiceType == typeof(ISurvivorCatalogue));
        }
    }
}
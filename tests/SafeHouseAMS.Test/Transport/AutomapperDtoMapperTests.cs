using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SafeHouseAMS.Transport;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.Transport
{
    public class AutomapperDtoMapperTests
    {
        [Fact, UnitTest]
        public void AddDtoMapping_WhenCalled_ReturnsSameCollection()
        {
            //arrange
            var src = new ServiceCollection();

            //act
            var res = AutomapperDtoMapper.AddDtoMapping(src);

            //assert
            res.Should().BeSameAs(src);
        }

        [Fact, IntegrationTest]
        public void AddDtoMapping_WhenCalled_RegistersAutomepper()
        {
            //arrange
            var src = new ServiceCollection();

            //act
            var res = AutomapperDtoMapper.AddDtoMapping(src);
            var sp = res.BuildServiceProvider();

            //assert
            var mapper = sp.GetService<IMapper>();
            mapper.Should().NotBeNull();
        }
    }
}

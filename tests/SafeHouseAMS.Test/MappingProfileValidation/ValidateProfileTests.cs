using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using SafeHouseAMS.Backend.Server;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.DataLayer;
using SafeHouseAMS.Transport;
using Xunit;

namespace SafeHouseAMS.Test.MappingProfileValidation;

public class ValidateProfileTests
{
    public static IEnumerable<object[]> GetAssemblies()
    {
        yield return new object[]{typeof(DataContext).Assembly};
        yield return new object[]{typeof(ICommand).Assembly};
        yield return new object[]{typeof(Startup).Assembly};
        yield return new object[]{typeof(AutomapperDtoMapper).Assembly};
    }

    [Theory, MemberData(nameof(GetAssemblies))]
    public void Map_IsValid(Assembly assembly)
    {
        //arrange
        var cfg = new MapperConfiguration(cfg => cfg.AddMaps(assembly));
        
        //act && assert
        cfg.AssertConfigurationIsValid();
    }
}

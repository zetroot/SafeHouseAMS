using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Регистратор бизнес-логики
    /// </summary>
    public static class BizLogicRegistrator
    {
        /// <summary>
        /// Добавить бизнес-логику в DI
        /// </summary>
        /// <param name="services">коллекция сервисов DI контейнера</param>
        /// <param name="configuration">конфигурация</param>
        /// <returns>та же коллекция сервисов для chaining</returns>
        public static IServiceCollection AddBizLogic(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.TryAddTransient<ISurvivorCatalogue, SurvivorCatalogue>();
            services.TryAddTransient<ILifeSituationDocumentsAggregate, LifeSituationDocumentsAggregate>();
            services.TryAddTransient<IEpisodesCatalogue, EpisodesCatalogue>();
            services.TryAddTransient<IGuidGenerator, GuidGenerator>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace SafeHouseAMS.Transport
{
    /// <summary>
    /// Статический класс регистратора маппинга DTO моделей
    /// </summary>
    public static class AutomapperDtoMapper
    {
        /// <summary>
        /// Зарегистрировать маппинг DTO моделей
        /// </summary>
        /// <param name="services">DI-контейнер в котором регистрируется автомаппер</param>
        /// <returns>Исходный контейнер для чейнинга</returns>
        public static IServiceCollection AddDtoMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AutomapperDtoMapper).Assembly));
            return services;
        }
    }
}
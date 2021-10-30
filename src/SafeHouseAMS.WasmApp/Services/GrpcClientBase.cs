using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;

namespace SafeHouseAMS.WasmApp.Services
{
    /// <summary>
    /// Базовый класс для всех grpc клиентов. Обеспечивает обработку исключения AccessTokenNotAvailableException
    /// </summary>
    internal abstract class GrpcClientBase
    {
        protected readonly ILogger Logger;

        public GrpcClientBase(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Обработчик запросов, возвращающих результат
        /// </summary>
        /// <param name="call">Функция запроса</param>
        /// <param name="fallback">Значение по умолчанию, которое возвращается, когда требуется редирект на страницу авторизации</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<T> CallHandler<T>(Func<Task<T>> call, T fallback)
        {
            try
            {
                return await call().ConfigureAwait(false);
            }
            catch (RpcException rpcException) when (rpcException.Status.DebugException is AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return fallback;
            }
            catch (RpcException rpcException)
            {
                Logger.LogError(rpcException, "RpcException caught");
                throw;
            }
            catch (Exception exc)
            {
                Logger.LogError(exc, "Generic exception caught");
                throw;
            }
        }

        /// <summary>
        /// Обработчик запросов не возвращающих результат
        /// </summary>
        /// <param name="call">делегат запроса</param>
        protected async Task CallHandler(Func<Task> call)
        {
            try
            {
                await call().ConfigureAwait(false);
            }
            catch (RpcException rpcException) when (rpcException.Status.DebugException is AccessTokenNotAvailableException e)
            {
                e.Redirect();
            }
            catch (RpcException rpcException)
            {
                Logger.LogError(rpcException, "RpcException caught");
                throw;
            }
            catch (Exception exc)
            {
                Logger.LogError(exc, "Generic exception caught");
                throw;
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class GrpcClientBase
    {
        protected ILogger _logger;

        public GrpcClientBase(ILogger logger)
        {
            _logger = logger;
        }

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
                _logger.LogError(rpcException, "RpcException caught");
                throw;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Generic exception caught");
                throw;
            }
        }

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
                _logger.LogError(rpcException, "RpcException caught");
                throw;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Generic exception caught");
                throw;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class SurvivorCatalogueClient : ISurvivorCatalogue
    {
        private readonly SurvivorCatalogue.SurvivorCatalogueClient client;
        private readonly IMapper _mapper;
        private readonly ILogger<SurvivorCatalogueClient> _logger;
        
        public SurvivorCatalogueClient(IMapper mapper, ILogger<SurvivorCatalogueClient> logger)
        {
            _mapper = mapper;
            _logger = logger;
            var baseUri = "https://localhost:4901/";
            
            //var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            
            var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpHandler = httpHandler });
            
            client = new SurvivorCatalogue.SurvivorCatalogueClient(channel);
        }
        
        public async Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var uuid = _mapper.Map<UUID>(id);
            var response = await client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<Survivor>(response);
        }
        
        public async Task ApplyCommand(SurvivorCommand command, CancellationToken cancellationToken)
        {
            var request = command switch
            {
                CreateSurvivor create => new CommandWrapper {CreateCommand = _mapper.Map<Transport.Protos.Models.Survivors.CreateSurvivor>(create)},
                _ => throw new InvalidOperationException()
            };
            await client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
        }
        
        public async IAsyncEnumerable<Survivor> GetCollection(int skip, int? take, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var streamingCall = client.GetCollection(new() {Skip = skip, Take = take}, new CallOptions(cancellationToken: cancellationToken));
            while (await streamingCall.ResponseStream.MoveNext())
            {
                var chunk = streamingCall.ResponseStream.Current;
                yield return _mapper.Map<Survivor>(chunk);
            }
        }
        
        public async Task<int> GetTotalCount()
        {
            var result = await client.GetTotalCountAsync(new());
            return result.Count;
        }
    }
}
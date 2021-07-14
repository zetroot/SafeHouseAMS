﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Models;

namespace SafeHouseAMS.BizLayer.Services
{
    /// <summary>
    /// Интерфейс каталога пострадавших
    /// </summary>
    public interface ISurvivorCatalogue
    {
        IAsyncEnumerable<Survivor> GetCollection();
        Task<Survivor> Add(Survivor adding);
        Task<Survivor> Update(Survivor updating);
        Task Delete(Guid id);
        Task<Survivor> GetSingleAsync(Guid id);
    }
}
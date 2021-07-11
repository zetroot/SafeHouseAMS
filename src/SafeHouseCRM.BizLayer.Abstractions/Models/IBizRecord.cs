﻿using System;

namespace SafeHouseCRM.BizLayer.Abstractions.Models
{
    public interface IBizRecord
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Признак удалённой записи
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
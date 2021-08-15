using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Пара запись-дата документа
    /// </summary>
    /// <param name="Record">Запись</param>
    /// <param name="DocumentDate">Дата документа к которой относится запись</param>
    /// <typeparam name="T">Тип записи</typeparam>
    public record RecordHistoryItem<T>(T Record, DateTime DocumentDate) where T : BaseRecord;
}
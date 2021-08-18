using System;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Элемент истории изменения записи
    /// </summary>
    public record RecordHistoryItem(DateTime DocumentDate, Guid DocumentID);
}

using System;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Интерфейс команды
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Идентификатор записи для которой выполняется команда
        /// </summary>
        Guid EntityID { get; }
    }
}
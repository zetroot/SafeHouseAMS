using System;

namespace SafeHouseAMS.BizLayer.Survivor.Commands
{
    /// <summary>
    /// Базовый класс команды агрегата реестра постардавших
    /// </summary>
    public abstract class SurvivorCommand : BaseCommand<ISurvivorRepository>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор пострадавшего для которого выполняется команда</param>
        protected SurvivorCommand(Guid entityID) : base(entityID)
        {
        }
    }
}
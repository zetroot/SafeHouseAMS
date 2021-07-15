using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// базовый класс коменды
    /// </summary>
    public abstract class BaseCommand<TRepository> : ICommand
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор пострадавшего для которого выполняется команда</param>
        protected BaseCommand(Guid entityID)
        {
            EntityID = entityID;
        }

        /// <inheritdoc />
        public Guid EntityID { get; }

        /// <summary>
        /// Применить комадну на репозитории 
        /// </summary>
        /// <param name="repository">Объект репозитория на который будет применяться команда</param>
        /// <returns>Задача, которая будет завершена, когда команда применится</returns>
        internal abstract Task ApplyOn(TRepository repository);
    }
}
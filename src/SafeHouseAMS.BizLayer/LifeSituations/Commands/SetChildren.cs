using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда добавления записей о детях в документ
    /// </summary> 
    public class SetChildren : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Есть ли дети
        /// </summary>
        public bool HasChildren { get; }

        /// <summary>
        /// Детальная информация о детях
        /// </summary>
        public string? Details { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа, на котором выполняется команда</param>
        /// <param name="hasChildren">Есть ли дети</param>
        /// <param name="details">подробности о имеющихся детях</param>
        public SetChildren(Guid entityID, bool hasChildren, string? details) : base(entityID)
        {
            HasChildren = hasChildren;
            Details = details;
        }
        
        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            var creatingRecord = new ChildrenRecord(Guid.NewGuid(), HasChildren, Details);
            return repository.AddRecord(EntityID, creatingRecord);
        }
    }
}
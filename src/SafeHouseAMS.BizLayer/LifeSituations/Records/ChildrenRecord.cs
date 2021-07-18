using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// наличие детей
    /// </summary>
    public class ChildrenRecord : BaseRecord
    {
        /// <summary>
        /// Есть ли дети
        /// </summary>
        public bool HasChildren { get; }

        /// <summary>
        /// Описание - пол, возраст, количество
        /// </summary>
        public string? Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="hasChildren">есть ли дети вообще</param>
        /// <param name="details">подробнее о детях</param>
        public ChildrenRecord(Guid id, bool hasChildren, string? details) : base(id)
        {
            HasChildren = hasChildren;
            Details = details;
        }
    }
}
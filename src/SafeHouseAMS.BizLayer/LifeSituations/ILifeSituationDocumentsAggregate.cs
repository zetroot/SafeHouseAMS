using SafeHouseAMS.BizLayer.LifeSituations.Commands;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Агрегат документов жизненных ситуаций 
    /// </summary>
    public interface ILifeSituationDocumentsAggregate : IDomainAggregate<LifeSituationDocument, LifeSituationDocumentCommand>
    {
        
    }
}
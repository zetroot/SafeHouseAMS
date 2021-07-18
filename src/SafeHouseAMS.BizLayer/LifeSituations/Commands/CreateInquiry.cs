using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Создать новы документ обращения
    /// </summary>
    public class CreateInquiry : LifeSituationDocumentCommand
    {
        /// <summary>
        /// пострадавший
        /// </summary>
        public Guid SurvivorID { get; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTimeOffset DocumentDate { get; }

        /// <summary>
        /// несовершеннолетний на момент обращения
        /// </summary>
        public bool IsJuvenile { get; }

        /// <summary>
        /// источники обращения
        /// </summary>
        public IEnumerable<IInquirySource> InquirySources { get; }

        /// <summary>
        /// гражданство
        /// </summary>
        public string Citizenship { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификтаор документа</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivorId">пострадавший</param>
        /// <param name="isJuvenile">несовершеннолетний на момент обращения</param>
        /// <param name="inquirySources">источники обращения</param>
        /// <param name="citizenship">гражданство</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CreateInquiry(Guid entityID, DateTimeOffset documentDate, Guid survivorId, bool? isJuvenile, IEnumerable<IInquirySource> inquirySources, string citizenship) :
            base(entityID)
        {
            SurvivorID = survivorId;
            DocumentDate = documentDate;
            
            InquirySources = inquirySources ?? throw new ArgumentNullException(nameof(inquirySources));
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
            IsJuvenile = isJuvenile ?? false;
        }
        
        
        internal override async Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            var now = DateTimeOffset.Now;
            await repository.CreateInquiry(EntityID, false, now, now,
            SurvivorID, DocumentDate, 
            IsJuvenile, InquirySources);

            await repository.AddRecord(EntityID, new CitizenshipRecord(Guid.NewGuid(), Citizenship));

        }
    }
}
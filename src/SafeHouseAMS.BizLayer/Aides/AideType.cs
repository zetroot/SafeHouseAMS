using System.ComponentModel;

namespace SafeHouseAMS.BizLayer.Aides;

/// <summary>
/// тип помощника
/// </summary>
public enum AideType
{
    /// <summary>
    /// значение по умолчанию
    /// </summary>
    None = 0,
    
    /// <summary>
    /// соцработник
    /// </summary>
    [Description("Соц.работник")] SocialWorker,
    
    /// <summary>
    /// Юрист
    /// </summary>
    [Description("Юрист")] Lawyer,
    
    /// <summary>
    /// психолог
    /// </summary>
    [Description("Психолог")] Psychologist
}
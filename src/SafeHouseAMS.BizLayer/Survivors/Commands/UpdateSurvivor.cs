using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivors.Commands;

/// <summary>
/// Команда обновления записи о пострадавшем. Перезаписывает поля, изменяет дату последнего изменения
/// </summary>
public class UpdateSurvivor : SurvivorCommand
{
    /// <summary>
    /// имя пострадавшего
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// номер карточки
    /// </summary>
    public int Num { get; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public SexEnum Sex { get; }
    
    /// <summary>
    /// Уточнение пола
    /// </summary>
    public string? OtherSex { get; }
    
    /// <summary>
    /// Дата рождения, если известна точная
    /// </summary>
    public DateTime? BirthDate { get; }
    
    
    /// <summary>
    /// Возраст, если неизвестна дата рождения
    /// </summary>
    public int? Age { get; }
    
    /// <summary>
    /// ctor
    /// </summary>
    public UpdateSurvivor(Guid entityID, string name, int num, SexEnum sex, string? otherSex, DateTime? birthDate, int? age) : base(entityID)
    {
        Name = name;
        Num = num;
        Sex = sex;
        OtherSex = otherSex;
        BirthDate = birthDate;
        Age = age;
    }

    internal override Task ApplyOn(ISurvivorRepository repository)
    {
        DateTime? calcDob = null;
        if (Age is not null) calcDob = DateTime.Today.AddYears(Age.Value * -1).AddDays(-180);

        return repository.Update(EntityID, DateTime.Now, Name, Num, Sex, OtherSex, BirthDate, calcDob);
    }
}

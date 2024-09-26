namespace Postech.TechChallenge.Persistency.Core.Entities;

public class AreaCodeEntity(string areaCodeValue)
{
    public int AreaCodeId { get; private set; }
    public string AreaCodeValue { get; private set; } = areaCodeValue;
}

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;

public class RegionEntity
{
    public required int RegionId { get; set; } 
    public required string RegionName { get; set; }
    public required string RegionStateName { get; set; }
    public required ICollection<AreaCodeEntity> AreaCodes { get; set; } 
}

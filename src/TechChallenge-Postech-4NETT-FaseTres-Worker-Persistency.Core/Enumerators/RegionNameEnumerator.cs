using System.ComponentModel;

namespace Postech.TechChallenge.Persistency.Core.Enumerators
{
    public enum RegionNameEnumerator
    {
        [Description("Centro-Oeste")]
        Midwest,
        [Description("Nordeste")]
        Northeast,
        [Description("Norte")]
        North,
        [Description("Sudeste")]
        Southeast,
        [Description("Sul")]
        South,
    }
}
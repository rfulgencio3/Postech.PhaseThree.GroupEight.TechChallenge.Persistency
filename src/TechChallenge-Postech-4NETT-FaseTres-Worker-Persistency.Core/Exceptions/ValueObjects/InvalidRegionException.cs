using System.Diagnostics.CodeAnalysis;
using Postech.TechChallenge.Persistency.Core.Enumerators;
using Postech.TechChallenge.Persistency.Core.Exceptions.Common;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class InvalidRegionException(string message, RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName) : DomainException(message)
    {
        public RegionNameEnumerator RegionName { get; } = regionName;
        public RegionStateNameEnumerator RegionStateName { get; } = regionStateName;
    }
}
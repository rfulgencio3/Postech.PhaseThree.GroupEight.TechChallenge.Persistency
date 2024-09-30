using System.Diagnostics.CodeAnalysis;
using Postech.TechChallenge.Persistency.Core.Exceptions.Common;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class AreaCodeValueNotSupportedException(string message, string areaCodeValue) : DomainException(message)
    {
        public string AreaCodeValue { get; } = areaCodeValue;
    }
}
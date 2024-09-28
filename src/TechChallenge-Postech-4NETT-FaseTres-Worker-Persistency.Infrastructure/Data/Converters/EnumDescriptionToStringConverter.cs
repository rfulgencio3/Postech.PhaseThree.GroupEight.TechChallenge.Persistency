using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postech.TechChallenge.Persistency.Core.Extensions;
using Postech.TechChallenge.Persistency.Core.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Postech.TechChallenge.Persistency.Infra.Data.Converters
{
    [ExcludeFromCodeCoverage]
    internal class EnumDescriptionToStringConverter<TEnum>(ConverterMappingHints? mappingHints = null) : ValueConverter<TEnum, string>(
          enumValue => enumValue.GetDescription(),
          stringValue => EnumFromDescriptionHelper<TEnum>.GetValue(stringValue),
          mappingHints)
        where TEnum : Enum
    {
    }
}
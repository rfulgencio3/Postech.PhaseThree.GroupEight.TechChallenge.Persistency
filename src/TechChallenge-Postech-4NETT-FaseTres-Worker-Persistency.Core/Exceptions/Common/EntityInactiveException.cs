using System.Diagnostics.CodeAnalysis;
using Postech.TechChallenge.Persistency.Core.Entities;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class EntityInactiveException(string message) : DomainException(message)
    {
        public static void ThrowWhenIsInactive(EntityBase entity, string errorMessage)
        {
            if (!entity.IsActive())
            {
                throw new EntityInactiveException(errorMessage);
            }
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Postech.TechChallenge.Persistency.Core.Exceptions.Common;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactFirstNameException(string message, string firstNameValue) : DomainException(message)
    {
        public string FirstNameValue { get; } = firstNameValue;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactFirstNameException("The contact's first name must contain only letters (lowercase or uppercase), accents, hyphens and must not exceed forty characters.", argument);
            }
        }
    }
}
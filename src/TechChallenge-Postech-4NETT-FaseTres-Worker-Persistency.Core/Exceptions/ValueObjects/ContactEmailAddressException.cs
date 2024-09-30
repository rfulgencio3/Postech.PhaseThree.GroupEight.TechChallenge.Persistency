using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Postech.TechChallenge.Persistency.Core.Exceptions.Common;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactEmailAddressException(string message, string emailAddressValue) : DomainException(message)
    {
        public string EmailAddressValue { get; } = emailAddressValue;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactEmailAddressException("The email address is not in a valid format.", argument);
            }
        }
    }
}
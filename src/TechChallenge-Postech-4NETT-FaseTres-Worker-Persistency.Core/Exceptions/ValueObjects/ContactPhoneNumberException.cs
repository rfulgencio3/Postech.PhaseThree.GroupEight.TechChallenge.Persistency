using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Postech.TechChallenge.Persistency.Core.Exceptions.Common;

namespace Postech.TechChallenge.Persistency.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactPhoneNumberException(string message, string phoneNumber) : DomainException(message)
    {
        public string PhoneNumber { get; } = phoneNumber;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactPhoneNumberException("The phone number must only have eight or nine digits, and must not contain special characters, such as hyphens.", argument);
            }
        }
    }
}
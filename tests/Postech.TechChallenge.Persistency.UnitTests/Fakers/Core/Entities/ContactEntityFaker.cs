using Bogus;
using Postech.TechChallenge.Persistency.Core.Entities;
using Postech.TechChallenge.Persistency.Core.ValueObjects;

namespace Postech.TechChallenge.Persistency.UnitTests.Fakers.Core.Entities
{
    internal class ContactEntityFaker : Faker<ContactEntity>
    {
        public ContactEntityFaker(string areaCode = "11")
        {
            Locale = "pt_BR";
            CustomInstantiator(c => new ContactEntity(
                new(c.Name.FirstName(), c.Name.LastName()), 
                new(c.Internet.Email()), 
                new(c.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create(areaCode)))
            );
        }
    }
}
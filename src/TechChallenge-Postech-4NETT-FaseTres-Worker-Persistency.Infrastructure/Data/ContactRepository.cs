﻿using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Contexts;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Entities;
using Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Data;

public class ContactRepository : IContactRepository
{
    private readonly ContactManagementDbContext _context;

    public ContactRepository(ContactManagementDbContext context)
    {
        _context = context;
    }

    public async Task CreateContactAsync(ContactEntity contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateContactAsync(ContactEntity contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactAsync(Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            contact.Active = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ContactEntity> GetContactByIdAsync(Guid id)
    {
        var contact = await _context.Contacts
            .Where(c => c.ContactId == id && c.Active)
            .SingleOrDefaultAsync();

        if (contact != null)
        {
            return contact;
        }
        return null!;
    }
}

﻿using Worker.Persistency.Core.Entities;
using Worker.Persistency.Core.Interfaces;
using Worker.Persistency.Infrastructure.Contexts;

namespace Worker.Persistency.Infrastructure.Data;

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

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            contact.Active = false;
            await _context.SaveChangesAsync();
        }
    }
}

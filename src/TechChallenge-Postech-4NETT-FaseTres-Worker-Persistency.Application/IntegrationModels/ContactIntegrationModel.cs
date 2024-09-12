﻿namespace Worker.Persistency.Application.IntegrationModels;

public class ContactIntegrationModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool Active { get; set; }
    public string OperationType { get; set; }
}

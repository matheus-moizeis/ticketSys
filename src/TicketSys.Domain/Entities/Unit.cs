namespace TicketSys.Domain.Entities;

using TicketSys.Domain.Validation;

public sealed class Unit
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public bool Active { get; set; }

    public Unit(string name, string address, bool active)
    {
        ValidateDomain(name, address);
        Name = name;
        Address = address;
        Active = active;
    }

    public Unit(int id, string name, string address, bool active)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value.");
        ValidateDomain(name, address);

        Id = id;
        Name = name;
        Address = address;
        Active = active;
    }

    public void Update(string name, string address, bool active)
    {
        ValidateDomain(name, address);
        Name = name;
        Address = address;
        Active = active;
    }

    private void ValidateDomain(string name, string address)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is required.");
        DomainExceptionValidation.When(name.Length < 3, "Name must be at least 3 characters long.");

        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(address), "Address is required.");
        DomainExceptionValidation.When(address.Length < 5, "Address must be at least 5 characters long.");
    }
}
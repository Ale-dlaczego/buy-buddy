namespace api.Entities;

public class Unit
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsArchived { get; set; }
}
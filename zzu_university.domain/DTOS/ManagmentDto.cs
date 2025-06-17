using System.ComponentModel.DataAnnotations.Schema;

public class ManagmentDto
{
  
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }

    // ID of type
    public int Type { get; set; }

    // Name of the type (from navigation property)
    public string? TypeName { get; set; }

    public string ImageUrl { get; set; }
}
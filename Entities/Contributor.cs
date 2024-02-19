using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class Contributor
{
    public int Id { get; set; }

    [StringLength(450, MinimumLength = 1)]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public int WikiId { get; set; }
    public Wiki Wiki { get; set; } = null!;
    
    public int ContributorRoleId { get; set; }
    public ContributorRole ContributorRole { get; set; } = null!;
}
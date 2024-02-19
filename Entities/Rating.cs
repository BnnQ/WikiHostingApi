using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class Rating
{
    public int Id { get; set; }

    [StringLength(450, MinimumLength = 1)]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public int PageId { get; set; }
    public Page Page { get; set; } = null!;
    
    public DateTime PostedAt { get; set; }
    
    // for subscriptions case, if for example premium user can place more than one like
    public int NumberOfLikes { get; set; }
    public int NumberOfDislikes { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class UserSubscription
{
    [StringLength(450, MinimumLength = 1)]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
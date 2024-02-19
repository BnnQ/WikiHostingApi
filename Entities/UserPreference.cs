using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class UserPreference
{
    [StringLength(450, MinimumLength = 1)]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public int SiteThemeId { get; set; }
    
    // Email preferences
    public bool PreferToReceiveUpdateEmails { get; set; }
    public bool PreferToReceivePromotionalEmails { get; set; }
    public bool PreferToReceiveSurveyEmails { get; set; }

    // Notification preferences
    public bool PreferToReceiveEventNotifications { get; set; }
    public bool PreferToReceiveMessageNotifications { get; set; }
}
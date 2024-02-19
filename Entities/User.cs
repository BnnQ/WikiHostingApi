using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class User : IdentityUser
{
    [StringLength(512, MinimumLength = 1)]
    public string AvatarPath { get; set; } = null!;
    
    public UserSubscription Subscription { get; set; } = null!;
    public UserPreference Preference { get; set; } = null!;

    public IList<Topic> InterestedTopics { get; set; } = new List<Topic>();
    public IList<Contributor> Contributions { get; set; } = new List<Contributor>();
    public IList<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    
    public IList<Rating> CreatedRatings { get; set; } = new List<Rating>();
    public IList<Report> CreatedReports { get; set; } = new List<Report>();
    
    public IList<Page> CreatedPages { get; set; } = new List<Page>();
    public IList<Theme> CreatedThemes { get; set; } = new List<Theme>();
    public IList<Template> CreatedTemplates { get; set; } = new List<Template>();
}
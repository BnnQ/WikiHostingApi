﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiHostingApi.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")] // Can be solved by correct JSON serialization configuration
public class Template
{
    public int Id { get; set; }
 
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = null!;

    [StringLength(450, MinimumLength = 1)]
    public string AuthorId { get; set; } = null!;
    public User Author { get; set; } = null!;

    [StringLength(int.MaxValue, MinimumLength = 1)]
    public string Html { get; set; } = null!;
    [StringLength(int.MaxValue)]
    public string? Css { get; set; }
    [StringLength(int.MaxValue)]
    public string? Js { get; set; }
    [StringLength(int.MaxValue)]
    public string? Variables { get; set; }
    
    public bool IsPublic { get; set; }
}
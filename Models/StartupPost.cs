using System;
using System.ComponentModel.DataAnnotations;

namespace StartupPitchingPlatform.Models
{
public class StartupPost
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime PostedOn { get; set; } = DateTime.Now;

   
    public string? AuthorId { get; set; }

    public string ImageUrl { get; set; }

    // Add Category
    [Required]
    public string Category { get; set; }

    // Add Social Links
    [Url(ErrorMessage = "Please enter a valid GitHub URL.")]
    public string? GitHubUrl { get; set; }

    [Url(ErrorMessage = "Please enter a valid LinkedIn URL.")]
    public string? LinkedInUrl { get; set; }
}
}

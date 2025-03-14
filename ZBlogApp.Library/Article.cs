using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZBlogApp.Library;

public class Article
{
    [Key]
    [Required]
    public int ArticleId { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    [DataType(DataType.Html)]
    public string? Body { get; set; }
    [DisplayName("Creation Date")]
    public DateTime CreateDate { get; set; } = DateTime.Now;
    [DisplayName("Start Date")]
    public DateTime StartDate { get; set; }
    [DisplayName("End Date")]
    public DateTime EndDate { get; set; }
    [DisplayName("Author")]
    public string? ContributorUserName { get; set; }
}

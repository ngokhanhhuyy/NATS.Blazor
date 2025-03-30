using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NATS.Services.Enums;
using NATS.Services.Interfaces;

namespace NATS.Services.Entities;

public class AboutUsIntroduction : IHasThumbnailEntity
{
    [Column("id")]
    [Key]
    public int Id { get; set; }
    
    [Column("thumbnail_url")]
    [StringLength(1000)]
    public string ThumbnailUrl { get; set; }

    [Column("thumbnail_type")]
    public ThumbnailType ThumbnailType { get; set; } = ThumbnailType.Photo;

    [Column("main_quote_content")]
    [Required]
    [StringLength(1000)]
    public string MainQuoteContent { get; set; }

    [Column("about_us_content")]
    [Required]
    [StringLength(1500)]
    public string AboutUsContent { get; set; }

    [Column("why_choose_us_content")]
    [Required]
    [StringLength(1500)]
    public string WhyChooseUsContent { get; set; }

    [Column("our_difference_content")]
    [Required]
    [StringLength(1500)]
    public string OurDifferenceContent { get; set; }

    [Column("our_culture_content")]
    [Required]
    [StringLength(1500)]
    public string OurCultureContent { get; set; }
}
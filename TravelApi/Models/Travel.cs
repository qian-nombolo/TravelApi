using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Travel
  {
    public int TravelId { get; set; }
    [Required]
    [StringLength(20)]
    public string Destination { get; set; }
    
    [Required]
    [StringLength(20)]    
    public string City { get; set; }

    [Required]
    [StringLength(20)]    
    public string Country { get; set; }

    [Required]
    [StringLength(25)]    
    public string Review { get; set; }

    // [Required]
    // public string ReviewAuthor { get; set; }
    
    [Required]
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
    public int Rating { get; set; }

    // [Required]
    public DateTime Date { get; set; }
  }
}
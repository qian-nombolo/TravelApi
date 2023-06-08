namespace TravelApi.Models
{
  public class TravelResponse
  {
    public List<Travel> Travels { get; set; }
    public int PageItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }
 
}
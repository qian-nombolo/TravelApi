using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;

namespace TravelsApi.Controllers.V2
{
  
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("2.0")]
  
  public class TravelsController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public TravelsController(TravelApiContext db)
    {
      _db = db;
    }
    
    // GET: api/v2/travels
    [HttpGet]
    public async Task<IActionResult>  Get(string country, string city, int filterRating, int? page)
    {
      IQueryable<Travel> query = _db.Travels.AsQueryable();

      if (country != null)
      {
        query = query.Where(e => e.Country == country);
      }

      if (city != null)
      {
        query = query.Where(e => e.City == city);
      }

      if (filterRating > 0)
      {
        query = query.Where(entry => entry.Rating >= filterRating);
      }

      int pageCount = query.Count();
      int pageSize = 2;
      int currentPage = page ?? 1;

      var travels = await query
        .Skip((currentPage - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      var response = new TravelResponse
      {
        Travels = travels,
        //page number inside the url
        CurrentPage = currentPage,
        //the amount of travels returned from the database
        PageItems  = pageCount,
        //amount of items on the page
        PageSize = pageSize         
      };

      return Ok(response);      
      // return await query.ToListAsync();
      
    }

    // GET: api/v2/travels/4
    [HttpGet("{id}")]
    public async Task<ActionResult<Travel>> GetTravel(int id)
    {
      Travel travel = await _db.Travels.FindAsync(id);

      if (travel == null)
      {
        return NotFound();
      }

      return travel;
    }
    
    // POST: api/v2/travels/4
    [HttpPost]
    public async Task<ActionResult<Travel>> Post([FromBody] Travel travel)
    {
      _db.Travels.Add(travel);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetTravel), new { id = travel.TravelId }, travel);
    }

    // [Authorize]
    // PUT: api/v2/travels/6
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Travel travel)
    {
      if (id != travel.TravelId)
      {
        return BadRequest();
      }

      _db.Travels.Update(travel);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TravelExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool TravelExists(int id)
    {
      return _db.Travels.Any(e => e.TravelId == id);
    }
    
    // [Authorize]
    // DELETE: api/v2/travels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTravel(int id)
    {
      Travel travel = await _db.Travels.FindAsync(id);
      if (travel == null)
      {
        return NotFound();
      }

      _db.Travels.Remove(travel);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    // GET: api/v2/travels/random
    [HttpGet("random")]
    public async Task<ActionResult<Travel>> GetRandomTravel()
    {
      List<Travel> travels = await _db.Travels.ToListAsync();
      int random = new Random().Next(travels.Count);
      return travels[random];
    }

    
  }
}


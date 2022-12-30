using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using Radiostation.WebAPI.Models;

namespace Radiostation.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TrackController : ControllerBase
{
    private readonly IBaseRepository<Track> _trackRepository;
    private readonly IBaseRepository<Genre> _genreRepository;
    private readonly IBaseRepository<Performer> _performerRepository;

    public TrackController(
        IBaseRepository<Track> trackRepository,
        IBaseRepository<Genre> genreRepository,
        IBaseRepository<Performer> performerRepository)
    {
        _trackRepository = trackRepository;
        _genreRepository = genreRepository;
        _performerRepository = performerRepository;
    }

    [HttpGet("Genres")]
    public async Task<IActionResult> GetAllGenresAsync()
    {
        var data = await _genreRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.Name
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("Performers")]
    public async Task<IActionResult> GetAllPerformersAsync()
    {
        var data = await _performerRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.Name
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _trackRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Duration,
                Genre = x.Genre.Name,
                x.Rating,
                Performer = x.Performer.Name,
                x.PerformerId,
                x.GenreId
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _trackRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateTrackBody body)
    {
        var entityToUpdate = new Track
        {
            Id = body.Id,
            GenreId = body.GenreId,
            Duration = body.Duration,
            Name = body.Name,
            PerformerId = body.PerformerId,
            Rating = body.Rating
        };

        await _trackRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateTrackBody body)
    {
        var entityToCreate = new Track
        {
            GenreId = body.GenreId,
            Duration = body.Duration,
            Name = body.Name,
            PerformerId = body.PerformerId,
            Rating = body.Rating
        };
        await _trackRepository.Create(entityToCreate);

        return NoContent();
    }
}
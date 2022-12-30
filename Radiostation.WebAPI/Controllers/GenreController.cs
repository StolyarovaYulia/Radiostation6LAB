using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using Radiostation.WebAPI.Models;

namespace Radiostation.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly IBaseRepository<Genre> _genreRepository;

    public GenreController(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _genreRepository.GetEntities()
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _genreRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateGenreBody body)
    {
        var entityToUpdate = new Genre
        {
            Id = body.Id,
            Name = body.Name,
            Description = body.Description
        };

        await _genreRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateGenreBody body)
    {
        var entityToCreate = new Genre
        {
            Description = body.Description,
            Name = body.Name,
        };
        await _genreRepository.Create(entityToCreate);

        return NoContent();
    }
}
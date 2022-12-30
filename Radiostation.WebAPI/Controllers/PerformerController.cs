using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using Radiostation.WebAPI.Models;

namespace Radiostation.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PerformerController : ControllerBase
{
    private readonly IBaseRepository<Performer> _performerRepository;

    public PerformerController(IBaseRepository<Performer> performerRepository)
    {
        _performerRepository = performerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _performerRepository.GetEntities()
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
        await _performerRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateGenreBody body)
    {
        var entityToUpdate = new Performer
        {
            Id = body.Id,
            Name = body.Name,
            Description = body.Description,
            GroupList = ""
        };

        await _performerRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateGenreBody body)
    {
        var entityToCreate = new Performer
        {
            Description = body.Description,
            Name = body.Name,
            GroupList = ""
        };
        await _performerRepository.Create(entityToCreate);

        return NoContent();
    }
}
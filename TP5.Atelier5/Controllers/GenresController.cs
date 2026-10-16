using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TP5.Api.Dtos;
using TP5.Api.Models;
using TP5.Api.Services;

namespace TP5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(IGenresService genresService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
    {
        var genres = await genresService.GetAll();
        return Ok(mapper.Map<IEnumerable<GenreDto>>(genres));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDto>> GetById(byte id)
    {
        var genre = await genresService.GetById(id);
        return genre is null ? NotFound() : Ok(mapper.Map<GenreDto>(genre));
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> Add(GenreDto genreDto)
    {
        var genre = await genresService.Add(mapper.Map<Genre>(genreDto));
        return CreatedAtAction(nameof(GetById), new { id = genre.Id }, mapper.Map<GenreDto>(genre));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GenreDto>> Update(byte id, GenreDto genreDto)
    {
        var genre = mapper.Map<Genre>(genreDto);
        genre.Id = id;
        var updated = await genresService.Update(genre);
        return updated is null ? NotFound() : Ok(mapper.Map<GenreDto>(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<GenreDto>> Delete(byte id)
    {
        var genre = await genresService.GetById(id);
        if (genre is null) return NotFound();

        try
        {
            var deleted = await genresService.Delete(genre);
            return Ok(mapper.Map<GenreDto>(deleted));
        }
        catch (InvalidOperationException)
        {
            return Conflict("Cannot delete a genre used by a movie");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TP5.Api.Dtos;
using TP5.Api.Models;
using TP5.Api.Services;

namespace TP5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper) : ControllerBase
{
    private const long MaxPosterSize = 1024 * 1024;
    private static readonly string[] AllowedExtensions = [".png", ".jpg"];

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDetailsDto>>> GetAll([FromQuery] byte genreId = 0)
    {
        var movies = await moviesService.GetAll(genreId);
        return Ok(mapper.Map<IEnumerable<MovieDetailsDto>>(movies));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDetailsDto>> GetById(int id)
    {
        var movie = await moviesService.GetById(id);
        return movie is null ? NotFound() : Ok(mapper.Map<MovieDetailsDto>(movie));
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<MovieDetailsDto>> Add([FromForm] MovieDto movieDto)
    {
        var validation = ValidatePoster(movieDto.Poster);
        if (validation is not null) return BadRequest(validation);
        if (!await genresService.IsValidGenre(movieDto.GenreId))
            return BadRequest("The selected genre does not exist");

        var movie = mapper.Map<Movie>(movieDto);
        movie.Poster = await ReadPoster(movieDto.Poster!);
        var created = await moviesService.Add(movie);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, mapper.Map<MovieDetailsDto>(created));
    }

    [HttpPut("{id:int}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<MovieDetailsDto>> Update(int id, [FromForm] MovieDto movieDto)
    {
        var validation = ValidatePoster(movieDto.Poster);
        if (validation is not null) return BadRequest(validation);
        if (!await genresService.IsValidGenre(movieDto.GenreId))
            return BadRequest("The selected genre does not exist");

        var movie = mapper.Map<Movie>(movieDto);
        movie.Id = id;
        movie.Poster = await ReadPoster(movieDto.Poster!);
        var updated = await moviesService.Update(movie);
        return updated is null ? NotFound() : Ok(mapper.Map<MovieDetailsDto>(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<MovieDetailsDto>> Delete(int id)
    {
        var movie = await moviesService.GetById(id);
        if (movie is null) return NotFound();
        var deleted = await moviesService.Delete(movie);
        return Ok(mapper.Map<MovieDetailsDto>(deleted));
    }

    private static string? ValidatePoster(IFormFile? poster)
    {
        if (poster is null || poster.Length == 0) return "A poster file is required";
        if (poster.Length > MaxPosterSize) return "The poster must not exceed 1 MB";
        var extension = Path.GetExtension(poster.FileName).ToLowerInvariant();
        return AllowedExtensions.Contains(extension) ? null : "The poster must be a .png or .jpg file";
    }

    private static async Task<byte[]> ReadPoster(IFormFile poster)
    {
        await using var stream = new MemoryStream();
        await poster.CopyToAsync(stream);
        return stream.ToArray();
    }
}

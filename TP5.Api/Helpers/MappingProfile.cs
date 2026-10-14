using AutoMapper;
using TP5.Api.Dtos;
using TP5.Api.Models;

namespace TP5.Api.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GenreDto, Genre>();
        CreateMap<Genre, GenreDto>();
        CreateMap<MovieDto, Movie>()
            .ForMember(destination => destination.Poster, options => options.Ignore())
            .ForMember(destination => destination.Genre, options => options.Ignore());
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(destination => destination.GenreName, options => options.MapFrom(source => source.Genre!.Name));
    }
}

using Mapster;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Config
{
    public static class configMapsterFilm
    {
        public static void configMapsterFilmRegister(this IServiceCollection services)
        {
            //FilmDto vers FilmViewLModel
            TypeAdapterConfig<FilmDto, FilmViewModel>
                .NewConfig()
                .Map(dest => dest.Director, src => src.Director.FullName)
                .Map(dest => dest.Scenarist, src => src.Scenarist.FullName)
                .Map(dest => dest.Duration, src => src.Duration.ToString())
                .Map(dest => dest.AgeRating, src => src.AgeRating.Name)
                .Map(dest => dest.Genre, src => src.Genre.Name)
                .Map(dest => dest.FirstActor, src => src.FirstActor.FullName);
            
            //FilmViewLModel vers FilmDto
            TypeAdapterConfig<FilmViewModel, FilmDto>
                .NewConfig()
                .Ignore(dest => dest.Scenarist)
                .Ignore(dest => dest.Director)
                .Ignore(dest => dest.FirstActor)
                .Map(dest => dest.AgeRating, src => new AgeRatingDto{Name = src.AgeRating})
                .Map(dest => dest.Genre, src => new GenreDto{Name = src.Genre});
        }
    }
}
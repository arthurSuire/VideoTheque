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
                //.Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Director, src => src.Director.FullName)
                .Map(dest => dest.Scenarist, src => src.Scenarist.FullName)
                .Map(dest => dest.Duration, src => src.Duration.ToString())
                //.Map(dest => dest.Support, src => src.Support)
                .Map(dest => dest.AgeRating, src => src.AgeRating.Name)
                .Map(dest => dest.Genre, src => src.Genre.Name)
                //.Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.FirstActor, src => src.FirstActor.FullName);
            
            //FilmViewLModel vers FilmDto
            TypeAdapterConfig<FilmViewModel, FilmDto>
                .NewConfig()
                .Map(dest => dest.Duration, src => src.Duration)
                .Map(dest => dest.AgeRating.Name, src => src.AgeRating)
                .Map(dest => dest.Genre.Name, src => src.Genre);
        }
    }
}
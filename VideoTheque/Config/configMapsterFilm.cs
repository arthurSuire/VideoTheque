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
                //.Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Director.Id, src => src.Director)
                .Map(dest => dest.Scenarist.Id, src => src.Scenarist)
                .Map(dest => dest.Duration, src => src.Duration)
                //.Map(dest => dest.Support, src => src.Support)
                .Map(dest => dest.AgeRating.Id, src => src.AgeRating)
                .Map(dest => dest.Genre.Id, src => src.Genre)
                //.Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.FirstActor.Id, src => src.FirstActor);
            
            //FilmDto vers BlurayDto
            TypeAdapterConfig<FilmDto, BluRayDto>
                .NewConfig()
                //.Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.IdDirector, src => src.Director.Id)
                .Map(dest => dest.IdScenarist, src => src.Scenarist.Id)
                .Map(dest => dest.Duration, src => src.Duration.ToString())
                .Map(dest => dest.IdAgeRating, src => src.AgeRating.Id)
                .Map(dest => dest.IdGenre, src => src.Genre.Id)
                //.Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.IdFirstActor, src => src.FirstActor.Id);
            
            //FilmDto vers BlurayDto
            TypeAdapterConfig<BluRayDto, FilmDto>
                .NewConfig()
                //.Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Director, src => src.IdDirector)
                .Map(dest => dest.Scenarist, src => src.IdScenarist)
                .Map(dest => dest.Duration, src => src.Duration.ToString())
                .Map(dest => dest.AgeRating, src => src.IdAgeRating)
                .Map(dest => dest.Genre, src => src.IdGenre)
                //.Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.FirstActor, src => src.IdFirstActor);
        }
    }
}
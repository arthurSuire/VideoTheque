using Mapster;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Config
{
    public class configMapsterFilm
    {
        public configMapsterFilm(IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<FilmDto, FilmViewModel>()
                .Map(filmView => filmView.Id, filmDto => filmDto.Id)
                .Map(filmView => filmView.Director, filmDto => filmDto.Director)
                .Map(filmView => filmView.Scenarist, filmDto => filmDto.Scenarist)
                .Map(filmView => filmView.Duration, filmDto => filmDto.Duration)
                .Map(filmView => filmView.Support, filmDto => filmDto.Support)
                .Map(filmView => filmView.AgeRating, filmDto => filmDto.AgeRating)
                .Map(filmView => filmView.Genre, filmDto => filmDto.Genre)
                .Map(filmView => filmView.Title, filmDto => filmDto.Title)
                .Map(filmView => filmView.FirstActor, filmDto => filmDto.FirstActor);
        }
    }
}
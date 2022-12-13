using Microsoft.OpenApi.Extensions;
using VideoTheque.Constants;

namespace VideoTheque.DTOs
{
    public class FilmDto
    {
        public int Id { get; set; }
        public PersonneDto Director { get; set; }
        
        public PersonneDto Scenarist { get; set; }
        public int Duration { get; set; }
        public string Support { get; set; }
        public AgeRatingDto AgeRating { get; set; }
        public GenreDto Genre { get; set; }
        public string Title { get; set; }
        public PersonneDto FirstActor { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public int? IdOwner { get; set; }

        public FilmDto(BluRayDto bluRayDto)
        {
            this.Id = bluRayDto.Id;
            this.Director = new PersonneDto { Id = bluRayDto.IdDirector };
            this.Scenarist = new PersonneDto { Id = bluRayDto.IdScenarist };
            this.Duration = bluRayDto.Duration;
            this.Support = EnumSupports.BluRay.GetDisplayName();
            this.AgeRating = new AgeRatingDto { Id = bluRayDto.IdAgeRating };
            this.Genre = new GenreDto { Id = bluRayDto.IdGenre };
            this.Title = bluRayDto.Title;
            this.FirstActor = new PersonneDto { Id = bluRayDto.IdFirstActor };
        }
    }
}
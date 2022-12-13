namespace VideoTheque.DTOs
{
    public class FilmDto
    {
        public int Id { get; set; }
        public int IdDirector { get; set; }
        public int IdScenarist { get; set; }
        public long Duration { get; set; }
        public int Support { get; set; }
        public int IdAgeRating { get; set; }
        public int IdGenre { get; set; }
        public string Title { get; set; }
        public int IdFirstActor { get; set; }

        public FilmDto(BluRayDto bluRayDto)
        {
            this.Id = bluRayDto.Id;
            this.IdDirector = bluRayDto.IdDirector;
            this.IdScenarist = bluRayDto.IdScenarist;
            this.Duration = bluRayDto.Duration;
            this.Support = bluRayDto.Id;
            this.IdAgeRating = bluRayDto.IdAgeRating;
            this.IdGenre = bluRayDto.IdGenre;
            this.Title = bluRayDto.Title;
            this.IdFirstActor = bluRayDto.IdFirstActor;
        }
    }
}
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.AgeRating
{
    public interface IAgeRatingRepository
    {
        Task<List<AgeRatingDto>> GetAgeRating();

        ValueTask<AgeRatingDto?> GetAgeRating(int id);

        Task InsertAgeRating(AgeRatingDto ageRating);

        Task UpdateAgeRating(int id, AgeRatingDto ageRating);

        Task DeleteAgeRating(int id);
    }
}
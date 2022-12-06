using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Genres;

namespace VideoTheque.Businesses.AgeRating
{
    public class AgeRatingBusiness: IAgeRatingBusiness
    {
    private readonly IAgeRatingRepository _ageRatingDao;

    public AgeRatingBusiness(IAgeRatingRepository ageRatingDao)
    {
        _ageRatingDao = ageRatingDao;
    }

    public Task<List<AgeRatingDto>> GetAgeRating() => _ageRatingDao.GetAgeRating();

    public AgeRatingDto GetAgeRating(int id)
    {
        var ageRating = _ageRatingDao.GetAgeRating(id).Result;

        if (ageRating == null)
        {
            throw new NotFoundException($"AgeRating '{id}' non trouvé");
        }

        return ageRating;
    }

    public AgeRatingDto InsertAgeRating(AgeRatingDto ageRating)
    {
        if (_ageRatingDao.InsertAgeRating(ageRating).IsFaulted)
        {
            throw new InternalErrorException($"Erreur lors de l'insertion de la limite d'âge : {ageRating.Name}");
        }

        return ageRating;
    }

    public void UpdateAgeRating(int id, AgeRatingDto ageRating)
    {
        if (_ageRatingDao.UpdateAgeRating(id, ageRating).IsFaulted)
        {
            throw new InternalErrorException($"Erreur lors de la modification du genre {ageRating.Name}");
        }
    }
                

    public void DeleteAgeRating(int id)
    {
        if (_ageRatingDao.DeleteAgeRating(id).IsFaulted)
        {
            throw new InternalErrorException($"Erreur lors de la suppression du genre d'identifiant {id}");
        }
    }
}
}
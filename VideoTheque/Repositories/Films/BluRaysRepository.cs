using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public class BluRaysRepository: IBluRaysRepository
    {
        private readonly VideothequeDb _db;

        public BluRaysRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<BluRayDto>> GetBluRays() => _db.BluRays.ToListAsync();

        public Task<List<BluRayDto>> GetBluRaysPartenaire(int idPartenaire) => _db.BluRays.ToListAsync();

        public ValueTask<BluRayDto> GetBluRay(int id) => _db.BluRays.FindAsync(id);
        
        public ValueTask<BluRayDto> GetBluRayPartenaire(int idPartenaire, int idFilmPartenaire) => _db.BluRays.FindAsync(idFilmPartenaire);
        
        public Task InsertBluRay(BluRayDto bluRayDto) 
        {
            _db.BluRays.AddAsync(bluRayDto);
            return _db.SaveChangesAsync();
        }
        
        public Task UpdateBluRay(BluRayDto bluRayDto)
        {
            var bluRayToUpdate = _db.BluRays.FindAsync(bluRayDto.Id).Result;

            if (bluRayToUpdate is null)
            {
                throw new KeyNotFoundException($"BluRay '{bluRayDto.Id}' non trouvé");
            }
            bluRayToUpdate.Title = bluRayDto.Title;
            bluRayToUpdate.Duration = bluRayDto.Duration;
            bluRayToUpdate.IdFirstActor = bluRayDto.IdFirstActor;
            bluRayToUpdate.IdDirector = bluRayDto.IdDirector;
            bluRayToUpdate.IdScenarist = bluRayDto.IdScenarist;
            bluRayToUpdate.IdAgeRating = bluRayDto.IdAgeRating;
            bluRayToUpdate.IdGenre = bluRayDto.IdGenre;
            return _db.SaveChangesAsync();
        }

        public Task DeleteBluRay(int id)
        {
            var bluRayToDelete = _db.BluRays.FindAsync(id).Result;

            if (bluRayToDelete is null)
            {
                throw new KeyNotFoundException($"BluRay '{id}' non trouvé");
            }
            _db.BluRays.Remove(bluRayToDelete);
            return _db.SaveChangesAsync();
        }
        
        public Task DeleteBluRayName(string name)
        {
            var bluRayToDelete = _db.BluRays.FindAsync(name).Result;

            if (bluRayToDelete is null)
            {
                throw new KeyNotFoundException($"BluRay '{name}' non trouvé");
            }
            _db.BluRays.Remove(bluRayToDelete);
            return _db.SaveChangesAsync();
        }
    }
}
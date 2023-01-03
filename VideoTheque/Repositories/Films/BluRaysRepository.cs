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

        public ValueTask<BluRayDto> GetBluRay(int id) => _db.BluRays.FindAsync(id);
        
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
    }
}
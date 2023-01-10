using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Films
{
    public interface IBluRaysRepository
    {
        Task<List<BluRayDto>> GetBluRays();
        
        ValueTask<BluRayDto?> GetBluRay(int id);
        
        Task InsertBluRay(BluRayDto bluRay);

        Task UpdateBluRay(BluRayDto bluRay);

        Task DeleteBluRay(int id);
    }
}
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    public interface IBluRaysRepository
    {
        Task<List<BluRayDto>> GetBluRays();

        ValueTask<BluRayDto?> GetBluRay(int id); 
    }
}
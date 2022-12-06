using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    public class BluRaysRepository: IBluRaysRepository
    {
        public Task<List<BluRayDto>> GetBluRays()
        {
            throw new NotImplementedException();
        }

        public ValueTask<BluRayDto?> GetBluRay(int id)
        {
            throw new NotImplementedException();
        }
    }
}
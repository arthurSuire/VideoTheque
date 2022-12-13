using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Supports;

namespace VideoTheque.Businesses.Support
{
    public class SupportBusiness: ISupportBusiness
    {
        private readonly ISupportsRepository _supportDao;

        public SupportBusiness(ISupportsRepository supportDao)
        {
            _supportDao = supportDao;
        }

        public List<SupportDto> GetSupports() => _supportDao.GetSupports();

        public SupportDto GetSupport(int id)
        {
            var support = _supportDao.GetSupport(id);

            if (support == null)
            {
                throw new NotFoundException($"Support '{id}' non trouvé");
            }

            return support;
        }
    }
}
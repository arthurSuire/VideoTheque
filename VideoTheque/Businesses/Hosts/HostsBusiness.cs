using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Hosts;

namespace VideoTheque.Businesses.Hosts
{
    public class HostsBusiness : IHostsBusiness
    {
        private readonly IHostsRepository _hostDao;

        public HostsBusiness(IHostsRepository hostDao)
        {
            _hostDao = hostDao;
        }

        public Task<List<HostDto>> GetHosts() => _hostDao.GetHosts();

        public HostDto GetHost(int id)
        {
            var host = _hostDao.GetHost(id).Result;

            if (host == null)
            {
                throw new NotFoundException($"Host '{id}' non trouvé");
            }

            return host;
        }

        public HostDto InsertHost(HostDto host)
        {
            if (_hostDao.InsertHost(host).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion de l'host {host.Name}");
            }

            return host;
        }

        public void UpdateHost(int id, HostDto host)
        {
            if (_hostDao.UpdateHost(id, host).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification de l'host {host.Name}");
            }
        }

        public void DeleteHost(int id)
        {
            if (_hostDao.DeleteHost(id).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression de l'host d'identifiant {id}");
            }
        }
    }
}

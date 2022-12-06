using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Personnes
{
    public class PersonnesBusiness : IPersonnesBusiness
    {
        private readonly IPersonnesRepository _personneDao;

        public PersonnesBusiness(IPersonnesRepository personneDao)
        {
            _personneDao = personneDao;
        }

        public Task<List<PersonneDto>> GetPersonnes() => _personneDao.GetPersonnes();

        public PersonneDto GetPersonne(int id)
        {
            var personne = _personneDao.GetPersonne(id).Result;

            if (personne == null)
            {
                throw new NotFoundException($"Personne '{id}' non trouvée");
            }

            return personne;
        }
        public PersonneDto InsertPersonne(PersonneDto personne)
        {
            if (_personneDao.InsertPersonne(personne).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion du genre {personne.FirstName + " " + personne.LastName}");
            }

            return personne;
        }

        public void UpdatePersonne(int id, PersonneDto personne)
        {
            if (_personneDao.UpdatePersonne(id, personne).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification de la personne {personne.FirstName +" "+ personne.LastName}");
            }
        }

        public void DeletePersonne(int id)
        {
            if (_personneDao.DeletePersonne(id).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression de la personne d'identifiant {id}");
            }
        }
    }
}

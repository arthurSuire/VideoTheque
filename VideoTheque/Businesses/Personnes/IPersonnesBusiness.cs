﻿using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Personnes
{
    public interface IPersonnesBusiness
    {
        Task<List<PersonneDto>> GetPersonnes();

        PersonneDto GetPersonne(int id);

        PersonneDto InsertPersonne(PersonneDto personne);

        void UpdatePersonne(int id, PersonneDto personne);

        void DeletePersonne(int id);
    }
}

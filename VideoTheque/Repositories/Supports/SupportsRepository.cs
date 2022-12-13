using Microsoft.OpenApi.Extensions;
using VideoTheque.Constants;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    public class SupportsRepository: ISupportsRepository
    {
        public List<SupportDto> GetSupports()
        {
            var listeEnums = Enum.GetValues(typeof(EnumSupports)).Cast<EnumSupports>();

            var liste = new List<SupportDto>();

            foreach (var elts in listeEnums)
            {
                var supp = new SupportDto();
                supp.Name = elts.GetDisplayName();
                supp.Id = (int)elts;
                liste.Add(supp);
            }
            return liste;
        }

        public SupportDto GetSupport(int id)
        {
            var support = new SupportDto();
            var liste = GetSupports();

            foreach (var elts in liste)
            {
                support = null;
                
                if (elts.Id == id)
                {
                    support = elts;
                    return support;
                }
            }
            if (support == null)
            {
                throw new KeyNotFoundException($"Support '{id}' non trouvé");
            }
            return support;
        }
    }
}
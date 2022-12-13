using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Support
{
    public interface ISupportBusiness
    {
        List<SupportDto> GetSupports();

        SupportDto GetSupport(int id);
    }
}
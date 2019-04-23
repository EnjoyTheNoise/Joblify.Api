using System.Threading.Tasks;

namespace Joblify.Core.Offers
{
    public interface IOfferService
    {
        Task<OfferDto> AddOfferAsync(OfferDto osfferDto);
    }
}
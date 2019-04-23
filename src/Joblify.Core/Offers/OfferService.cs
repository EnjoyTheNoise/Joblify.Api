using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using System.Threading.Tasks;

namespace Joblify.Core.Offers
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OfferDto> AddOfferAsync(OfferDto offerDto)
        {
            var offer = _mapper.Map<Offer>(offerDto);

            await _unitOfWork.OfferRepository.AddAsync(offer);
            await _unitOfWork.CommitAsync();

            var created = _mapper.Map<OfferDto>(offer);

            return created;
        }
    }
}

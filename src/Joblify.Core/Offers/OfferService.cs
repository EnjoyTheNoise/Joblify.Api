using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using System.Threading.Tasks;
using Joblify.Core.Offers.Dto;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<GetAllCategoriesDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.Entities.ToListAsync();
            var getAllCategoriesDtos = _mapper.Map<List<Category>, List<GetAllCategoriesDto>>(categories);
            return getAllCategoriesDtos;
        }

        public async Task<IEnumerable<GetAllTradesDto>> GetAllTrades()
        {
            var trades = await _unitOfWork.TradeRepository.Entities.ToListAsync();
            var getAllTradesDtos = _mapper.Map<List<Trade>, List<GetAllTradesDto>>(trades);
            return getAllTradesDtos;
        }

        public async Task<GetOfferByIdDto> GetOfferById(int id)
        {
            var offer = await _unitOfWork.OfferRepository.Entities.Where(x => x.Id == id).Include(x => x.User).FirstOrDefaultAsync();
            var showOfferDto = _mapper.Map<Offer, GetOfferByIdDto>(offer);
            return showOfferDto;
        }
    }
}

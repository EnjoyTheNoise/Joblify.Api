using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var trade = await GetTradeByNameAsync(offerDto.Trade);
            var category = await GetCategoryByNameAsync(offerDto.Category);
            var user = await GetUserByIdAsync(offerDto.UserId);

            if(user == null || category == null)
            {
                return null;
            }

            offer.UserId = user.Id;
            offer.TradeId = trade.Id;
            offer.CategoryId = category.Id;

            await _unitOfWork.OfferRepository.AddAsync(offer);
            await _unitOfWork.CommitAsync();

            var created = _mapper.Map<OfferDto>(offer);
            return created;
        }

        private async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository
               .Entities.Where(t => t.Id == id)
               .FirstAsync();

            return user;
        }

        private async Task<Trade> GetTradeByNameAsync(string name)
        {
            var trade = await _unitOfWork.TradeRepository
               .Entities.Where(t => t.Name == name)
               .FirstOrDefaultAsync();

            if (trade == null)
            {
                trade = new Trade
                {
                    Name = name
                };

                await _unitOfWork.TradeRepository.AddAsync(trade);
                await _unitOfWork.CommitAsync();
            }

            return trade;
        }

        private async Task<Category> GetCategoryByNameAsync(string name)
        {
            var category = await _unitOfWork.CategoryRepository
                .Entities.Where(c => c.Name == name)
                .FirstOrDefaultAsync();

            return category;
        }
    }
}

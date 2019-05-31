﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Search;
using Joblify.Search.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Joblify.Core.Offers.Dto;

namespace Joblify.Core.Offers
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOfferSearchIndex _offerSearchIndex;

        public OfferService(IMapper mapper, IUnitOfWork unitOfWork, IOfferSearchIndex offerSearchIndex)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _offerSearchIndex = offerSearchIndex;
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

            var indexModel = _mapper.Map<OfferSearchModel>(offer);
            await _offerSearchIndex.AddOfferAsync(indexModel);

            return offerDto;
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

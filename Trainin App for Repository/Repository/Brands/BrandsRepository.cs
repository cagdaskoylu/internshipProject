using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Data.DTO.Brand;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Brands
{
    public class BrandsRepository : IBrandsRepository
    {
        private readonly FuelPriceDbContext _fuelPriceDbContext;
        private readonly IMapper _mapper;
        public BrandsRepository(FuelPriceDbContext context, IMapper mapper)
        {
            _fuelPriceDbContext = context;
            _mapper = mapper;
        }
        public async Task<BrandsEntity> Delete(Guid brandId)
        {
            BrandsEntity brandEntity = await _fuelPriceDbContext.Brand.FirstOrDefaultAsync(x => x.Id == brandId);
            brandEntity.isDeleted = true;
            await _fuelPriceDbContext.SaveChangesAsync();
            return brandEntity;
        }

        public async Task DeleteBrand(BrandsEntity brand)
        {
            _fuelPriceDbContext.Remove(brand);
            await _fuelPriceDbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAll()
        {
            await _fuelPriceDbContext.Brand.Where(x => x.isDeleted == false).ForEachAsync(x => x.isDeleted = true);

            await _fuelPriceDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllOldBrands()
        {
            var brandList = await _fuelPriceDbContext.Brand.Where(x => x.isDeleted == true).ToListAsync();
            _fuelPriceDbContext.RemoveRange(brandList);

            await _fuelPriceDbContext.SaveChangesAsync();
        }

        public async Task<BrandsEntity> FindByAsync(Expression<Func<BrandsEntity, bool>> predicate, bool trace = false)
        {
            return await _fuelPriceDbContext.Brand.AsNoTracking().SingleOrDefaultAsync(predicate);
        }
        public async Task<List<BrandsEntity>> GetAll()
        {
            return await _fuelPriceDbContext.Brand.AsQueryable().Where(x => x.isDeleted == false).ToListAsync();
        }

        public async Task<BrandsEntity> GetById(Guid id)
        {
            return await _fuelPriceDbContext.Brand.FirstOrDefaultAsync(x => x.Id == id && x.isDeleted == false);
        }

        public async Task<List<string>> GetAllCitiesNames()
        {
            return await _fuelPriceDbContext.City.AsQueryable().Select(x => x.Name).ToListAsync();
        }

        public async Task<List<string>> GetDistrictById(int id)
        {
            return await _fuelPriceDbContext.District.AsQueryable().Where(x => x.CitiesEntityId == id).Select(x => x.Name).ToListAsync();
        }

        public async Task<int> GetCityIdByName(string name)
        {
            var cityId = await _fuelPriceDbContext.City.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
            return cityId;
        }

        public async Task<BrandsEntity> GetBrandByNameAndCityAndDistrict(string name, string city, string district)
        {
            return await _fuelPriceDbContext.Brand.FirstOrDefaultAsync(x => x.Name == name && x.City == city && x.District == district);
        }

        public async Task<List<BrandsEntity>> GetBrandByNameAndCity(string name, string city)
        {
            return await _fuelPriceDbContext.Brand.AsQueryable().Where(x => x.Name == name && x.City == city).ToListAsync();
        }

        public async Task<BrandsEntity> Post(BrandsEntity brand)
        {
            brand.isDeleted = false;

            _fuelPriceDbContext.Brand.Add(brand);
            await _fuelPriceDbContext.SaveChangesAsync();
            return brand;
        }

        public async Task<BrandsEntity> Put(BrandsEntity brand)
        {
            _fuelPriceDbContext.Brand.Update(brand);
            await _fuelPriceDbContext.SaveChangesAsync();
            return brand;
        }

        public async Task<BrandsEntity> GetNotDeletedSimilarBrand(string name, string city, string district)
        {
            return await _fuelPriceDbContext.Brand.Where(x => x.Name.Contains(name) && x.City == city && x.District == district && x.isDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<BrandsEntity> GetDeletedSimilarBrand(string name, string city, string district)
        {
            return await _fuelPriceDbContext.Brand.Where(x => x.Name.Contains(name) && x.City == city && x.District == district && x.isDeleted == true).FirstOrDefaultAsync();
        }

        public async Task<string> GetLastUpdate()
        {
            return await _fuelPriceDbContext.Brand.Select(x => x.LastUpdate).FirstOrDefaultAsync();
        }

    }
}

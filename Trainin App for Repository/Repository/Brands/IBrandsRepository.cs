using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data.DTO.Brand;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Brands
{
    public interface IBrandsRepository
    {
        Task<List<BrandsEntity>> GetAll();
        Task<BrandsEntity> GetById(Guid brandId);
        Task<List<BrandsEntity>> GetBrandByNameAndCity(string name, string city);
        Task<BrandsEntity> GetBrandByNameAndCityAndDistrict(string name, string city, string district);
        Task<BrandsEntity> GetNotDeletedSimilarBrand(string name, string city, string district);
        Task<BrandsEntity> GetDeletedSimilarBrand(string name, string city, string district);
        Task<string> GetLastUpdate();
        Task SoftDeleteAll();
        Task DeleteAllOldBrands();
        Task DeleteBrand(BrandsEntity brand);
        Task<List<string>> GetAllCitiesNames();
        Task<List<string>> GetDistrictById(int id);
        Task<int> GetCityIdByName(string name);
        Task<BrandsEntity> Post(BrandsEntity brand);
        Task<BrandsEntity> Put(BrandsEntity brand);
        Task<BrandsEntity> Delete(Guid brandId);
        Task<BrandsEntity> FindByAsync(Expression<Func<BrandsEntity, bool>> predicate, bool trace = false);
        
    }
}

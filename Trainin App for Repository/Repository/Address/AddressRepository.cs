using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FuelPriceDbContext _fuelPriceDbContext;
        public AddressRepository(FuelPriceDbContext context)
        {
            _fuelPriceDbContext = context;
        }

        public async Task<List<AddressesEntity>> GetAll()
        {
            return await _fuelPriceDbContext.Address.AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();

        }

        public async Task<List<AddressesEntity>> GetByUserId(Guid Id)
        {
            return await _fuelPriceDbContext.Address.AsQueryable().Where(x => x.UsersEntityId == Id && x.IsDeleted == false).ToListAsync();
        }
        public async Task<AddressesEntity> Delete(Guid addressId)
        {
            AddressesEntity addressEntity = await _fuelPriceDbContext.Address.FirstOrDefaultAsync(x => x.Id == addressId);
            addressEntity.IsDeleted = true;
            await _fuelPriceDbContext.SaveChangesAsync();
            return addressEntity;
        }

        public async Task<AddressesEntity> FindByAsync(Expression<Func<AddressesEntity, bool>> predicate, bool trace = false)
        {
            return await _fuelPriceDbContext.Address.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<AddressesEntity> GetById(Guid id)
        {
            return await _fuelPriceDbContext.Address.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task<AddressesEntity> Post(AddressesEntity address)
        {
            address.IsDeleted = false;
           
            _fuelPriceDbContext.Address.Add(address);
            await _fuelPriceDbContext.SaveChangesAsync();
            return address;
        }

        public async Task<AddressesEntity> Put(AddressesEntity address)
        {
            _fuelPriceDbContext.Address.Update(address);
            await _fuelPriceDbContext.SaveChangesAsync();
            return address; 
        }

        public async Task<AddressesEntity> GetFav()
        {
            return await _fuelPriceDbContext.Address.FirstOrDefaultAsync(x => x.IsFav == true);
        }

    }
}











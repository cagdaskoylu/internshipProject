using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.FavStation
{
    public class FavStationsRepository: IFavStationsRepository
    {
        private readonly FuelPriceDbContext _fuelPriceDbContext;
        public FavStationsRepository(FuelPriceDbContext context)
        {
            _fuelPriceDbContext = context;
        }
        public async Task<List<FavStationsEntity>> GetAll()
        {
            return await _fuelPriceDbContext.FavStations.AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();

        }
        public async Task<FavStationsEntity> GetById(Guid Id)
        {
            return await _fuelPriceDbContext.FavStations.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
        }
        public async Task<List<FavStationsEntity>> GetByUserId(Guid userId)
        {
            return await _fuelPriceDbContext.FavStations.AsQueryable().Where(x => x.UsersEntityId == userId && x.IsDeleted == false).ToListAsync();
        }
        public async Task<FavStationsEntity> Delete(Guid favStationId)
        {

            FavStationsEntity stationEntity = await _fuelPriceDbContext.FavStations.FirstOrDefaultAsync(x => x.Id == favStationId);
            stationEntity.IsDeleted = true;
            await _fuelPriceDbContext.SaveChangesAsync();
            return stationEntity;
        }
        public async Task<FavStationsEntity> Post(FavStationsEntity stationCreate)
        {
            stationCreate.IsDeleted = false;
            _fuelPriceDbContext.FavStations.Add(stationCreate);
            await _fuelPriceDbContext.SaveChangesAsync();
            return stationCreate;
        }



    }
}

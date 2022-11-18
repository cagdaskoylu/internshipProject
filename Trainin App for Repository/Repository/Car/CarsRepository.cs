using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Cars
{
    public class CarsRepository : ICarsRepository
    {
        private readonly FuelPriceDbContext _fuelPriceDbContext;
        public CarsRepository(FuelPriceDbContext context)
        {
            _fuelPriceDbContext = context;
        }
        public async Task<CarsEntity> Delete(Guid carId)
        {
            CarsEntity carsEntity = await _fuelPriceDbContext.Car.FirstOrDefaultAsync(x => x.Id == carId);
            carsEntity.IsDeleted = true;
            await _fuelPriceDbContext.SaveChangesAsync();
            return carsEntity;
        }

        public async Task<CarsEntity> FindByAsync(Expression<Func<CarsEntity, bool>> predicate, bool trace = false)
        {
            return await _fuelPriceDbContext.Car.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<List<CarsEntity>> GetAll()
        {
            return await _fuelPriceDbContext.Car.AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<CarsEntity> GetById(Guid id)
        {
            return await _fuelPriceDbContext.Car.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task<List<CarsEntity>> GetByUserId(Guid Id)
        {
            return await _fuelPriceDbContext.Car.AsQueryable().Where(x => x.UsersEntityId == Id && x.IsDeleted == false).ToListAsync();
        }

        public async Task<CarsEntity> Post(CarsEntity car)
        {
            car.IsDeleted = false;

            _fuelPriceDbContext.Car.Add(car);
            await _fuelPriceDbContext.SaveChangesAsync();
            return car;
        }

        public async Task<CarsEntity> Put(CarsEntity car)
        {
            _fuelPriceDbContext.Car.Update(car);
            await _fuelPriceDbContext.SaveChangesAsync();
            return car;
        }

        public async Task<CarsEntity> GetFav()
        {
            return await _fuelPriceDbContext.Car.FirstOrDefaultAsync(x => x.IsFav == true);
        }
    }
}

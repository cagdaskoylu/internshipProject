using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Cars
{
    public interface ICarsRepository
    {
        Task<List<CarsEntity>> GetAll();
        Task<CarsEntity> GetById(Guid carId);
        Task<List<CarsEntity>> GetByUserId(Guid userId);
        Task<CarsEntity> GetFav();
        Task<CarsEntity> Post(CarsEntity car);
        Task<CarsEntity> Put(CarsEntity car);
        Task<CarsEntity> Delete(Guid carId);
        Task<CarsEntity> FindByAsync(Expression<Func<CarsEntity, bool>> predicate, bool trace = false);
    }
}

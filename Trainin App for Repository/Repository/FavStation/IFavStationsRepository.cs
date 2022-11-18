using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.FavStation
{
    public interface IFavStationsRepository
    {
        Task<List<FavStationsEntity>> GetAll();
        Task<List<FavStationsEntity>> GetByUserId(Guid userId);
        Task<FavStationsEntity> Post(FavStationsEntity station);
        Task<FavStationsEntity> Delete(Guid stationId);
        Task<FavStationsEntity> GetById(Guid id);
    }
}

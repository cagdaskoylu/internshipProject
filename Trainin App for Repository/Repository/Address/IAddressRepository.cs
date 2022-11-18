using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository.Address
{
    public interface IAddressRepository
    {
        Task<List<AddressesEntity>> GetAll();
        Task<AddressesEntity> GetById(Guid addressId);
        Task<List<AddressesEntity>> GetByUserId(Guid userId);
        Task<AddressesEntity> GetFav();
        Task<AddressesEntity> Post(AddressesEntity address);
        Task<AddressesEntity> Put(AddressesEntity address);
        Task<AddressesEntity> Delete(Guid addressId);
        Task<AddressesEntity> FindByAsync(Expression<Func<AddressesEntity, bool>> predicate, bool trace = false);
    }
}

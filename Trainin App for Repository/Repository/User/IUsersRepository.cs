using System.Collections;
using Trainin_App_for_Repository.Data.Entities;
using System.Collections.Generic;
using System;
using Trainin_App_for_Repository.Data.DTO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Trainin_App_for_Repository.Repository
{
    public interface IUsersRepository
    {
        Task<List<UsersEntity>> GetAll();
        Task<UsersEntity> GetById(Guid id);
        Task<UsersEntity> Post(UsersEntity user);
        Task<UsersEntity> Put(UsersEntity user);
        Task<UsersEntity> Delete(Guid userId); 
        Task<UsersEntity> FindByAsync(Expression<Func<UsersEntity, bool>> predicate, bool trace = false);
        Task<UsersEntity> Login(UsersEntity userEntity);
        Task<UsersEntity> GetByEmail(string email);

    }
}

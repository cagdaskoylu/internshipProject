using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly FuelPriceDbContext _fuelPriceDbContext;
        public UsersRepository(FuelPriceDbContext context)
        {
            _fuelPriceDbContext = context;
        }

        public async Task<List<UsersEntity>> GetAll()
        {
            return await _fuelPriceDbContext.User.AsQueryable().Where(x => x.isDeleted == false).ToListAsync();

        }

        public async Task<UsersEntity> Delete(Guid userId)
        {

            UsersEntity userEntity = await _fuelPriceDbContext.User.FirstOrDefaultAsync(x => x.Id == userId);
            userEntity.isDeleted = true;
            await _fuelPriceDbContext.SaveChangesAsync();
            return userEntity;
        }

        public async Task<UsersEntity> GetById(Guid Id)
        {
            return await _fuelPriceDbContext.User.FirstOrDefaultAsync(x => x.Id == Id && x.isDeleted == false);
        }

        public async Task<UsersEntity> FindByAsync(Expression<Func<UsersEntity, bool>> predicate, bool trace = false)
        {
            return await _fuelPriceDbContext.User.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<UsersEntity> Post(UsersEntity userCreate)
        {
            userCreate.isDeleted = false;
            _fuelPriceDbContext.User.Add(userCreate);
            await _fuelPriceDbContext.SaveChangesAsync();
            return userCreate;
        }


        public async Task<UsersEntity> Put(UsersEntity usersEntity)
        {
            _fuelPriceDbContext.User.Update(usersEntity);
            await _fuelPriceDbContext.SaveChangesAsync();
            return usersEntity;
        }

        public async Task<UsersEntity> Login(UsersEntity userEntity)
        {
            var check = await _fuelPriceDbContext.User.FirstOrDefaultAsync(x => x.Email == userEntity.Email && x.isDeleted == false);
            return check;
        }

        public async Task<UsersEntity> GetByEmail(string email)
        {
            return await _fuelPriceDbContext.User.FirstOrDefaultAsync(x => x.Email == email && x.isDeleted == false);
        }
    }
}

using System;
using MongoDB.Bson;
using MongoDB.Driver;
using FortCode.Model;
using System.Threading.Tasks;
using FortCode.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

using MongoDB.Driver.Linq;
using FortCode.Data;
using FortCode.Data;

namespace FortCode.Data
{
    public class FortRepository : IFortRepository
    {
        private readonly FortContext _context = null;

        public FortRepository(IOptions<Settings> settings)
        {
            _context = new FortContext(settings);
        }

        public async Task<IEnumerable<Fort>> GetAllData()
        {
            try
            {
                //return await _context.Fort.(x => true);
                return await _context.Fort.Find(x => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Fort> GetUserInfo(string id)
        {
            try
            {

                return await _context.Fort
                                .Find(fort => fort.Id == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Fort> ValidateUser(string EmailId, string Password)
        {
            try
            {

                return await _context.Fort
                                .Find(fort => fort.Email == EmailId && fort.Password == Password)
                                .FirstOrDefaultAsync();
              
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<IEnumerable<UserLocation>> GetAllCities(string EmailId)
        {
            try
            {

                return await _context.UserLocation
                                .Find(UserLocation => UserLocation.Email == EmailId).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
       
        public virtual async Task<Fort> UpdateOneAsync(Fort item, bool updateDate = true)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                    item.CreatedDate = DateTime.UtcNow;
                    item.UpdatedDate = DateTime.UtcNow;
                    await _context.Fort.InsertOneAsync(item);
                }
                else
                {
                    item.UpdatedDate = DateTime.UtcNow;

                }

                var filter = Builders<Fort>.Filter.Eq(s => s.Id, item.Id);
                await _context.Fort.ReplaceOneAsync(filter, item, new UpdateOptions { IsUpsert = true });


                return item;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public virtual async Task<UserLocation>InsertUserLocation(UserLocation item, bool updateDate = true)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                    item.CreatedDate = DateTime.UtcNow;
                    item.UpdatedDate = DateTime.UtcNow;
                    await _context.UserLocation.InsertOneAsync(item);
                }
                else
                {
                    item.UpdatedDate = DateTime.UtcNow;

                }

                var filter = Builders<UserLocation>.Filter.Eq(s => s.Id, item.Id);
                await _context.UserLocation.ReplaceOneAsync(filter, item, new UpdateOptions { IsUpsert = true });


                return item;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}

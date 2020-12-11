using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FortCode.Model;

namespace FortCode.Interfaces
{
    public interface IFortRepository
    {
        Task<IEnumerable<Fort>> GetAllData();
        Task<Fort> GetUserInfo(string id);
        Task<Fort> UpdateOneAsync(Fort item, bool updateDate = true);
        Task<Fort> ValidateUser(string EmailId, string Password);

        Task<UserLocation> InsertUserLocation(UserLocation item, bool updateDate = true);
        Task<IEnumerable<UserLocation>> GetAllCities(string EmailId);
    }
}

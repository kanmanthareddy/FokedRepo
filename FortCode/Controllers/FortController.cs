using FortCode.Interfaces;
using FortCode.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FortCode.Data;
using MongoDB.Bson;

namespace FortCode.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class FortController : Controller
    {
        private readonly IFortRepository _fortRepository;

        public FortController(IFortRepository FortRepository)
        {
            _fortRepository = FortRepository;
            // _fortRepository = new FortContext(dbConfigs.Value);
        }

        [HttpGet]
        public async Task<IEnumerable<Fort>> GetAllData()
        {
            return await _fortRepository.GetAllData();
        }

        // GET api/notes/5
        [HttpGet]
        public async Task<Fort> Get(string id)
        {
            return await _fortRepository.GetUserInfo(id) ?? new Fort();
        }

        [HttpGet]
        public async Task<ResponseMessage> CreateUser(string Name, string EmailId, string Password)
        {
            if ((Name != null || Name != "") && (EmailId != null || EmailId != "") && (Password != null || Password != ""))
            {
                var existUser = await _fortRepository.ValidateUser(EmailId, Password);
                if (existUser != null)
                {
                    return new ResponseMessage { Status = "Ok", Message = "Duplicate user" };
                }
                else
                {
                    Fort fort = new Fort();
                    fort.Name = Name;
                    fort.Email = EmailId;
                    fort.Password = Password;
                   

                    var UserResponse = await _fortRepository.UpdateOneAsync(fort, true);
                    if (UserResponse != null)
                        return new ResponseMessage { Status = "Ok", Message = "User Created SuccessFully" };
                    else
                        return new ResponseMessage { Status = "Error", Message = "Error in UserCreation" };
                }
            }
            else
            {
                return new ResponseMessage { Status = "Error", Message = "Please Fill Email,Name And Password." };
            }
        }

        [HttpGet]
        public async Task<ResponseMessage> AutheticateUser(string EmailId, String Password)
        {
            if ((EmailId != null || EmailId != "") && (Password != null || Password != ""))
            {
                var validateUser = await _fortRepository.ValidateUser(EmailId, Password);
                if (validateUser != null)
                    return new ResponseMessage { Status = "Ok", Message = "ValidUser" };
                else
                    return new ResponseMessage { Status = "Ok", Message = "InValidUser" };
            }
            else
            {
                return new ResponseMessage { Status = "Note", Message = "Please Fill Email And Password." };
            }
        }

        [HttpGet]
        public async Task<ResponseMessage> AddCityCountry(string EmailId, String Password,string City,string Country)
        {
            if ((EmailId != null || EmailId != "") && (Password != null || Password != "") && (City != null || City != "")
                && (Country != null || Country != ""))
            {
                var validateUser =  _fortRepository.ValidateUser(EmailId, Password).Result;
                if (validateUser != null)
                {
                    UserLocation userLocation = new UserLocation();
                    userLocation.Email = validateUser.Email;
                    userLocation.City = City;
                    userLocation.Country = Country;
                    var UserResponse = await _fortRepository.InsertUserLocation(userLocation, true);

                    if(UserResponse != null)
                    return new ResponseMessage { Status = "Ok", Message = "Updated Successfully" };
                    else
                        return new ResponseMessage { Status = "Ok", Message = "Error in Updation" };
                }
                    
                else
                    return new ResponseMessage { Status = "Ok", Message = "InValidUser" };
            }
            else

            {
                return new ResponseMessage { Status = "Note", Message = "Please Fill Email And Password." };
            }
        }

         [HttpGet]
        public async Task<ResponseMessage> RetrieveCities(string EmailId, String Password)
        {
            if ((EmailId != null || EmailId != "") && (Password != null || Password != "") )
            {
                var validateUser =  _fortRepository.ValidateUser(EmailId, Password).Result;
                if (validateUser != null)
                {
                    var Cities = await _fortRepository.GetAllCities(validateUser.Email);
                    List<string> CityList = new List<string>();
                    Parallel.ForEach(Cities, (city) =>
                    {
                        CityList.Add(city.City);
                    }); 
                    
                    return new ResponseMessage { Status = "Ok", Data = CityList };
                }
                    
                else
                    return new ResponseMessage { Status = "Ok", Message = "InValidUser" };
            }
            else

            {
                return new ResponseMessage { Status = "Note", Message = "Please Fill Email And Password." };
            }
        }


    }
}

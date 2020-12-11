using Microsoft.Extensions.Options;
using MongoDB.Driver;
using FortCode.Model;

namespace FortCode.Data
{
    public class FortContext
    {
        private readonly IMongoDatabase _database = null;

        public FortContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Fort> Fort
        {
            get
            {
                return _database.GetCollection<Fort>("FortData");
            }
        }
        public IMongoCollection<UserLocation> UserLocation
        {
            get
            {
                return _database.GetCollection<UserLocation>("UserLocation");
            }
        }

    }
}

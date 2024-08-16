using MongoDB.Driver;

namespace sciences_nation_back.Services.Interfaces
{
	public interface IMongoDbService
	{
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
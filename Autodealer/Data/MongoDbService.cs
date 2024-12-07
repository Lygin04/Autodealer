using MongoDB.Driver;

namespace Autodealer.Data;

public class MongoDbService
{
    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        Console.WriteLine(mongoUrl.DatabaseName);
        Console.WriteLine(mongoUrl);
        Database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase Database { get; }
}
using Autodealer.Data;
using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using MongoDB.Driver;

namespace Autodealer.Repositories;

public class EngineRepository(MongoDbService mongoDbService) : IEngineRepository
{
    private readonly IMongoCollection<Engine> _engines = mongoDbService.Database.GetCollection<Engine>("engines");

    public async Task<IEnumerable<Engine>?> GetAll()
    {
        return await _engines.Find(FilterDefinition<Engine>.Empty).ToListAsync();
    }

    public Engine GetById(string id)
    {
        var filter = Builders<Engine>.Filter.Eq(x => x.Id, id);
        var engine = _engines.Find(filter).FirstOrDefault();
        return engine ?? throw new InvalidOperationException();
    }

    public async Task<Engine> Create(EngineDto engineDto)
    {
        var engine = new Engine
        {
            Brand = engineDto.Brand,
            Model = engineDto.Model,
            Capacity = engineDto.Capacity,
            CountBlock = engineDto.CountBlock,
        };

        await _engines.InsertOneAsync(engine);
        return engine;
    }

    public async Task Update(Engine newEngine)
    {
        var filter = Builders<Engine>.Filter.Eq(x => x.Id, newEngine.Id);
        var update = Builders<Engine>.Update
            .Set(x => x.Brand, newEngine.Brand)
            .Set(x => x.Model, newEngine.Model)
            .Set(x => x.Capacity, newEngine.Capacity)
            .Set(x => x.CountBlock, newEngine.CountBlock);
        
        await _engines.UpdateOneAsync(filter, update);
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Engine>.Filter.Eq(x => x.Id, id);
        await _engines.DeleteOneAsync(filter);
    }
}
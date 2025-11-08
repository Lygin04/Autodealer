using Autodealer.Data;
using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using MongoDB.Driver;

namespace Autodealer.Repositories;

public class CarRepository(MongoDbService mongoDbService) : ICarRepository
{
    private readonly IMongoCollection<Car> _cars = mongoDbService.Database.GetCollection<Car>("car");

    public async Task<IEnumerable<Car>?> GetAll()
    {
        return await _cars.Find(FilterDefinition<Car>.Empty).ToListAsync();
    }

    public Car GetById(string id)
    {
        var filter = Builders<Car>.Filter.Eq(x => x.Id, id);
        var car = _cars.Find(filter).FirstOrDefault();
        return car ?? throw new InvalidOperationException();
    }

    public async Task<Car> Create(CarDto newCarDto)
    {
        var car = new Car
        {
            Brand = newCarDto.Brand,
            Model = newCarDto.Model,
            Generation = newCarDto.Generation,
            EngineId = newCarDto.EngineId
        };
        await _cars.InsertOneAsync(car);
        return car;
    }

    public async Task Update(Car newCar)
    {
        var filter = Builders<Car>.Filter.Eq(x => x.Id, newCar.Id);
        var update = Builders<Car>.Update
            .Set(x => x.Brand, newCar.Brand)
            .Set(x => x.Model, newCar.Model)
            .Set(x => x.Generation, newCar.Generation)
            .Set(x => x.EngineId, newCar.EngineId);

        await _cars.UpdateOneAsync(filter, update);
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Car>.Filter.Eq(x => x.Id, id);
        await _cars.DeleteOneAsync(filter);
    }
}
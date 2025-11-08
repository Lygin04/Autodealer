using Autodealer.Data;
using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using MongoDB.Driver;

namespace Autodealer.Services;

public class CarService(ICarRepository carRepository) : ICarService
{
    public async Task<IEnumerable<Car>?> GetAll()
    {
        return await carRepository.GetAll();
    }

    public Car GetById(string id)
    {
        return carRepository.GetById(id);
    }

    public async Task<Car> Create(CarDto newCarDto)
    {
        return await carRepository.Create(newCarDto);
    }

    public async Task Update(Car newCar)
    {
        await carRepository.Update(newCar);
    }

    public async Task Delete(string id)
    {
        await carRepository.Delete(id);
    }
}
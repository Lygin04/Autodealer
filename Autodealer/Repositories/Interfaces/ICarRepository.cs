using Autodealer.Dto;
using Autodealer.Entities;

namespace Autodealer.Repositories.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<Car>?> GetAll();
    Car GetById(string id);
    Task<Car> Create(CarDto car);
    Task Update(Car newCar);
    Task Delete(string id);
}